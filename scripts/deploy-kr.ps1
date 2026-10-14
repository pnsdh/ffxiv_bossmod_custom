# Builds the plugin against the KR Dalamud and deploys it to the KR dev plugin folder.
# Usage: powershell -ExecutionPolicy Bypass -File scripts\deploy-kr.ps1 [-ModulesOnly]
#   -ModulesOnly: only copy module dlls (BossMod.Ultimate etc.) into %APPDATA%\vbm\modules-dev,
#                 which the running plugin hot-reloads without restarting the game.
param([switch]$ModulesOnly)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$launcher = Join-Path $env:APPDATA 'XIVLauncherKR'
$publish = Join-Path $root 'BossMod\bin\Release\publish'
$target = Join-Path $launcher 'customPlugins\BossMod'
# a dev plugin (IsDev) loads module dlls from modules-dev, not modules (PackLoader.ModuleDir); upstream fills it only from
# debug builds, and the plugin itself copies its modules to 'modules' on start, so this script has to put them there
$modules = Join-Path $env:APPDATA 'vbm\modules-dev'

# use KR Dalamud (and its FFXIVClientStructs, which match the KR client) instead of the global one / local submodule
$env:DALAMUD_HOME = Join-Path $launcher 'addon\Hooks\dev\'
if (Test-Path $publish) { Remove-Item -Recurse -Force $publish }
# module projects are built by BossMod's PackagePublish target without a restore of their own
foreach ($p in 'BossMod.Autorotation', 'BossMod.Modules', 'BossMod.Ultimate') {
    dotnet restore (Join-Path $root "$p\$p.csproj") -p:Configuration=Release -p:LocalCS=false
    if ($LASTEXITCODE -ne 0) { throw "Restore of $p failed" }
}
# same versioning as release-custom.yml (<upstream major.minor.build>.<commit count>), so local builds don't look older than releases
[xml]$csproj = Get-Content (Join-Path $root 'BossMod\BossMod.csproj')
$base = ($csproj.Project.PropertyGroup.Version | Where-Object { $_ } | Select-Object -First 1).Split('.')
$version = "$($base[0]).$($base[1]).$($base[2]).$(git -C $root rev-list --count HEAD)"
Write-Host "Version: $version"
dotnet publish (Join-Path $root 'BossMod\BossMod.csproj') -c Release -p:LocalCS=false -p:Version=$version
if ($LASTEXITCODE -ne 0) { throw "Build failed" }

# same manifest changes as release-custom.yml
$manifestPath = Join-Path $publish 'BossMod.json'
$m = Get-Content $manifestPath -Raw | ConvertFrom-Json
$m.Name = 'Boss Mod (Custom)'
$m.RepoUrl = 'https://github.com/pnsdh/ffxiv_bossmod_custom'
$m.Punchline = '레이드 기믹 실수 방지 (한국 서버 공략 옵션 추가)'
$m.Description = '보스 기믹 레이더, 자동 로테이션, 쿨다운 플래너, AI를 제공하는 플러그인입니다. 본가(awgil/ffxiv_bossmod)를 자동으로 따라가며, 한국 서버 공략에 맞춘 옵션과 한국어 UI를 추가한 버전입니다.'
$m | Add-Member -NotePropertyName Tags -NotePropertyValue @('boss', 'raid', 'radar', 'korean', 'kr') -Force
[IO.File]::WriteAllText($manifestPath, ($m | ConvertTo-Json -Depth 10), (New-Object Text.UTF8Encoding $false))

$moduleDlls ='BossMod.Autorotation.dll', 'BossMod.Modules.dll', 'BossMod.Ultimate.dll'
New-Item -ItemType Directory -Force $modules | Out-Null
foreach ($m in $moduleDlls) { Copy-Item (Join-Path $publish $m) $modules -Force }
Write-Host "Modules copied to $modules"
if ($ModulesOnly) { return }

# overwrite files in place instead of recreating the folder: Dalamud's dev plugin auto-reload watches this folder,
# and deleting it breaks the watcher
New-Item -ItemType Directory -Force $target | Out-Null
# skip the packager's BossMod\latest.zip subfolder
$files = Get-ChildItem $publish | Where-Object { $_.Name -ne 'BossMod' }
# BossMod.dll last: its change is what triggers the reload, so everything else must already be in place
$files | Where-Object { $_.Name -ne 'BossMod.dll' } | Copy-Item -Destination $target -Recurse -Force
Copy-Item (Join-Path $publish 'BossMod.dll') $target -Force
# remove leftovers from older builds
Get-ChildItem $target | Where-Object { $files.Name -notcontains $_.Name } | Remove-Item -Recurse -Force
Write-Host "Deployed to $target"
