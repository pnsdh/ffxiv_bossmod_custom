# Builds the plugin against the KR Dalamud and deploys it to the KR dev plugin folder.
# Usage: powershell -ExecutionPolicy Bypass -File scripts\deploy-kr.ps1 [-ModulesOnly]
#   -ModulesOnly: only copy module dlls (BossMod.Ultimate etc.) into %APPDATA%\vbm\modules,
#                 which the running plugin hot-reloads without restarting the game.
param([switch]$ModulesOnly)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$launcher = Join-Path $env:APPDATA 'XIVLauncherKR'
$publish = Join-Path $root 'BossMod\bin\Release\publish'
$target = Join-Path $launcher 'customPlugins\BossMod'
$modules = Join-Path $env:APPDATA 'vbm\modules'

# use KR Dalamud (and its FFXIVClientStructs, which match the KR client) instead of the global one / local submodule
$env:DALAMUD_HOME = Join-Path $launcher 'addon\Hooks\dev\'
if (Test-Path $publish) { Remove-Item -Recurse -Force $publish }
# module projects are built by BossMod's PackagePublish target without a restore of their own
foreach ($p in 'BossMod.Autorotation', 'BossMod.Modules', 'BossMod.Ultimate') {
    dotnet restore (Join-Path $root "$p\$p.csproj") -p:Configuration=Release -p:LocalCS=false
    if ($LASTEXITCODE -ne 0) { throw "Restore of $p failed" }
}
dotnet publish (Join-Path $root 'BossMod\BossMod.csproj') -c Release -p:LocalCS=false
if ($LASTEXITCODE -ne 0) { throw "Build failed" }

$moduleDlls = 'BossMod.Autorotation.dll', 'BossMod.Modules.dll', 'BossMod.Ultimate.dll'
if ($ModulesOnly) {
    New-Item -ItemType Directory -Force $modules | Out-Null
    foreach ($m in $moduleDlls) { Copy-Item (Join-Path $publish $m) $modules -Force }
    Write-Host "Modules copied to $modules"
    return
}

if (Test-Path $target) { Remove-Item -Recurse -Force $target }
New-Item -ItemType Directory -Force $target | Out-Null
# skip the packager's BossMod\latest.zip subfolder
Get-ChildItem $publish | Where-Object { $_.Name -ne 'BossMod' } | Copy-Item -Destination $target -Recurse -Force
Write-Host "Deployed to $target"
