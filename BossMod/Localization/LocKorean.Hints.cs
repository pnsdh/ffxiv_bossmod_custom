namespace BossMod;

// Korean translations: in-fight hints of all modules and generic components
static partial class LocKorean
{
    private static readonly (string, string)[] Hints =
    [
        // BossMod.Modules/Dawntrail/Alliance/A11Prishe/CrystallineThorns.cs
        ("GTFO from spikes!", "가시에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Alliance/A13ArkAngels/Utsusemi.cs
        ("Kite the add!", "쫄 끌고 다니기!"),
        // BossMod.Modules/Dawntrail/Alliance/A14ShadowLord/CthonicFury.cs
        ("GTFO from aoe!", "장판에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Alliance/A22OmegaTheOne/CitadelSiege.cs
        ("Jump!", "점프!"),
        // BossMod.Modules/Dawntrail/Alliance/A22OmegaTheOne/GuidedMissile.cs
        ("Bait away from party!", "파티에서 떨어져서 유도!"),
        ("GTFO from baited AOE!", "유도 장판에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Alliance/A35Promathia/A35Promathia.cs
        ("Go to platform!", "발판으로 이동!"),
        // BossMod.Modules/Dawntrail/Chaotic/Ch01CloudOfDarkness/DelugeOfDarkness.cs
        ("Cell breaks in {0}s", "{0}초 후 감옥 파괴"),
        // BossMod.Modules/Dawntrail/Chaotic/Ch01CloudOfDarkness/EnaeroEndeath.cs
        ("Delayed {0}", "지연 {0}"),
        // BossMod.Modules/Dawntrail/Chaotic/Ch01CloudOfDarkness/GrimEmbrace.cs
        ("Dodge {0} in {1}s", "{1}초 후 {0} 회피"),
        // BossMod.Modules/Dawntrail/Criterion/C01MerchantsTale/C012LoneSwordmaster/C012LoneSwordmaster.cs
        ("Bait away from raid!", "공격대에서 떨어져서 유도!"),
        ("GTFO from baited aoe!", "유도 장판에서 나가세요!"),
        ("Bait away from rocks!", "바위에서 떨어져서 유도!"),
        // BossMod.Modules/Dawntrail/Criterion/C01MerchantsTale/C013PariOfPlenty/C013PariOfPlenty.cs
        ("Next: stack", "다음: 쉐어"),
        ("Next: spread", "다음: 산개"),
        ("Side: {0}", "방향: {0}"),
        ("Bait: {0}", "유도: {0}"),
        // BossMod.Modules/Dawntrail/DeepDungeon/PilgrimsTraverse/D50Ogbunabali.cs
        ("Go to quicksand!", "모래늪으로 이동!"),
        ("Stay out of quicksand!", "모래늪 밖에 있기!"),
        // BossMod.Modules/Dawntrail/DeepDungeon/PilgrimsTraverse/D99TheFinalVerse/D99TheFinalVerse.cs
        ("HP: {0} +{1}%", "HP: {0} +{1}%"),
        ("Correct color: {0}", "올바른 색: {0}"),
        // BossMod.Modules/Dawntrail/Dungeon/D02WorqorZormor/D022Kahderyor.cs
        ("Get to safe zone!", "안전 지대로!"),
        ("Break debris!", "잔해 부수기!"),
        // BossMod.Modules/Dawntrail/Dungeon/D02WorqorZormor/D023Gurfurlur.cs
        ("Touch the balls!", "구슬 닿기!"),
        // BossMod.Modules/Dawntrail/Dungeon/D11MesoTerminal/D112BloodyHeadsman.cs
        ("Prepare to cleanse!", "에스나 준비!"),
        ("Cleanse yourself!", "에스나 받기!"),
        // BossMod.Modules/Dawntrail/Dungeon/D12Mistwake/D121TrenoCatoblepas.cs
        ("Good luck, buddy", "행운을 빕니다"),
        // BossMod.Modules/Dawntrail/Extreme/Ex1Valigarmanda/HailOfFeathers.cs
        ("Kill feather!", "깃털 처치!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex3Sphene/AbsoluteAuthority.cs
        ("Stack with someone!", "아무하고나 쉐어!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex3Sphene/RadicalShift.cs
        ("Go to safe platform!", "안전한 발판으로!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex3Sphene/VirtualShiftEarth.cs
        ("Go to ground!", "지상으로 이동!"),
        ("Hide behind boulder!", "바위 뒤에 숨기!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex3Sphene/VirtualShiftIce.cs
        ("Stretch tether!", "선 늘리기!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex4Zelenia/Bloom1.cs
        ("GTFO from rose tile!", "장미 타일에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex4Zelenia/Bloom3.cs
        ("Don't connect towers!", "탑 연결하지 마세요!"),
        ("GTFO from tile!", "타일에서 나가세요!"),
        ("Too many people in tower!", "탑 인원 초과!"),
        ("Soak a tower!", "탑 밟기!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex4Zelenia/Bloom4.cs
        ("Go to safe quadrant!", "안전한 사분면으로!"),
        ("Stay away from connected tiles!", "연결된 타일에서 떨어지세요!"),
        ("Stay away from baiters!", "유도 대상에게서 떨어지세요!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex4Zelenia/Tiles.cs
        ("GTFO from spreads!", "산개 범위에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex5Necron/ColdGrip.cs
        ("Safe side: {0}", "안전한 쪽: {0}"),
        // BossMod.Modules/Dawntrail/Extreme/Ex5Necron/Shockwave.cs
        ("Next: {0}", "다음: {0}"),
        ("Unable to predict order. Hope your party members were watching!", "순서 예측 불가. 파티원이 잘 봤기를!"),
        ("GTFO from other healer!", "다른 힐러에게서 나가세요!"),
        ("Stack with party!", "파티와 쉐어!"),
        ("Stack with healer!", "힐러와 쉐어!"),
        ("Bait away from other {0}!", "다른 {0}에게서 떨어져서 유도!"),
        ("Stack with partner!", "파트너와 쉐어!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex6GuardianArkveld/AethericResonance.cs
        ("Bait away from towers!", "탑에서 떨어져서 유도!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex6GuardianArkveld/ClamorousCharge.cs
        ("Get away from buddy!", "짝에게서 떨어지세요!"),
        ("GTFO from bait!", "유도 장판에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex6GuardianArkveld/WyvernsWeal.cs
        ("Laser starts in {0}s", "{0}초 후 레이저 시작"),
        // BossMod.Modules/Dawntrail/Extreme/Ex7Doomtrain/Ex7Doomtrain.cs
        ("Get to launchpad!", "발사대로 이동!"),
        // BossMod.Modules/Dawntrail/Extreme/Ex8Enuo/Components.cs
        ("GTFO from orb!", "구슬에서 나가세요!"),
        ("Soak an orb!", "구슬 하나 밟기!"),
        ("Soak the orb!", "구슬 밟기!"),
        // BossMod.Modules/Dawntrail/Foray/CriticalEngagement/CommandUrn.cs
        ("Adds enrage in {0}s!", "{0}초 후 쫄 전멸기!"),
        // BossMod.Modules/Dawntrail/Foray/CriticalEngagement/CrescentBerserker.cs
        ("Stay close to puddle!", "장판 가까이 있기!"),
        // BossMod.Modules/Dawntrail/Foray/CriticalEngagement/MysteriousMindflayer.cs
        ("Move away from imp!", "임프에게서 떨어지세요!"),
        // BossMod.Modules/Dawntrail/Foray/CriticalEngagement/NeoGarula.cs
        ("Preposition near bird!", "새 근처에 미리 자리 잡기!"),
        // BossMod.Modules/Dawntrail/Foray/CriticalEngagement/TradeTortoise.cs
        ("Drop off!", "반납!"),
        ("Pick up {0} coin{1}!", "코인 {0}개 줍기!"),
        // BossMod.Modules/Dawntrail/Foray/FATE/Lifereaper.cs
        ("Follow the charge attack!", "돌진 따라가기!"),
        ("Keep away from the charge attack!", "돌진에서 떨어지세요!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB1DemonTablet/Explosion.cs
        ("Go to statue!", "석상으로 이동!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB1DemonTablet/Meteors.cs
        ("Drop meteor near marker!", "마커 근처에 메테오 떨구기!"),
        ("Stay on meteor side!", "메테오 쪽에 있기!"),
        ("Stay away from meteor side!", "메테오 쪽에서 떨어지세요!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB2DeadStars/DecisiveBattle.cs
        ("Hit by too many AOEs!", "장판에 너무 많이 맞음!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB2DeadStars/Fireball.cs
        ("GTFO from forbidden bait!", "금지된 유도 장판에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB2DeadStars/PrimordialChaos.cs
        ("Debuff: {0} x{1}", "디버프: {0} x{1}"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB2DeadStars/Snowball.cs
        ("GTFO from forbidden charge!", "금지된 돌진에서 나가세요!"),
        ("Stand in charge!", "돌진 경로에 서기!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB3MarbleDragon/Rain3.cs
        ("Stand in ice puddle!", "얼음 장판 안에 서기!"),
        ("Destroy ice cubes!", "얼음 큐브 부수기!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB4Magitaur/Conduit.cs
        ("Avoid baiting!", "유도 피하기!"),
        ("Bait purple canister!", "보라색 통 유도!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB4Magitaur/HolyLance.cs
        ("Sac! Your stack cannot be resolved!", "희생! 쉐어 처리 불가!"),
        ("Order: {0}, platform: {1} {2}", "순서: {0}, 발판: {1} {2}"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB4Magitaur/RuneAxe.cs
        ("GTFO from platforms!", "발판에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Foray/ForkedTower/FTB4Magitaur/Unseal.cs
        ("Autos: close", "평타: 가까이"),
        ("Autos: far", "평타: 멀리"),
        ("Get on platform!", "발판 위로!"),
        ("Tankbuster", "탱버"),
        ("Too close to boss!", "보스에 너무 가까움!"),
        ("Too far from boss!", "보스에서 너무 멂!"),
        // BossMod.Modules/Dawntrail/Hunt/RankA/RraxYitya.cs
        ("Sequence: {0}", "순서: {0}"),
        // BossMod.Modules/Dawntrail/Quantum/Q01TheFinalVerse/LightDark.cs
        ("Buff remaining: {0}s", "버프 남은 시간: {0}초"),
        ("Stay dark!", "어둠 유지!"),
        // BossMod.Modules/Dawntrail/Quantum/Q01TheFinalVerse/Neutralize.cs
        ("GTFO from same color!", "같은 색에게서 나가세요!"),
        ("Stack with opposite color!", "반대 색과 쉐어!"),
        // BossMod.Modules/Dawntrail/Quantum/Q01TheFinalVerse/Q01TheFinalVerse.cs
        ("Get light debuff!", "빛 디버프 받기!"),
        // BossMod.Modules/Dawntrail/Quantum/Q01TheFinalVerse/Shackles.cs
        ("Allies in radius: {0}", "범위 내 아군: {0}"),
        ("Go far to bait tether!", "멀리 가서 선 유도!"),
        // BossMod.Modules/Dawntrail/Quantum/Q01TheFinalVerse/SinBearer.cs
        ("Switch to dark debuff!", "어둠 디버프로 전환!"),
        ("Stack with buddy!", "짝과 쉐어!"),
        ("GTFO from forbidden players!", "금지된 플레이어에게서 나가세요!"),
        ("Cleanse {0}!", "{0} 에스나!"),
        // BossMod.Modules/Dawntrail/Quantum/Q01TheFinalVerse/Spinelash.cs
        ("Aim for window!", "창문 쪽으로 조준!"),
        // BossMod.Modules/Dawntrail/Savage/RM01SBlackCat/Mouser.cs
        ("{0} in {1}s", "{1}초 후 {0}"),
        ("Aim to intact cell!", "멀쩡한 칸으로 조준!"),
        ("GTFO from shockwave!", "충격파에서 나가세요!"),
        ("Spread by roles!", "역할별 산개!"),
        // BossMod.Modules/Dawntrail/Savage/RM03SBruteBomber/Lariat.cs
        ("Stack in pairs!", "2인 쉐어!"),
        // BossMod.Modules/Dawntrail/Savage/RM04SWickedThunder/AetherialConversion.cs
        ("{0} {1}", "{0} {1}"),
        // BossMod.Modules/Dawntrail/Savage/RM04SWickedThunder/ElectropeEdge.cs
        ("Order: {0}, position: {1}", "순서: {0}, 위치: {1}"),
        // BossMod.Modules/Dawntrail/Savage/RM04SWickedThunder/IonCluster.cs
        ("Move closer!", "더 가까이!"),
        ("Hide behind tank!", "탱커 뒤에 숨기!"),
        ("Get hit by the cone!", "부채꼴 맞기!"),
        // BossMod.Modules/Dawntrail/Savage/RM04SWickedThunder/SunriseSabbath.cs
        ("Bait order: {0}", "유도 순서: {0}"),
        // BossMod.Modules/Dawntrail/Savage/RM04SWickedThunder/WideningNarrowingWitchHunt.cs
        ("Next bait: {0}", "다음 유도: {0}"),
        // BossMod.Modules/Dawntrail/Savage/RM05SDancingGreen/ASideBSide.cs
        ("Next: roles", "다음: 역할별"),
        ("Next: light parties", "다음: 라이트 파티"),
        // BossMod.Modules/Dawntrail/Savage/RM05SDancingGreen/FunkyFloor.cs
        ("Short timer", "짧은 타이머"),
        ("Long timer", "긴 타이머"),
        ("Bait next!", "다음 유도!"),
        ("Go to spotlight!", "조명 아래로!"),
        // BossMod.Modules/Dawntrail/Savage/RM05SDancingGreen/Wavelength.cs
        ("GTFO from other players!", "다른 플레이어에게서 나가세요!"),
        ("GTFO from forbidden stacks!", "금지된 쉐어에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Savage/RM06SSugarRiot/Adds.cs
        ("Cat stacks: {0}", "고양이 중첩: {0}"),
        // BossMod.Modules/Dawntrail/Savage/RM06SSugarRiot/Arena.cs
        ("Stand in water!", "물에 서기!"),
        ("Avoid water!", "물 피하기!"),
        // BossMod.Modules/Dawntrail/Savage/RM06SSugarRiot/ColorRiot.cs
        ("Bait close!", "가까이 유도!"),
        ("Bait far!", "멀리 유도!"),
        // BossMod.Modules/Dawntrail/Savage/RM06SSugarRiot/DesertPhase.cs
        ("Defamation in {0}s", "{0}초 후 명예형"),
        ("Party stack in {0}s", "{0}초 후 파티 쉐어"),
        ("Aim at quicksand!", "모래늪 쪽으로 조준!"),
        // BossMod.Modules/Dawntrail/Savage/RM06SSugarRiot/StormPhase.cs
        ("GTFO from grass!", "풀에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Savage/RM07SBruteAbominator/BrutalImpact.cs
        ("Raidwide", "전체 공격"),
        // BossMod.Modules/Dawntrail/Savage/RM08SHowlingBlade/Mooncleaver.cs
        ("Shirk!", "기피!"),
        ("Provoke!", "도발!"),
        // BossMod.Modules/Dawntrail/Savage/RM08SHowlingBlade/QuakeIII.cs
        ("GTFO from other stack!", "다른 쉐어에서 나가세요!"),
        ("GTFO from tanks!", "탱커에게서 나가세요!"),
        // BossMod.Modules/Dawntrail/Savage/RM08SHowlingBlade/RevolutionaryReign.cs
        ("Next: in", "다음: 안"),
        ("Next: out", "다음: 밖"),
        // BossMod.Modules/Dawntrail/Savage/RM08SHowlingBlade/TacticalPack.cs
        ("GTFO from stack!", "쉐어에서 나가세요!"),
        ("Cleanse!", "에스나!"),
        // BossMod.Modules/Dawntrail/Savage/RM08SHowlingBlade/TwofoldTempest.cs
        ("Pass tether!", "선 넘기기!"),
        // BossMod.Modules/Dawntrail/Savage/RM08SHowlingBlade/UltraviolentRay.cs
        ("GTFO from other baits!", "다른 유도에서 나가세요!"),
        // BossMod.Modules/Dawntrail/Savage/RM09SVampFatale/Aetherletting.cs
        ("Puddle drops in {0}s", "{0}초 후 장판 생성"),
        // BossMod.Modules/Dawntrail/Savage/RM10STheXtremes/BlueComponents.cs
        ("Bait away from other party!", "다른 파티에서 떨어져서 유도!"),
        // BossMod.Modules/Dawntrail/Savage/RM10STheXtremes/Bubble.cs
        ("Hit bubble!", "거품 맞히기!"),
        ("Bait away from bubble!", "거품에서 떨어져서 유도!"),
        // BossMod.Modules/Dawntrail/Savage/RM11STheTyrant/Meteorain.cs
        ("Hide behind meteor!", "메테오 뒤에 숨기!"),
        ("GTFO from meteor!", "메테오에서 나가세요!"),
        ("Take tether!", "선 가져가기!"),
        // BossMod.Modules/Dawntrail/Savage/RM12S1TheLindwurm/GrotesquerieAct2.cs
        ("Order: {0}, assignment: {1}", "순서: {0}, 담당: {1}"),
        ("Too few soakers in the tower!", "탑 인원 부족!"),
        ("Too many soakers in the tower!", "탑 인원 초과!"),
        ("Soak the tower!", "탑 밟기!"),
        // BossMod.Modules/Dawntrail/Savage/RM12S2TheLindwurm/BloodMana.cs
        ("Debuff: {0}", "디버프: {0}"),
        // BossMod.Modules/Dawntrail/Savage/RM12S2TheLindwurm/Replication1.cs
        ("Assignment: {0}", "담당: {0}"),
        ("Avoid baiting fire!", "불 유도 피하기!"),
        ("Avoid baiting dark!", "어둠 유도 피하기!"),
        // BossMod.Modules/Dawntrail/Savage/RM12S2TheLindwurm/StagingAssignment.cs
        ("Grab correct tether!", "올바른 선 잡기!"),
        // BossMod.Modules/Dawntrail/Trial/T05Necron.cs
        ("Aetherblight: {0}", "푸른 파동: {0}"),
        ("Kill the adds!", "쫄 처치!"),
        ("Jump into the wells!", "우물로 뛰어들기!"),
        // BossMod.Modules/Dawntrail/Unreal/Un1Byakko/UnrelentingAnguish.cs
        ("GTFO from other bubble!", "다른 거품에서 나가세요!"),
        // BossMod.Modules/Endwalker/Alliance/A13Azeyma/WildfireWard.cs
        ("Go to safe zone!", "안전 지대로!"),
        ("About to be knocked into fire!", "불 속으로 넉백당함!"),
        // BossMod.Modules/Endwalker/Alliance/A21Nophica/MatronsBreath.cs
        ("Go to correct safe zone!", "올바른 안전 지대로!"),
        // BossMod.Modules/Endwalker/Criterion/C01ASS/C011Silkie/SlipperySoap.cs
        ("Stack inside charge!", "돌진 안에서 쉐어!"),
        ("Move!", "이동!"),
        ("Prepare to spread", "산개 준비"),
        // BossMod.Modules/Endwalker/Criterion/C01ASS/C012Gladiator/WrathOfRuin.cs
        ("Go to correct cell!", "올바른 칸으로!"),
        // BossMod.Modules/Endwalker/Criterion/C01ASS/C013Shadowcaster/CrypticFlames.cs
        ("Break order: {0}", "해제 순서: {0}"),
        // BossMod.Modules/Endwalker/Criterion/C01ASS/C013Shadowcaster/FiresteelStrike.cs
        ("Hide behind someone!", "누군가 뒤에 숨기!"),
        ("Intercept next cleave!", "다음 광역 공격 대신 맞기!"),
        // BossMod.Modules/Endwalker/Criterion/C01ASS/C013Shadowcaster/InfernWave.cs
        ("Baiting mulitple cones!", "부채꼴 여러 개 유도 중!"),
        ("GTFO from other bait!", "다른 유도에서 나가세요!"),
        // BossMod.Modules/Endwalker/Criterion/C02AMR/C021Shishio/UnnaturalWail.cs
        ("Debuff order: {0}", "디버프 순서: {0}"),
        // BossMod.Modules/Endwalker/Criterion/C02AMR/C022Gorai/MalformedReincarnation.cs
        ("Bait away from other towers!", "다른 탑에서 떨어져서 유도!"),
        // BossMod.Modules/Endwalker/Criterion/C02AMR/C023Moko/Giri.cs
        ("Safespots: {0}", "안전 지대: {0}"),
        ("Safespot: {0}", "안전 지대: {0}"),
        ("Second safespots: {0}", "두 번째 안전 지대: {0}"),
        // BossMod.Modules/Endwalker/Criterion/C02AMR/C023Moko/ShadowTwin.cs
        ("Untethered bait: {0}", "선 없는 유도: {0}"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C031Ketuduke/FlukeGale.cs
        ("Aim towards safe zone!", "안전 지대 쪽으로 조준!"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C032Lala/Analysis.cs
        ("Rotation: {0}", "회전: {0}"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C032Lala/ArcanePoint.cs
        ("Spread on different squares!", "다른 칸으로 산개!"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C032Lala/SpatialTactics.cs
        ("Remaining stacks: {0}", "남은 중첩: {0}"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C033Statice/Dartboard.cs
        ("GTFO from forbidden color!", "금지된 색에서 나가세요!"),
        ("Stay on different segments!", "서로 다른 구역에 있기!"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C033Statice/Fireworks.cs
        ("Tether: {0}", "선: {0}"),
        ("Stay with partner!", "파트너와 함께 있기!"),
        ("Break chains!", "사슬 끊기!"),
        // BossMod.Modules/Endwalker/Criterion/C03AAI/C033Statice/TrickReload.cs
        ("Order: {0} -> {1} -> {2}", "순서: {0} -> {1} -> {2}"),
        ("Order: {0} -> ???", "순서: {0} -> ???"),
        // BossMod.Modules/Endwalker/DeepDungeon/EurekaOrthos/D20CloningNode.cs
        ("{0} will tether to the clones on the edge of the arena. Those are the dragons that will be casting a cone.\nAfter a certain point, the boss will tether x2 sets of adds, find the first safe spot, rotate into the next.", "{0}이(가) 전장 가장자리의 분신들과 선으로 연결됩니다. 이들이 부채꼴을 시전할 용들입니다.\n일정 시점 이후 보스가 쫄 2세트와 선으로 연결되니, 첫 안전 지대를 찾은 뒤 다음 안전 지대로 회전하세요."),
        // BossMod.Modules/Endwalker/DeepDungeon/EurekaOrthos/D70Aeturna.cs
        ("Tether is stretched!", "선이 늘어남!"),
        ("Stretch tether further!", "선 더 늘리기!"),
        // BossMod.Modules/Endwalker/Dungeon/D01TowerOfZot/D011Minduruva.cs
        ("You were poisoned! Get cleansed fast.", "독에 걸림! 빨리 에스나 받기."),
        ("Cleanse yourself! (Poison).", "에스나 받기! (독)."),
        ("Cleanse {0} (Poison)", "{0} 에스나 (독)"),
        ("3 single target hits + DoTs", "단일 대상 3회 + 도트"),
        // BossMod.Modules/Endwalker/Dungeon/D02TowerOfBabil/D023Anima.cs
        ("Get away from orb!", "구슬에서 떨어지세요!"),
        // BossMod.Modules/Endwalker/Dungeon/D11LapisManalis/D111Albion.cs
        ("Bait away!", "떨어져서 유도!"),
        // BossMod.Modules/Endwalker/Dungeon/D11LapisManalis/D112GalateaMagna.cs
        ("You were doomed! Get cleansed fast.", "죽음의 선고! 빨리 에스나 받기."),
        ("Cleanse yourself! (Doom).", "에스나 받기! (죽음의 선고)."),
        ("Cleanse {0} (Doom)", "{0} 에스나 (죽음의 선고)"),
        // BossMod.Modules/Endwalker/Extreme/Ex1Zodiark/Adikia.cs
        ("GTFO from side smash aoe!", "측면 강타 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex1Zodiark/Algedon.cs
        ("GTFO from diagonal aoe!", "대각선 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex1Zodiark/Ania.cs
        ("Pass aggro!", "어그로 넘기기!"),
        ("GTFO from tank!", "탱커에게서 나가세요!"),
        ("Taunt!", "도발!"),
        // BossMod.Modules/Endwalker/Extreme/Ex1Zodiark/AstralEclipse.cs
        ("GTFO from explosion!", "폭발에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex1Zodiark/Exoterikos.cs
        ("GTFO from exo aoe!", "엑소 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex1Zodiark/Paradeigma.cs
        ("GTFO from bird/behemoth aoe!", "새/베히모스 장판에서 나가세요!"),
        ("GTFO from snake aoe!", "뱀 장판에서 나가세요!"),
        ("GTFO from fire aoe!", "불 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex2Hydaelyn/Crystallize.cs
        ("Hit by two aoes!", "장판 두 개에 맞음!"),
        // BossMod.Modules/Endwalker/Extreme/Ex2Hydaelyn/HerosSundering.cs
        ("Turn boss away from raid!", "보스를 공격대 반대로 돌리기!"),
        ("GTFO from tankbuster aoe!", "탱버 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex2Hydaelyn/Lightwave1.cs
        ("GTFO from wave!", "파도에서 나가세요!"),
        ("Hide behind crystal!", "크리스탈 뒤에 숨기!"),
        // BossMod.Modules/Endwalker/Extreme/Ex2Hydaelyn/Lightwave2.cs
        ("GTFO from glory aoe!", "영광 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex2Hydaelyn/Spectrum.cs
        ("Stack with co-tank", "다른 탱커와 쉐어"),
        // BossMod.Modules/Endwalker/Extreme/Ex2Hydaelyn/WeaponTracker.cs
        ("GTFO from weapon aoe!", "무기 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex3Endsinger/DespairUnforgotten.cs
        ("Stay in center", "중앙에 있기"),
        ("Go to edge", "가장자리로"),
        // BossMod.Modules/Endwalker/Extreme/Ex3Endsinger/Planets.cs
        ("GTFO from head aoe!", "머리 장판에서 나가세요!"),
        ("GTFO from planet aoe!", "행성 장판에서 나가세요!"),
        ("About to be knocked into wall!", "벽으로 넉백당함!"),
        // BossMod.Modules/Endwalker/Extreme/Ex6Golbez/AzdajasShadow.cs
        ("Next mechanic: {0}", "다음 기믹: {0}"),
        // BossMod.Modules/Endwalker/Extreme/Ex6Golbez/EventideFallTriad.cs
        ("Stack by: {0}", "쉐어 기준: {0}"),
        // BossMod.Modules/Endwalker/Extreme/Ex7Zeromus/BlackHole.cs
        ("GTFO from black hole baiter!", "블랙홀 유도자에게서 나가세요!"),
        // BossMod.Modules/Endwalker/Extreme/Ex7Zeromus/VisceralWhirl.cs
        ("Break tether!", "선 끊기!"),
        // BossMod.Modules/Endwalker/Extreme/Ex7Zeromus/VoidMeteor.cs
        ("Avoid other meteors!", "다른 메테오 피하기!"),
        ("Stretch the tether!", "선 늘리기!"),
        ("GTFO from charges!", "돌진에서 나가세요!"),
        // BossMod.Modules/Endwalker/FATE/Daivadipa.cs
        ("Apply backwards march debuff", "후방 이동 명령 디버프 받기"),
        ("Apply right march debuff", "우측 이동 명령 디버프 받기"),
        ("Apply forwards march debuff", "전방 이동 명령 디버프 받기"),
        ("Apply left march debuff", "좌측 이동 명령 디버프 받기"),
        ("Aim into AOEs!", "장판 쪽으로 조준!"),
        // BossMod.Modules/Endwalker/Hunt/RankA/Sugriva.cs
        ("Stack and knockback", "쉐어 후 넉백"),
        ("Tankbuster cleave", "탱버 광역 공격"),
        // BossMod.Modules/Endwalker/Hunt/RankA/Yilan.cs
        ("Apply march debuffs", "이동 명령 디버프 받기"),
        // BossMod.Modules/Endwalker/Hunt/RankS/Ker.cs
        ("Mirrored interments left: {0}!", "남은 반전 매장: {0}!"),
        ("The next three interments will be mirrored!", "다음 매장 3회는 반전!"),
        ("The next four interments will be mirrored!", "다음 매장 4회는 반전!"),
        ("Pyretic on you! STOP everything!", "열병! 모든 행동 중지!"),
        ("Applies Pyretic - STOP everything until it runs out!", "열병 부여 - 끝날 때까지 모든 행동 중지!"),
        // BossMod.Modules/Endwalker/Quest/AnUnforeseenBargain/P2Andromalius.cs
        ("Take cover!", "엄폐!"),
        // BossMod.Modules/Endwalker/Quest/Endwalker/AetherialRay.cs
        ("Tankbuster 5x", "탱버 5회"),
        // BossMod.Modules/Endwalker/Quest/Endwalker/AkhMorn.cs
        ("Tankbuster x{0}", "탱버 x{0}"),
        // BossMod.Modules/Endwalker/Quest/WhereEverythingBegins/P2.cs
        ("Raidwide + poison wall spawn", "전체 공격 + 독 벽 생성"),
        // BossMod.Modules/Endwalker/Savage/P10SPandaemonium/EntanglingWeb.cs
        ("Stand near pillar!", "기둥 근처에 서기!"),
        ("Overlap with other web!", "다른 거미줄과 겹치기!"),
        ("GTFO from webs!", "거미줄에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P10SPandaemonium/PandaemoniacMeltdown.cs
        ("GTFO from other spreads!", "다른 산개에서 나가세요!"),
        ("Aim spread away from raid!", "산개를 공격대 반대로 조준!"),
        ("Aim stack away from spreads!", "쉐어를 산개 반대로 조준!"),
        // BossMod.Modules/Endwalker/Savage/P10SPandaemonium/Silkspit.cs
        ("GTFO from pillars!", "기둥에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P10SPandaemonium/Turrets.cs
        ("GTFO from one of the knockbacks!", "넉백 중 하나에서 나가세요!"),
        ("GTFO from knockback!", "넉백에서 나가세요!"),
        ("About to be knocked off platform!", "발판 밖으로 넉백당함!"),
        // BossMod.Modules/Endwalker/Savage/P11SThemis/DarkAndLight.cs
        ("{0} tether", "{0} 선"),
        // BossMod.Modules/Endwalker/Savage/P12S1Athena/Palladion.cs
        ("Too close!", "너무 가까움!"),
        ("Bait next aoe", "다음 장판 유도"),
        // BossMod.Modules/Endwalker/Savage/P12S1Athena/TrinityOfSouls.cs
        ("Trinity: {0}", "삼위일혼: {0}"),
        // BossMod.Modules/Endwalker/Savage/P12S2PallasAthena/CaloricTheory.cs
        ("Stay still!", "가만히 있기!"),
        // BossMod.Modules/Endwalker/Savage/P12S2PallasAthena/ClassicalConcepts.cs
        ("Stand between assigned shapes!", "지정 도형 사이에 서기!"),
        // BossMod.Modules/Endwalker/Savage/P12S2PallasAthena/Gaiaochos.cs
        ("Hide behind partner!", "파트너 뒤에 숨기!"),
        ("Intercept the charge!", "돌진 대신 맞기!"),
        ("GTFO from other charges!", "다른 돌진에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P12S2PallasAthena/PalladianGrasp.cs
        ("GTFO from cleaved side!", "공격받는 쪽에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P1SErichthonios/AetherExplosion.cs
        ("Hit by aether explosion!", "에테르 폭발에 맞음!"),
        // BossMod.Modules/Endwalker/Savage/P1SErichthonios/Flails.cs
        ("Hit by first flail!", "첫 번째 철퇴에 맞음!"),
        ("Hit by second flail!", "두 번째 철퇴에 맞음!"),
        // BossMod.Modules/Endwalker/Savage/P1SErichthonios/Intemperance.cs
        ("GTFO from delimiter!", "구획 경계에서 나가세요!"),
        ("Go to assigned cell!", "지정 칸으로!"),
        ("Stand in own cell!", "내 칸에 서기!"),
        ("Order: {0}, pattern: {1}.", "순서: {0}, 패턴: {1}."),
        // BossMod.Modules/Endwalker/Savage/P1SErichthonios/Knockback.cs
        ("GTFO from co-tank!", "다른 탱커에게서 나가세요!"),
        ("Stack with raid!", "공격대와 쉐어!"),
        ("Press invul!", "무적기 사용!"),
        ("Stack with target!", "대상과 쉐어!"),
        ("GTFO from target!", "대상에게서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P1SErichthonios/Shackles.cs
        ("Target of two tethers!", "선 두 개의 대상!"),
        // BossMod.Modules/Endwalker/Savage/P2SHippokampos/Cataract.cs
        ("GTFO from cataract!", "홍수에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P2SHippokampos/ChannelingFlow.cs
        ("Aim to hit partner!", "파트너를 맞히도록 조준!"),
        ("Avoid clipping irrelevant players!", "상관없는 사람 맞히지 않기!"),
        ("Too close to partner!", "파트너와 너무 가까움!"),
        ("GTFO from imminent flow!", "임박한 흐름에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P2SHippokampos/Coherence.cs
        ("Pass tether to tank!", "탱커에게 선 넘기기!"),
        ("Go behind tank!", "탱커 뒤로!"),
        ("Make sure ray hits everyone!", "광선이 모두 맞도록!"),
        ("Go in front of raid!", "공격대 앞으로!"),
        // BossMod.Modules/Endwalker/Savage/P2SHippokampos/Dissociation.cs
        ("GTFO from dissociation!", "머리 분리 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P2SHippokampos/PredatoryAvarice.cs
        ("GTFO from avarice!", "다중 각인에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P2SHippokampos/TaintedFlood.cs
        ("GTFO from flood!", "홍수에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/Ashplume.cs
        ("Stack in fours!", "4인 쉐어!"),
        ("Incorrect stack!", "쉐어 잘못됨!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/BirdDistance.cs
        ("Drag bird away!", "새를 멀리 끌기!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/BirdTether.cs
        ("GTFO from charge zone!", "돌진 범위에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/BrightenedFire.cs
        ("Get to correct position {0}!", "올바른 위치 {0}로!"),
        ("Get closer to adds!", "쫄에게 더 가까이!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/Cinderwing.cs
        ("GTFO from wing!", "날개에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/DarkblazeTwister.cs
        ("About to be knocked back into wall!", "벽으로 넉백당함!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/DarkenedFire.cs
        ("Too close to other players!", "다른 플레이어와 너무 가까움!"),
        ("Too far from other players!", "다른 플레이어와 너무 멂!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/DevouringBrand.cs
        ("GTFO from brand!", "낙인에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/Fireplume.cs
        ("GTFO from plume!", "겁화천초에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/FlamesOfAsphodelos.cs
        ("GTFO from cone!", "부채꼴에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/FledglingFlight.cs
        ("Get closer to eye placement position!", "눈 배치 위치로 더 가까이!"),
        ("Enter more aoes ({0}/{1})!", "장판 더 맞기 ({0}/{1})!"),
        ("GTFO from eyes ({0}/{1})!", "눈에서 나가세요 ({0}/{1})!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/StormsOfAsphodelos.cs
        ("Intercept tether!", "선 가로채기!"),
        ("GTFO from aoes!", "장판에서 나가세요!"),
        ("GTFO from tether!", "선에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P3SPhoinix/TrailOfCondemnation.cs
        ("Incorrect pairing!", "짝 잘못됨!"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/BeloneCoils.cs
        ("Soak the tower", "탑 밟기"),
        ("GTFO from tower", "탑에서 나가세요"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/DirectorsBelone.cs
        ("Debuffs: spread and prepare to handle!", "디버프: 산개 후 처리 준비!"),
        ("Debuffs: failed to handle", "디버프: 처리 실패"),
        ("Debuffs: OK", "디버프: OK"),
        ("Debuffs: grab!", "디버프: 받기!"),
        ("Debuffs: stack and prepare to pass!", "디버프: 쉐어 후 넘길 준비!"),
        ("Debuffs: pass!", "디버프: 넘기기!"),
        ("Debuffs: avoid", "디버프: 피하기"),
        ("Stack: {0}", "쉐어: {0}"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/ElementalBelone.cs
        ("GTFO from exploding square", "폭발하는 칸에서 나가세요"),
        ("Safe square: {0}", "안전 칸: {0}"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/InversiveChlamys.cs
        ("Tethers: prepare to intercept", "선: 가로챌 준비"),
        ("Tethers: intercept!", "선: 가로채기!"),
        ("Tethers: GTFO from others!", "선: 다른 사람에게서 나가세요!"),
        ("Tethers: OK", "선: OK"),
        ("Tethers: prepare to pass", "선: 넘길 준비"),
        ("Tethers: pass!", "선: 넘기기!"),
        ("Tethers: GTFO from aoe!", "선: 장판에서 나가세요!"),
        ("Tethers: avoid", "선: 피하기"),
        ("Intercept: {0}", "가로채기: {0}"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/Pinax.cs
        ("GTFO from acid square!", "산성 칸에서 나가세요!"),
        ("GTFO from fire square!", "불 칸에서 나가세요!"),
        ("GTFO from water square!", "물 칸에서 나가세요!"),
        ("GTFO from lighting square!", "번개 칸에서 나가세요!"),
        ("GTFO from center!", "중앙에서 나가세요!"),
        ("Pinax order: {0}", "배경판 순서: {0}"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/Shift.cs
        ("GTFO from sword!", "검에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P4S1Hesperos/VengefulBelone.cs
        ("Failed orbs...", "구슬 실패..."),
        ("GTFO from wrong orb!", "잘못된 구슬에서 나가세요!"),
        ("Pop next orb {0}/2!", "다음 구슬 터뜨리기 {0}/2!"),
        ("Avoid orbs", "구슬 피하기"),
        // BossMod.Modules/Endwalker/Savage/P4S2Hesperos/CurtainCall.cs
        ("Tether break order: {0}", "선 끊는 순서: {0}"),
        // BossMod.Modules/Endwalker/Savage/P4S2Hesperos/WreathOfThorns1.cs
        ("GTFO from AOE!", "장판에서 나가세요!"),
        ("Multiple soakers for the tower!", "탑 인원 초과!"),
        // BossMod.Modules/Endwalker/Savage/P4S2Hesperos/WreathOfThorns2.cs
        ("Stack with breaking tether!", "끊기는 선과 쉐어!"),
        // BossMod.Modules/Endwalker/Savage/P4S2Hesperos/WreathOfThorns3.cs
        ("GTFO from boss!", "보스에게서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P4S2Hesperos/WreathOfThorns4.cs
        ("Dark order: {0}", "어둠 순서: {0}"),
        // BossMod.Modules/Endwalker/Savage/P4S2Hesperos/WreathOfThorns5.cs
        ("Soak tower!", "탑 밟기!"),
        // BossMod.Modules/Endwalker/Savage/P5SProtoCarbuncle/RubyGlow3.cs
        ("Safespot for third: {0}", "세 번째 안전 지대: {0}"),
        // BossMod.Modules/Endwalker/Savage/P5SProtoCarbuncle/VenomousMass.cs
        ("GTFO from tankbuster!", "탱버에서 나가세요!"),
        // BossMod.Modules/Endwalker/Savage/P6SHegemone/AetheronecrosisPredation.cs
        ("Order: {0}, side: {1}", "순서: {0}, 방향: {1}"),
        // BossMod.Modules/Endwalker/Savage/P6SHegemone/Synergy.cs
        ("Stack with other tanks or press invuln!", "다른 탱커와 쉐어하거나 무적기!"),
        // BossMod.Modules/Endwalker/Savage/P6SHegemone/Transmission.cs
        ("Face away from raid", "공격대 반대로 바라보기"),
        ("Face raid", "공격대 바라보기"),
        ("Avoid transmission aoe!", "기생 장판 피하기!"),
        // BossMod.Modules/Endwalker/Savage/P7SAgdistis/DispersedCondensedAero.cs
        ("Stack with other tank or press invuln!", "다른 탱커와 쉐어하거나 무적기 사용!"),
        // BossMod.Modules/Endwalker/Savage/P7SAgdistis/ForbiddenFruitCommon.cs
        ("Hitting others with tether!", "선이 다른 사람에게 닿음!"),
        ("Clipped by other tethers!", "다른 선에 걸림!"),
        // BossMod.Modules/Endwalker/Savage/P8S1Hephaistos/Snake1.cs
        ("Order: {0} {1}", "순서: {0} {1}"),
        // BossMod.Modules/Endwalker/Savage/P8S1Hephaistos/Snake2.cs
        ("Petrify order: {0}, {1}", "석화 순서: {0}, {1}"),
        // BossMod.Modules/Endwalker/Savage/P8S2Hephaistos/Dominion.cs
        ("Soak order: {0}", "밟기 순서: {0}"),
        // BossMod.Modules/Endwalker/Savage/P8S2Hephaistos/EndOfDaysTethered.cs
        ("Move away from other baits!", "다른 유도 대상자에게서 떨어지세요!"),
        // BossMod.Modules/Endwalker/Savage/P8S2Hephaistos/LimitlessDesolation.cs
        ("Soak assigned tower!", "지정된 탑 밟기!"),
        // BossMod.Modules/Endwalker/Savage/P8S2Hephaistos/NaturalAlignment.cs
        ("Next NA: {0}", "다음 술식 기술: {0}"),
        // BossMod.Modules/Endwalker/Savage/P9SKokytos/Charibdys.cs
        ("GTFO from normal comets!", "일반 혜성에서 나가세요!"),
        ("Bait to glowing comet!", "빛나는 혜성 쪽으로 유도!"),
        ("Aim away from comets!", "혜성 반대쪽으로 조준!"),
        // BossMod.Modules/Endwalker/Savage/P9SKokytos/Dualspell.cs
        ("Pairs", "2인 쉐어"),
        ("Spread", "산개"),
        // BossMod.Modules/Endwalker/TreasureHunt/TheShiftingGymnasionAgonon/GymnasiouPithekos.cs
        ("Bait levinorb away!", "번개 구슬을 멀리 유도!"),
        // BossMod.Modules/Endwalker/TreasureHunt/TheShiftingGymnasionAgonon/Narkissos.cs
        ("Circles resolve before cross", "원형이 십자보다 먼저 터짐"),
        ("Circles resolve before cross, aim forced march into cross", "원형이 십자보다 먼저 터짐, 강제 이동은 십자 쪽으로 조준"),
        // BossMod.Modules/Endwalker/Unreal/Un1Ultima/Garuda.cs
        ("Stand near inner edge", "안쪽 가장자리 근처에 서기"),
        ("Go to edge!", "가장자리로!"),
        // BossMod.Modules/Endwalker/Unreal/Un1Ultima/Mechanics.cs
        ("Pass aggro to co-tank!", "다른 탱커에게 적개심 넘기기!"),
        ("Taunt boss!", "보스 도발!"),
        ("Move from boss", "보스에게서 떨어지기"),
        ("GTFO from ray aoe!", "광선 장판에서 나가세요!"),
        ("Kite the orb!", "구슬 끌고 다니기!"),
        ("GTFO from bit aoe!", "비트 장판에서 나가세요!"),
        // BossMod.Modules/Endwalker/Unreal/Un2Sephirot/P1FiendishRage.cs
        ("GTFO from marked players!", "표식 대상자에게서 떨어지세요!"),
        ("Stack with single group!", "한 그룹으로 쉐어!"),
        // BossMod.Modules/Endwalker/Unreal/Un2Sephirot/P1Ratzon.cs
        ("Spread! (debuff: {0})", "산개! (디버프: {0})"),
        // BossMod.Modules/Endwalker/Unreal/Un5Thordan/Intermission2.cs
        ("Mitigate NOW!", "지금 경감!"),
        ("Swap target!", "대상 변경!"),
        ("Separate adds!", "쫄 떼어놓기!"),
        ("Focus on {0}!", "{0} 집중!"),
        // BossMod.Modules/Endwalker/Variant/V03Aloalo/V031Quaqua.cs
        ("Away from orb!", "구슬에서 떨어지세요!"),
        ("Lead orb into statue laser!", "구슬을 석상 레이저로 유도!"),
        // BossMod.Modules/Endwalker/Variant/V03Aloalo/V034Statice.cs
        ("Reload: {0} (X = safe)", "재장전: {0} (X = 안전)"),
        ("Away from missile!", "미사일에서 떨어지세요!"),
        ("Away from surprising weapon!", "깜짝 병기에서 떨어지세요!"),
        // BossMod.Modules/Global/Crucible/Board2/VoidmancerPiece.cs
        ("Zombies hit: {0}", "맞힌 좀비: {0}"),
        // BossMod.Modules/Global/Crucible/Board3/GuttlerTheGutter.cs
        ("Bait away from pet!", "펫에게서 멀리 유도!"),
        ("GTFO from cleave!", "광역 공격에서 나가세요!"),
        // BossMod.Modules/Global/Crucible/Master1/GargoylePiece.cs
        ("Pick up orbs!", "구슬 줍기!"),
        // BossMod.Modules/Global/Crucible/Master1/StrixPiece.cs
        ("Move feather!", "깃털 옮기기!"),
        // BossMod.Modules/Global/Crucible/Master2/KingAhrimanPiece.cs
        ("Dismiss pet!", "펫 소환 해제!"),
        // BossMod.Modules/Global/Crucible/Master2/MindflayerPiece.cs
        ("GTFO from puddle!", "장판에서 나가세요!"),
        // BossMod.Modules/Global/Crucible/Master2/SphinxPiece.cs
        ("Stand on correct tile!", "올바른 타일 위에 서세요!"),
        // BossMod.Modules/Global/Crucible/ResistHelper.cs
        ("{0} immunity (permanent)", "{0} 면역 (영구)"),
        ("{0} immunity for {1}s", "{0} 면역 {1}초"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage01AllsWellThatStartsWell/Stage01.cs
        ("This stage is trivial.\nUse whatever skills you have to defeat these opponents.", "이 스테이지는 간단합니다.\n가지고 있는 아무 기술로나 적을 처치하세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage02MuchAdoAboutPudding/Stage02Act1.cs
        ("To beat this stage in a timely manner,\nyou should have at least one spell of each element.\n(Water, Fire, Ice, Lightning, Earth and Wind)", "이 스테이지를 빠르게 클리어하려면\n각 속성 마법을 최소 하나씩 갖추는 것이 좋습니다.\n(물, 불, 얼음, 번개, 땅, 바람)"),
        ("Pudding is weak to wind spells.\nMarshmallow is weak to ice spells.\nBavarois is weak to earth spells.", "푸딩은 바람 마법에 약합니다.\n마시멜로는 얼음 마법에 약합니다.\n바바루아는 땅 마법에 약합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage02MuchAdoAboutPudding/Stage02Act2.cs
        ("Gelato is weak to fire spells.\nFlan is weak to lightning spells.\nLicorice is weak to water spells.", "젤라토는 불 마법에 약합니다.\n플랑은 번개 마법에 약합니다.\n리코리스는 물 마법에 약합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage03WaitingForGolem/Stage03.cs
        ("Zipacna is weak against water based spells.\nFlying Sardine is recommended to interrupt raidwide.", "시파크나는 물 속성 마법에 약합니다.\n전체 공격을 끊으려면 날아라 정어리를 추천합니다."),
        ("Zipacna is weak against water based spells.\nEarth based spells are useless against Zipacna.", "시파크나는 물 속성 마법에 약합니다.\n땅 속성 마법은 시파크나에게 통하지 않습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage04GentlemanPreferSwords/Stage04Act1.cs
        ("Trivial act. Enemies here are weak to lightning and fire.\nIn Act 2 the Ram's Voice and Ultravibration combo can be useful.\nFlying Sardine for interrupts can be beneficial.", "간단한 막입니다. 이곳의 적은 번개와 불에 약합니다.\n2막에서는 빙결의 포효와 초진동 콤보가 유용합니다.\n시전 끊기용으로 날아라 정어리가 있으면 좋습니다."),
        ("Bats are weak to lightning.\nThe wolf is weak to fire.", "박쥐는 번개에 약합니다.\n늑대는 불에 약합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage04GentlemanPreferSwords/Stage04Act2.cs
        ("Kreios is weak to lightning spells.\nDuring the fight he will spawn 6 beetles.\nIf available use the Ram's Voice + Ultravibration combo for the instant kill.", "크레이오스는 번개 마법에 약합니다.\n전투 중 딱정벌레 6마리를 소환합니다.\n가능하면 빙결의 포효 + 초진동 콤보로 즉사시키세요."),
        ("Kreios is weak against lightning spells and can be frozen.", "크레이오스는 번개 마법에 약하며 빙결시킬 수 있습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage05TheThreepennyTurtles/Stage05.cs
        ("These turtles have very high defenses.\nBring 1000 Needles or Doom to defeat them.\nAlternatively you can remove their buff with Eerie Soundwave.", "이 거북들은 방어력이 매우 높습니다.\n바늘 1,000개나 죽음의 선고로 처치하세요.\n또는 괴음파로 버프를 해제할 수 있습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage06EyeSociety/Stage06Act1.cs
        ("Kill mandragoras last incase you need to get blinded again.", "다시 실명에 걸려야 할 수도 있으니 만드라고라는 마지막에 처치하세요."),
        ("Stack to get blinded!", "모여서 실명 걸리기!"),
        ("Get blinded by the Teary Twirl AOE from the mandragoras.\nBlindness makes you immune to all the gaze attacks.\nThe eyes in act 2 are weak to lightning damage.", "만드라고라의 양파즙 장판에 맞아 실명에 걸리세요.\n실명 상태에서는 모든 시선 공격에 면역입니다.\n2막의 눈알은 번개 피해에 약합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage06EyeSociety/Stage06Act2.cs
        ("The eyes are weak to lightning spells.", "눈알은 번개 마법에 약합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage07AChorusSlime/Stage07Act1.cs
        ("In slime explosion radius!", "슬라임 폭발 범위 안!"),
        ("For this stage the spells Sticky Tongue and Snort are recommended.\nUse them to pull or push Slimes close toIce Sprites.\nThen hit the slime from a distance with anything but fire spells to set of an explosion.", "이 스테이지에는 찐득한 혀와 콧김을 추천합니다.\n이를 이용해 슬라임을 얼음 정령 가까이 끌어오거나 밀어내세요.\n그런 다음 불 마법 외의 공격으로 멀리서 슬라임을 때려 폭발시키세요."),
        ("Hit the Lava Slime from a safe distance to win this act.", "안전한 거리에서 용암 슬라임을 때려 이 막을 클리어하세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage07AChorusSlime/Stage07Act2.cs
        ("Pull or push the Lava Slimes to the Ice Sprites and then hit the slimes\nfrom a distance to set of the explosions.", "용암 슬라임을 얼음 정령 쪽으로 끌어오거나 밀어낸 뒤\n멀리서 슬라임을 때려 폭발시키세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage07AChorusSlime/Stage07Act3.cs
        ("Pull or push the Lava Slimes to the towers and then hit the slimes\nfrom a distance to set off the explosions. The towers create a damage\npulse every 12s and a deadly explosion when they die. Take cover.", "용암 슬라임을 탑 쪽으로 끌어오거나 밀어낸 뒤\n멀리서 슬라임을 때려 폭발시키세요. 탑은 12초마다\n피해 파동을 일으키고 파괴되면 치명적인 폭발을 일으킵니다. 엄폐하세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage08BombedyOfErrors/Stage08Act1.cs
        ("For this stage the spell Flying Sardine to interrupt the Progenitrix in Act 2\nis highly recommended. Hit the Cherry Bomb from a safe distance\nwith anything but fire damage to set of a chain reaction to win this act.", "이 스테이지에는 2막에서 엄마 봄의 시전을 끊을 날아라 정어리를\n강력히 추천합니다. 안전한 거리에서 불 속성 외의 공격으로\n꼬마 봄을 때려 연쇄 폭발을 일으키면 이 막을 클리어합니다."),
        ("Hit the Cherry Bomb from a safe distance to win this act.", "안전한 거리에서 꼬마 봄을 때려 이 막을 클리어하세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage08BombedyOfErrors/Stage08Act2.cs
        ("Clever activation of cherry bombs will freeze the Progenitrix.\nInterrupt its burst skill or wipe. The Progenitrix is weak to wind spells.", "꼬마 봄을 잘 터뜨리면 엄마 봄을 빙결시킬 수 있습니다.\n폭발 기술을 끊지 못하면 전멸합니다. 엄마 봄은 바람 마법에 약합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage09ToKillAMockingslime/Stage09.cs
        ("Guimauve summons a total of 6 adds during the fight, one of each element.\nHealer mimikry can be helpful if you have trouble surviving.", "기모브는 전투 중 속성별로 하나씩 총 6마리의 쫄을 소환합니다.\n버티기 힘들다면 에테르 복사(힐러)가 도움이 됩니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage10ALittleKnightMusic/Stage10.cs
        ("Crom Dubh will cast King's Will during the fight. Interrupt it with\nFlying Sardine or he will become stronger each time. After 3 casts he\nstarts using the interruptible enrage Black Nebula.", "크롬 두브는 전투 중 왕의 유언을 시전합니다. 날아라 정어리로\n끊지 않으면 매번 강해집니다. 3회 시전 후에는\n끊을 수 있는 전멸기 암흑 성운을 사용합니다."),
        ("Requirement for achievement: Let Crom Dubh cast King's Will 3 times.", "업적 조건: 크롬 두브가 왕의 유언을 3회 시전하게 두기."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage11SomeLikeItExcruciatinglyHot/Stage11Act1.cs
        ("These bombs start self-destruction on combat start. Pull them together\nwith Sticky Tongue and attack them with anything to interrupt them.\nThey are weak against wind and strong against fire.", "이 봄들은 전투 시작 시 자폭을 시작합니다. 찐득한 혀로\n한데 모은 뒤 아무 공격으로나 때려 시전을 끊으세요.\n바람에 약하고 불에 강합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage11SomeLikeItExcruciatinglyHot/Stage11Act2.cs
        ("Same as last act, but this time there are 4 bombs. Pull them to the\nmiddle with Sticky Tongue and attack them with any AOE to keep them\ninterrupted. They are weak against wind and strong against fire.", "지난 막과 같지만 이번엔 봄이 4마리입니다. 찐득한 혀로\n중앙에 모은 뒤 광역 공격으로 계속 시전을 끊으세요.\n바람에 약하고 불에 강합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage12ThePlantomOfTheOpera/Stage12Act1.cs
        ("For this stage Ice Spikes and Bomb Toss are recommended spells.\nUse Ice Spikes to instantly kill roselets once they become aggressive.\nHydnora in act 2 is weak against water and strong against earth spells.", "이 스테이지에는 얼음 보호막과 폭탄 던지기를 추천합니다.\n작은 넝쿨장미가 공격적으로 변하면 얼음 보호막으로 즉시 처치하세요.\n2막의 히드노라는 물에 약하고 땅 마법에 강합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage12ThePlantomOfTheOpera/Stage12Act2.cs
        ("Use Bomb Toss to stun Hydnora when he casts Inflammable Fumes.\nUse Ice Spikes to instantly kill roselets once they become aggressive.\nHydnora is weak against water and strong against earth spells.", "히드노라가 가연성 가스를 시전하면 폭탄 던지기로 기절시키세요.\n작은 넝쿨장미가 공격적으로 변하면 얼음 보호막으로 즉시 처치하세요.\n히드노라는 물에 약하고 땅 마법에 강합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage13BeautyAndABeast/Stage13Act1.cs
        ("The first act is trivial, almost anything will work.\nFor act 2 having Flying Sardine is recommended.", "1막은 간단해서 거의 무엇이든 통합니다.\n2막에는 날아라 정어리를 추천합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage13BeautyAndABeast/Stage13Act2.cs
        ("Camilla will cast various AOEs and summons adds.\nInterrupt the adds with Flying Sardine and kill them fast.\nIf the add is still alive during the next Black Sabbath, you will be wiped.", "카밀라는 다양한 장판을 시전하고 쫄을 소환합니다.\n날아라 정어리로 쫄의 시전을 끊고 빠르게 처치하세요.\n다음 악마의 연회 때까지 쫄이 살아 있으면 전멸합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage14BlobsInTheWoods/Stage14Act1.cs
        ("Take cover behind a barricade!", "바리케이드 뒤에 숨으세요!"),
        ("These slimes start casting Final Song after death.\nWhile Final Song is not deadly, it does heavy damage and applies silence\nto you. Take cover! For act 2 the spell Loom is strongly recommended.\nThe slimes are strong against blunt melee damage such as J Kick.", "이 슬라임들은 죽은 뒤 마지막 노래를 시전합니다.\n마지막 노래는 치명적이진 않지만 큰 피해와 함께 침묵을\n부여합니다. 엄폐하세요! 2막에는 희미한 징조를 강력히 추천합니다.\n슬라임은 정의의 발차기 같은 타격 근접 공격에 강합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage14BlobsInTheWoods/Stage14Act2.cs
        ("Use the cube to take cover!", "큐브 뒤에 숨으세요!"),
        ("Same as first act, but the slimes will apply heavy to you.\nUse Loom to get out of line of sight as soon as Final Song gets casted.", "1막과 같지만 이번엔 슬라임이 과중력을 부여합니다.\n마지막 노래 시전이 시작되면 즉시 희미한 징조로 시야 밖으로 벗어나세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage15TheMeNobodyNodes/Stage15.cs
        ("For this stage Flying Sardine and Acorn Bomb are highly recommended.\nUse Flying Sardine to interrupt High Voltage.\nUse Acorn Bomb to put Shabtis to sleep until their buff runs out.", "이 스테이지에는 날아라 정어리와 도토리 폭탄을 강력히 추천합니다.\n날아라 정어리로 고압 전류를 끊으세요.\n도토리 폭탄으로 샤브티를 재워 버프가 끝날 때까지 기다리세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage16SunsetBullevard/Stage16Act1.cs
        ("The cyclops are very slow, but will instantly kill you, if they catch you.\nKite them or kill them with the self-destruct combo. (Toad Oil->Bristle->\nMoonflute->Swiftcast->Self-destruct) If you don't use the self-destruct\ncombo in act 1, you can bring the Final Sting combo for act 2.\n(Off-guard->Bristle->Moonflute->Final Sting)\nDiamondback is highly recommended in act 2.", "사이클롭스는 매우 느리지만 붙잡히면 즉사합니다.\n끌고 다니거나 자폭 콤보로 처치하세요. (두꺼비 기름->노발대발->\n달의 피리->신속한 마법->자폭) 1막에서 자폭 콤보를 쓰지\n않았다면 2막에 마지막 벌침 콤보를 챙길 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침)\n2막에는 초경화를 강력히 추천합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage16SunsetBullevard/Stage16Act2.cs
        ("Tikbalang will spawn a cyclops a few seconds into the fight. Make sure\nto kill it before it reaches you. After that you can just slowly take down the\nboss. Use Diamondback to survive the 1111 Tonze Swing. Alternatively\nyou can try the Final Sting combo when he drops to about 75% health.\n(Off-guard->Bristle->Moonflute->Final Sting)", "틱발랑은 전투 시작 몇 초 후 사이클롭스를 소환합니다. 사이클롭스가\n닿기 전에 반드시 처치하세요. 그 후에는 천천히 보스를\n잡으면 됩니다. 초경화로 1111톤즈 휘두르기를 버티세요. 또는\n보스 HP가 약 75%일 때 마지막 벌침 콤보를 시도할 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침)"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage17TheSwordOfMusic/Stage17Act1.cs
        ("{0} counters magical damage!", "{0}: 마법 공격에 반격!"),
        ("{0} counters physical damage!", "{0}: 물리 공격에 반격!"),
        ("The {0} counters magical attacks, the {1} counters physical\nattacks. If you have healing spells you can just tank the counter damage\nand kill them however you like anyway. All opponents in this stage are\nweak to lightning.\nThe Ram's Voice and Ultravibration combo can be used in Act 2.", "{0}은(는) 마법 공격에, {1}은(는) 물리 공격에\n반격합니다. 회복 마법이 있다면 반격 피해를 버티면서\n원하는 대로 처치해도 됩니다. 이 스테이지의 모든 적은\n번개에 약합니다.\n2막에서는 빙결의 포효와 초진동 콤보를 쓸 수 있습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage17TheSwordOfMusic/Stage17Act2.cs
        ("{0} is weak to lightning spells.\nDuring the fight he will spawn one of each claws as known from act 1.\nIf available use the Ram's Voice + Ultravibration combo for instant kill.", "{0}은(는) 번개 마법에 약합니다.\n전투 중 1막에 나온 손톱을 종류별로 하나씩 소환합니다.\n가능하면 빙결의 포효 + 초진동 콤보로 즉사시키세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage18MidsummerNightsExplosion/Stage18Act1.cs
        ("Aim charge at a keg!", "돌진을 나무통 쪽으로 유도!"),
        ("In keg explosion radius!", "나무통 폭발 범위 안!"),
        ("Make the manticores run to the kegs and their attacks will make them\nblow up. They take 2500 damage per keg explosion.\nThe Ram's Voice and Ultravibration combo can be used to kill manticores.", "만티코어가 나무통 쪽으로 달려가게 하면 그 공격에 통이\n폭발합니다. 통 폭발 1회당 2500 피해를 입습니다.\n빙결의 포효와 초진동 콤보로 만티코어를 처치할 수 있습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage18MidsummerNightsExplosion/Stage18Act2.cs
        ("Same as last stage. Make the manticores run to the kegs and their attacks\nwill make them blow up. Their attacks will also do friendly fire damage\nto each other.\nThe Ram's Voice and Ultravibration combo can be used to kill manticores.", "지난 스테이지와 같습니다. 만티코어가 나무통 쪽으로 달려가게 하면\n그 공격에 통이 폭발합니다. 만티코어의 공격은 서로에게도\n피해를 줍니다.\n빙결의 포효와 초진동 콤보로 만티코어를 처치할 수 있습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage19OnAClearDayYouCanSmellForever/Stage19Act1.cs
        ("Boss will reflect all magic damage!", "보스가 모든 마법 피해를 반사합니다!"),
        ("Boss reflects all magic damage!", "보스가 모든 마법 피해를 반사 중!"),
        ("At the start of the fight Rebekkah will cast Reflect. This will reflect all\nmagic damage back to you. Useful skills: Sharpened Knife,\nFlying Sardine, Ink Jet (Act 2), Exuviation (Act 2), potentially a Final Sting\ncombo. (Off-guard->Bristle->Moonflute->Final Sting)", "전투 시작 시 레베카가 리플렉트를 시전해 모든\n마법 피해를 되돌려 보냅니다. 유용한 기술: 칼갈이,\n날아라 정어리, 먹물 쏘기(2막), 허물 벗기(2막), 경우에 따라 마지막 벌침\n콤보. (무방비->노발대발->달의 피리->마지막 벌침)"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage19OnAClearDayYouCanSmellForever/Stage19Act2.cs
        ("Cast Ink Jet on boss to get blinded!", "보스에게 먹물 쏘기를 써서 실명 걸리기!"),
        ("Same as first act, but this time the boss will cast a gaze from all directions.\nThe easiest counter for this is to blind yourself by casting Ink Jet on the\nboss after it casted Schizocarps.\nThe Final Sting combo window opens at around 75% health.\n(Off-guard->Bristle->Moonflute->Final Sting)", "1막과 같지만 이번엔 보스가 모든 방향에서 시선 공격을 합니다.\n가장 쉬운 대처법은 보스가 씨뿌리기를 시전한 뒤\n보스에게 먹물 쏘기를 써서 스스로 실명에 걸리는 것입니다.\n마지막 벌침 콤보는 HP 약 75%부터 가능합니다.\n(무방비->노발대발->달의 피리->마지막 벌침)"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage20MissTyphon/Stage20Act1.cs
        ("Diamondback and Flying Sardine are essential for this stage. The Final\nSting combo (Off-guard->Bristle->Moonflute->Final Sting) can make act 3\nincluding the achievement much easier. Ultros in act 2 and 3 is weak to\nfire.", "이 스테이지에는 초경화와 날아라 정어리가 필수입니다. 마지막\n벌침 콤보(무방비->노발대발->달의 피리->마지막 벌침)로 3막과\n업적을 훨씬 쉽게 깰 수 있습니다. 2막과 3막의 오르트로스는\n불에 약합니다."),
        ("Requirement for achievement: Don't kill any tentacles in act 3", "업적 조건: 3막에서 문어발을 하나도 처치하지 않기"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage20MissTyphon/Stage20Act2.cs
        ("Ultros is weak to fire. Interrupt Imp Song.", "오르트로스는 불에 약합니다. 물요정의 노래를 끊으세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage20MissTyphon/Stage20Act3.cs
        ("This act is act 1+2 combined with tentacles on top.\nThe Final Sting combo (Off-guard->Bristle->Moonflute->Final Sting) makes\nthis act including the achievement much easier. Ultros is weak to fire.", "이 막은 1+2막에 문어발이 추가된 형태입니다.\n마지막 벌침 콤보(무방비->노발대발->달의 피리->마지막 벌침)로\n이 막과 업적을 훨씬 쉽게 깰 수 있습니다. 오르트로스는 불에 약합니다."),
        ("Requirement for achievement: Don't kill any tentacles in this act", "업적 조건: 이 막에서 문어발을 하나도 처치하지 않기"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage21ChimeraOnAHotTinRoof/Stage21Act1.cs
        ("The first act is fairly easy. Interrupt the Void Blizzards with Spitting\nSardine and most of the danger is gone. The Imps are weak against fire spells.\nIn the 2nd act you can start the Final Sting combination at about 50%\nhealth left. (Off-guard->Bristle->Moonflute->Final Sting)", "1막은 꽤 쉽습니다. 날아라 정어리로 보이드 블리자드를\n끊으면 위험은 거의 사라집니다. 임프는 불 마법에 약합니다.\n2막에서는 HP가 약 50% 남았을 때 마지막 벌침 콤보를\n시작할 수 있습니다. (무방비->노발대발->달의 피리->마지막 벌침)"),
        ("The imps are weak to fire spells and strong against ice.\nInterrupt Void Blizzard with Spitting Sardine.", "임프는 불 마법에 약하고 얼음에 강합니다.\n날아라 정어리로 보이드 블리자드를 끊으세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage21ChimeraOnAHotTinRoof/Stage21Act2.cs
        ("Interrupt The Rams Keeper with Spitting Sardine. You can start the\nFinal Sting combination at about 50% health left.\n(Off-guard->Bristle->Moonflute->Final Sting).\nThe boss will sometimes spawn an Arena Imp during the fight.", "날아라 정어리로 차디찬 시선을 끊으세요. HP가 약 50% 남았을 때\n마지막 벌침 콤보를 시작할 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침).\n보스는 전투 중 가끔 투기장 임프를 소환합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage22HereComesTheBoom/Stage22Act1.cs
        ("The first act is easy. Kill the grenades in one hit each or they will wipe you.\nIf you gear is bad consider using 1000 Needles.\nFor the 2nd act you should bring Sticky Tongue. In the 2nd act you can start\nthe Final Sting combination at about 50%\nhealth left. (Off-guard->Bristle->Moonflute->Final Sting)", "1막은 쉽습니다. 그레네이드는 각각 한 방에 처치하지 않으면 전멸합니다.\n장비가 약하다면 바늘 1,000개를 고려하세요.\n2막에는 찐득한 혀를 챙기세요. 2막에서는 HP가\n약 50% 남았을 때 마지막 벌침 콤보를\n시작할 수 있습니다. (무방비->노발대발->달의 피리->마지막 벌침)"),
        ("Kill the grenades in one hit each or they will wipe you. They got 543 HP.", "그레네이드는 각각 한 방에 처치하지 않으면 전멸합니다. HP는 543입니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage22HereComesTheBoom/Stage22Act2.cs
        ("{0} spawns grenades and gas bombs during the fight. Just as in\nact 1 the grenades must be killed in one hit each or they will wipe you.\nUse Sticky Tongue to pull Gas Bombs to the boss so they interrupt the enrage.\nYou can start the Final Sting combination at about 50% health left.\n(Off-guard->Bristle->Moonflute->Final Sting)", "{0}은(는) 전투 중 그레네이드와 가스 봄을 소환합니다. 1막과 마찬가지로\n그레네이드는 각각 한 방에 처치하지 않으면 전멸합니다.\n찐득한 혀로 가스 봄을 보스에게 끌어와 전멸기를 끊으세요.\nHP가 약 50% 남았을 때 마지막 벌침 콤보를 시작할 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침)"),
        ("Kill the {0} in one hit or it will wipe you. It got 543 HP.", "{0}을(를) 한 방에 처치하지 않으면 전멸합니다. HP는 543입니다."),
        ("Use Sticky Tongue to pull the {0} to the bos\nto interrupt the enrage!", "찐득한 혀로 {0}을(를) 보스에게 끌어와\n전멸기를 끊으세요!"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage23BehemothsAndBroomsticks/Stage23.cs
        ("The {0} will use Ecliptic Meteor.\nUse Diamondback to survive it.\nYou can start the Final Sting combination at about 40% health left.\n(Off-guard->Bristle->Moonflute->Final Sting)", "{0}이(가) 황도 메테오를 사용합니다.\n초경화로 버티세요.\nHP가 약 40% 남았을 때 마지막 벌침 콤보를 시작할 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침)"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage24AmazingTechnicolorPitFiends/Stage24Act1.cs
        ("{0} is immune to magical damage!", "{0}: 마법 피해 면역!"),
        ("{0} is immune to physical damage!", "{0}: 물리 피해 면역!"),
        ("The {0} is immune to magic, the {1} is immune to\nphysical attacks. For the 2nd act Diamondback is highly recommended.\nFor the 3rd act a ranged physical spell such as Fire Angon\nis highly recommended.", "{0}은(는) 마법에, {1}은(는) 물리 공격에\n면역입니다. 2막에는 초경화를 강력히 추천합니다.\n3막에는 화염 투창 같은 원거리 물리 기술을\n강력히 추천합니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage24AmazingTechnicolorPitFiends/Stage24Act2.cs
        ("The {0} casts Silence which should be interrupted.\nCondensed Libra puts a debuff on you. Use Diamondback to survive the\nfollowing attack. Alternatively you can cleanse the debuff with Exuviation.", "{0}이(가) 사일레스를 시전하니 끊어야 합니다.\n프티 라이브라는 디버프를 부여합니다. 초경화로 이어지는\n공격을 버티세요. 또는 허물 벗기로 디버프를 해제할 수 있습니다."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage24AmazingTechnicolorPitFiends/Stage24Act3.cs
        ("Kill {0} fast or wipe!\nUse ranged physical attacks.", "{0}을(를) 빨리 처치하지 않으면 전멸!\n원거리 물리 공격을 쓰세요."),
        ("{0} spawns two adds when casting Boneshaker. These should be a\npriority or they will explode and wipe you. To kill them without touching\nthe electric field use a ranged physical attack such as Fire Angon.\nYou can start the Final Sting combination at about 50% health left.\n(Off-guard->Bristle->Moonflute->Final Sting)", "{0}은(는) 뼈대 흔들기를 시전할 때 쫄 2마리를 소환합니다. 이들을\n우선 처치하지 않으면 폭발해 전멸합니다. 전기장에 닿지 않고\n처치하려면 화염 투창 같은 원거리 물리 공격을 쓰세요.\nHP가 약 50% 남았을 때 마지막 벌침 콤보를 시작할 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침)"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage25DirtyRottenAzulmagia/Stage25Act1.cs
        ("{0} will reflect all physical damage in act 1, all magic damage in act 2\nand switch between both in act 3. Loom, Exuviation and Diamondback\nare recommended. In act 3 can start the Final Sting combination\nat about 50% health left. (Off-guard->Bristle->Moonflute->Final Sting)", "{0}은(는) 1막에서 모든 물리 피해를, 2막에서 모든 마법 피해를 반사하고\n3막에서는 둘을 번갈아 반사합니다. 희미한 징조, 허물 벗기, 초경화를\n추천합니다. 3막에서는 HP가 약 50% 남았을 때 마지막 벌침 콤보를\n시작할 수 있습니다. (무방비->노발대발->달의 피리->마지막 벌침)"),
        ("Requirements for achievement: Take no damage, use all 6 magic elements,\nuse all 3 melee types and finish faster than ideal time", "업적 조건: 피해 받지 않기, 마법 6속성 모두 사용,\n근접 3종류 모두 사용, 목표 시간보다 빨리 클리어"),
        ("{0} will reflect all physical damage!", "{0}이(가) 모든 물리 피해를 반사합니다!"),
        ("You were doomed! Cleanse it with Exuviation or finish the act fast.", "죽음의 선고에 걸렸습니다! 허물 벗기로 해제하거나 빨리 막을 끝내세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage25DirtyRottenAzulmagia/Stage25Act2.cs
        ("In this act {0} will reflect all magic attacks.\nHe will also spawn adds that need to be dealed with swiftly\nsince they will spam raidwides. The adds are immune against magic\nand fire attacks.", "이 막에서 {0}은(는) 모든 마법 공격을 반사합니다.\n또한 쫄을 소환하는데, 전체 공격을 연발하므로\n빠르게 처리해야 합니다. 쫄은 마법과\n불 공격에 면역입니다."),
        ("Kill {0}! Use physical attacks except fire aspected.", "{0} 처치! 불 속성을 제외한 물리 공격을 쓰세요."),
        ("{0} will reflect all magic damage!", "{0}이(가) 모든 마법 피해를 반사합니다!"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage25DirtyRottenAzulmagia/Stage25Act3.cs
        ("Bait the Meteor to the edge of the arena!\nUse Loom to escape or Diamondback to survive.", "메테오를 전장 가장자리로 유도!\n희미한 징조로 탈출하거나 초경화로 버티세요."),
        ("In this act {0} will switch between magic and physical reflects.\nSpend attention to that so you don't accidently kill yourself.\nAs soon as he starts casting Web go to the edge to bait Meteor, then use Loom\nto escape. You can start the Final Sting combination at about 50% health left.\n(Off-guard->Bristle->Moonflute->Final Sting)", "이 막에서 {0}은(는) 마법 반사와 물리 반사를 번갈아 사용합니다.\n실수로 자멸하지 않도록 주의하세요.\n그물 시전이 시작되면 가장자리로 가서 메테오를 유도한 뒤 희미한 징조로\n탈출하세요. HP가 약 50% 남았을 때 마지막 벌침 콤보를 시작할 수 있습니다.\n(무방비->노발대발->달의 피리->마지막 벌침)"),
        // BossMod.Modules/Global/MaskedCarnivale/Stage26PapaMia/Stage26Act1.cs
        ("{0} will cast Alternate Plumage, which makes him almost\nimmune to damage. Use Eerie Soundwave to dispel it. Caber Toss must be\ninterrupted or you wipe.\nAdditionally Exuviation and earth spells are recommended for act 2.", "{0}은(는) 깃갈이를 시전해 피해를 거의 받지 않게\n됩니다. 괴음파로 해제하세요. 집어던지기는\n끊지 못하면 전멸합니다.\n추가로 2막에는 허물 벗기와 땅 마법을 추천합니다."),
        ("Dispel {0} with Eerie Soundwave!", "괴음파로 {0} 해제!"),
        ("Windburn on you! Cleanse it with Exuviation.", "열상 상태! 허물 벗기로 해제하세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage26PapaMia/Stage26Act2.cs
        ("{0} will cast Raw Instinct, which causes all his hits to crit.\nUse Eerie Soundwave to dispel it.\n{1} is weak against earth and strong against lightning attacks.", "{0}은(는) 야수의 직감을 시전해 모든 공격이 극대화됩니다.\n괴음파로 해제하세요.\n{1}은(는) 땅에 약하고 번개 공격에 강합니다."),
        ("Electrocution on you! Cleanse it with Exuviation.", "감전 상태! 허물 벗기로 해제하세요."),
        // BossMod.Modules/Global/MaskedCarnivale/Stage27LockUpYourSnorters/Stage27.cs
        ("{0} will spawn Bombs and Magitek Explosives throughout the fight.\nUse Snort to push away Bombs from Magitek Explosives and bait Fireballs\naway from the MEs. Meanwhile destroy the MEs asap because they will blow\nup on their own after about 35s. If any ME detonates you will be wiped.\nThe MEs are weak against water abilities and strong against fire attacks.", "{0}은(는) 전투 내내 봄과 마도 폭탄을 소환합니다.\n콧김으로 봄을 마도 폭탄에서 밀어내고 화염구를 마도 폭탄에서\n멀리 유도하세요. 그동안 마도 폭탄을 최대한 빨리 파괴하세요. 약 35초 후\n스스로 폭발하며, 하나라도 터지면 전멸합니다.\n마도 폭탄은 물 속성에 약하고 불 공격에 강합니다."),
        ("A {0} spawned, destroy it asap.", "{0} 등장, 최대한 빨리 파괴하세요."),
        ("Explosion in ca.: {0}s", "폭발까지 약 {0}초"),
        // BossMod.Modules/Global/Quest/FF15Collab/Garuda.cs
        ("Keep using duty action on the {0}s to stay out of the AOE!", "{0}에 임무 액션을 계속 사용해 장판을 피하세요!"),
        ("Use duty action to teleport to the {0} at the opposite side of Garuda!", "임무 액션으로 가루다 반대편의 {0}(으)로 순간이동하세요!"),
        // BossMod.Modules/Global/Quest/FF16Collab/InfernalShadow.cs
        ("Prepare to defend Clive!", "클라이브 방어 준비!"),
        ("Interact with {0} and solve a QTE!", "{0}에 상호작용해 QTE를 해결하세요!"),
        ("Prepare to solve a QTE!", "QTE 준비!"),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D100NybethObdilord.cs
        ("There is 3 sets of adds that spawn at HP %'s -> (90%, 65%, 40%) \nA resolution can make the adds permanently disappear once they are at 0% HP/the corpse are just laying on the floor.\nResolution is also does high damage to the adds + 0.3% to the Boss\nSolo tip: Either pop a resolution on all add packs, or pop lust -> resolution on 2nd ad pack. Make sure to keep regen up!", "HP (90%, 65%, 40%)마다 쫄 무리가 총 3번 등장합니다 \n쫄이 HP 0%가 되어 시체가 바닥에 누워 있을 때 쿠리부 변신을 쓰면 쫄이 영구히 사라집니다.\n쿠리부 변신은 쫄에게 큰 피해 + 보스에게도 0.3% 피해를 줍니다\n솔로 팁: 모든 쫄 무리에 쿠리부 변신을 쓰거나, 두 번째 쫄 무리에 서큐버스 변신 -> 쿠리부 변신을 쓰세요. 리제네를 계속 유지하세요!"),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D130Alfard.cs
        ("Bait the boss away from the middle of the arena. \n{0} will cast x2 Fire Puddles & x2 Ice Puddles. \nAfter the 4th puddle is dropped, run to the middle.", "보스를 전장 중앙에서 멀리 유도하세요. \n{0}이(가) 불 장판 2개와 얼음 장판 2개를 시전합니다. \n4번째 장판이 깔리면 중앙으로 달려가세요."),
        ("Run to the middle of the arena! \n{0} is about to cast a donut AOE!", "전장 중앙으로 달려가세요! \n{0}이(가) 곧 도넛 장판을 시전합니다!"),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D150Tisiphone.cs
        ("{0} will spawn 4 zombies, you can either kite them or kill them. The BloodRain raidwide will also kill them if they're still alive. \nThe boss will also life-steal however much HP is left of the Succubus, you're choice if you want to kill it or not.", "{0}은(는) 좀비 4마리를 소환합니다. 끌고 다니거나 처치하세요. 살아 있다면 핏빛 비 전체 공격에 함께 죽습니다. \n보스는 서큐버스의 남은 HP만큼 생명력을 흡수하니, 처치할지는 선택하세요."),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D170Yulunggu.cs
        ("Pull the boss out of the water puddle!", "보스를 물 장판 밖으로 끌어내세요!"),
        ("Consider provoking and pulling the boss out of the water puddle.", "도발해서 보스를 물 장판 밖으로 끌어내는 것을 고려하세요."),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D180Dendainsonne.cs
        ("Thunderbolt -> Charybdis x2 -> Thunderbolt -> Trounce from South Wall.", "벼락 -> 대소용돌이 x2 -> 벼락 -> 남쪽 벽에서 호령."),
        ("Charybdis x2 -> Thunderbolt -> Trounce from South Wall.", "대소용돌이 x2 -> 벼락 -> 남쪽 벽에서 호령."),
        ("Charybdis x1 -> Thunderbolt -> Trounce from South Wall!", "대소용돌이 x1 -> 벼락 -> 남쪽 벽에서 호령!"),
        ("Thunderbolt -> Trounce from South Wall!", "벼락 -> 남쪽 벽에서 호령!"),
        ("Boss is running to the South wall to cast Trounce!", "보스가 호령을 시전하러 남쪽 벽으로 이동 중!"),
        ("Charybdis x2 -> Thunderbolt -> Charybdis -> Trounce from North Wall.", "대소용돌이 x2 -> 벼락 -> 대소용돌이 -> 북쪽 벽에서 호령."),
        ("Charybdis x1 -> Thunderbolt -> Charybdis -> Trounce from North Wall.", "대소용돌이 x1 -> 벼락 -> 대소용돌이 -> 북쪽 벽에서 호령."),
        ("Thunderbolt -> Charybdis -> Trounce from North Wall!", "벼락 -> 대소용돌이 -> 북쪽 벽에서 호령!"),
        ("Charybdis -> Trounce from North Wall!", "대소용돌이 -> 북쪽 벽에서 호령!"),
        ("Boss is running to the North wall to cast Trounce!", "보스가 호령을 시전하러 북쪽 벽으로 이동 중!"),
        ("{0} will cast Trounce (Cone AOE) from the South and North wall. \nMake sure to stay near him to dodge the AOE. \n{1} will also cast Ecliptic Meteor at 15% HP, plan accordingly!", "{0}은(는) 남쪽과 북쪽 벽에서 호령(부채꼴 장판)을 시전합니다. \n보스 가까이 붙어서 장판을 피하세요. \n{1}은(는) HP 15%에서 황도 메테오도 시전하니 미리 대비하세요!"),
        ("Autorotation will not attack if boss HP is between 15 and 16% - press buttons manually when ready to start burst", "보스 HP가 15~16% 사이면 자동 로테이션이 공격하지 않습니다 - 버스트 준비가 되면 직접 기술을 사용하세요"),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D190TheGodfather.cs
        ("Remedy bomb is enraging in {0}s, kill it!", "치유의 봄 전멸기까지 {0}초, 처치하세요!"),
        ("Boss will cast a roomwide in {0}s, stun it with the Lavabomb!", "{0}초 후 보스가 전체 공격을 시전합니다, 용암 봄으로 기절시키세요!"),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D70Yaquaru.cs
        ("Pull boss out of puddle!", "보스를 장판 밖으로 끌어내세요!"),
        // BossMod.Modules/Heavensward/DeepDungeon/PalaceOfTheDead/D80Gudanna.cs
        ("Thunderbolt -> Charybdis -> Thunderbolt -> Trounce from East Wall", "벼락 -> 대소용돌이 -> 벼락 -> 동쪽 벽에서 호령"),
        ("Charybdis -> Thunderbolt -> Trounce from East Wall", "대소용돌이 -> 벼락 -> 동쪽 벽에서 호령"),
        ("Thunderbolt -> Trounce from East Wall", "벼락 -> 동쪽 벽에서 호령"),
        ("Trounce from East Wall", "동쪽 벽에서 호령"),
        ("Charybdis -> Thunderbolt -> Charybdis -> Trounce from West Wall.", "대소용돌이 -> 벼락 -> 대소용돌이 -> 서쪽 벽에서 호령."),
        ("Thunderbolt -> Charybdis -> Trounce from West Wall.", "벼락 -> 대소용돌이 -> 서쪽 벽에서 호령."),
        ("Charybdis -> Trounce from West Wall!", "대소용돌이 -> 서쪽 벽에서 호령!"),
        ("Trounce from West Wall!", "서쪽 벽에서 호령!"),
        ("{0} will cast Trounce (Cone AOE) from the East and West wall. \nMake sure to stay near him to dodge the AOE. \n{1} will also cast Ecliptic Meteor at 10% HP, plan accordingly!", "{0}은(는) 동쪽과 서쪽 벽에서 호령(부채꼴 장판)을 시전합니다. \n보스 가까이 붙어서 장판을 피하세요. \n{1}은(는) HP 10%에서 황도 메테오도 시전하니 미리 대비하세요!"),
        // BossMod.Modules/Heavensward/Dungeon/D03Aery/D031Rangda.cs
        ("Bait tether to a statue!", "선을 석상으로 유도!"),
        // BossMod.Modules/Heavensward/Dungeon/D05GreatGubalLibrary/D051DemonTome.cs
        ("Get Behind Boss!", "보스 뒤로!"),
        // BossMod.Modules/Heavensward/Dungeon/D05GreatGubalLibrary/D052Byblos.cs
        ("Pull orb into boss!", "구슬을 보스에게 끌어오기!"),
        // BossMod.Modules/Heavensward/Dungeon/D11Antitower/D113Calcabrina.cs
        ("Turn away from gaze!", "시선 등지세요!"),
        // BossMod.Modules/Heavensward/Dungeon/D13SohrKhai/D131ChieftainMoglin.cs
        ("Knock moogles out of raise!", "모그리 부활 시전 끊기!"),
        // BossMod.Modules/Heavensward/Dungeon/D13SohrKhai/D132Poqhiraj.cs
        ("GTFO from lightning!", "번개에서 나가세요!"),
        // BossMod.Modules/Heavensward/Dungeon/D15Xelphatol/D151NuzalHueloc.cs
        ("Stand under boss!", "보스 밑에 서세요!"),
        // BossMod.Modules/Heavensward/Dungeon/D17BaelsarsWall/D173TheGriffin.cs
        ("About to be knocked into danger!", "위험 지역으로 넉백될 예정!"),
        // BossMod.Modules/Heavensward/Trial/T01Ravana.cs
        ("Out → star → cross → stack center", "밖 → 별 → 십자 → 중앙 쉐어"),
        // BossMod.Modules/RealmReborn/Alliance/A11BoneDragon/A11BoneDragon.cs
        ("GTFO from poison!", "독에서 나가세요!"),
        // BossMod.Modules/RealmReborn/Alliance/A12Atomos/A12Atomos.cs
        ("Stand on pad!", "발판 위에 서세요!"),
        // BossMod.Modules/RealmReborn/Alliance/A15KingBehemoth/A15KingBehemoth.cs
        ("Drop comet outside boss!", "혜성을 보스 밖에 떨어뜨리세요!"),
        // BossMod.Modules/RealmReborn/Dungeon/D08Qarn/D081Teratotaur.cs
        ("Go to glowing platform!", "빛나는 발판으로!"),
        // BossMod.Modules/RealmReborn/Dungeon/D14Praetorium/D143Gaius.cs
        ("Enrage in {0}s", "전멸기까지 {0}초"),
        // BossMod.Modules/RealmReborn/Extreme/Ex2Garuda/SpinyShield.cs
        ("Go inside shield", "보호막 안으로"),
        // BossMod.Modules/RealmReborn/Extreme/Ex2Garuda/WickedWheel.cs
        ("Wicked wheel in ~{0}s", "마녀의 수레바퀴까지 ~{0}초"),
        // BossMod.Modules/RealmReborn/Extreme/Ex3Titan/Geocrush.cs
        ("Move closer to center!", "중앙 가까이 이동!"),
        ("Move closer to the edge!", "가장자리 가까이 이동!"),
        // BossMod.Modules/RealmReborn/Raid/T01Caduceus/T01Caduceus.cs
        ("Next cleave in ~{0}s", "다음 광역 공격까지 ~{0}초"),
        ("Clone HP: {0}{1}%, distance: {2}, check in {3}s", "클론 HP: {0}{1}%, 거리: {2}, 확인까지 {3}초"),
        // BossMod.Modules/RealmReborn/Raid/T02MultiADS/AllaganRot.cs
        ("Rot: {0}s", "알라그 부패: {0}초"),
        // BossMod.Modules/RealmReborn/Raid/T05Twintania/Common.cs
        ("Next death sentence in ~{0}s", "다음 사형 선고까지 ~{0}초"),
        // BossMod.Modules/RealmReborn/Raid/T05Twintania/Phase2.cs
        ("Next fireball in ~{0}s", "다음 화염구까지 ~{0}초"),
        ("Next conflagrate in ~{0}s", "다음 불보라까지 ~{0}초"),
        // BossMod.Modules/RealmReborn/Raid/T05Twintania/Phase5.cs
        ("Go to neurolink!", "구속장치로!"),
        // BossMod.Modules/RealmReborn/Trial/T01IfritN/T01IfritN.cs
        ("Nail enrage in: {0}s", "말뚝 전멸기까지: {0}초"),
        // BossMod.Modules/RealmReborn/Trial/T02TitanN/T02TitanN.cs
        ("Heart enrage in: {0}s", "심장석 전멸기까지: {0}초"),
        // BossMod.Modules/RealmReborn/Trial/T04PortaDecumana/T04PortaDecumana2.cs
        ("Prepare to soak the orbs!", "구슬 밟기 준비!"),
        ("Soak the orbs!", "구슬 밟기!"),
        // BossMod.Modules/RealmReborn/Trial/T09WhorleaterH/T09WhorleaterHHints.cs
        ("Activate the {0} or wipe!", "{0}을(를) 작동하지 않으면 전멸!"),
        ("Kill Sahagins or lose control!", "사하긴을 처치하지 않으면 조작 불능!"),
        ("Destroy the spumes!", "물거품 파괴!"),
        ("Attack the head! (Attacking the tail will reflect damage onto you)", "머리 공격! (꼬리를 공격하면 피해가 반사됨)"),
        ("Attack the tail! (Attacking the head will reflect damage onto you)", "꼬리 공격! (머리를 공격하면 피해가 반사됨)"),
        // BossMod.Modules/Shadowbringers/Alliance/A12Hobbes/Impact.cs
        ("Take tethers from party!", "파티원의 선 가져오기!"),
        // BossMod.Modules/Shadowbringers/Alliance/A32HanselAndGretel/A32HanselAndGretel.cs
        ("Separate bosses!", "보스 떼어놓기!"),
        // BossMod.Modules/Shadowbringers/Alliance/A34RedGirl/ChildsPlay.cs
        ("Aim for safe spot!", "안전 지대로 조준!"),
        // BossMod.Modules/Shadowbringers/Alliance/A36FalseIdol/Distortion.cs
        ("Face away from ring!", "고리 등지세요!"),
        ("Face towards ring!", "고리 바라보세요!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D01Holminster/D013Philia.cs
        ("{0} is about to be chained!", "{0}이(가) 곧 사슬에 묶입니다!"),
        ("Destroy chains on {0}!", "{0}의 사슬 파괴!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D02DohnMheg/D022Griaule.cs
        ("Grab a tether!", "선 가져오기!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D03QitanaRavel/D033Eros.cs
        ("Bait voidzone away!", "장판을 멀리 유도!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D06Amaurot/D061TheFirstBeast.cs
        ("Place meteor!", "메테오 배치!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D09GrandCosmos/D092LeannanSith.cs
        ("Move seeds!", "씨앗 옮기기!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D09GrandCosmos/D093Lugus.cs
        ("Bait away from furniture!", "가구에서 멀리 유도!"),
        ("Pass flame to furniture!", "불꽃을 가구로 넘기기!"),
        ("RIP", "사망"),
        ("GTFO from charge!", "돌진에서 나가세요!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D10AnamnesisAnyder/D103RukshsDheem.cs
        ("Block drains!", "흡수 막기!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D11HeroesGauntlet/D113SpectralBerserker.cs
        ("Stack with rock!", "바위와 쉐어!"),
        ("Hide from AOE!", "장판을 피해 숨으세요!"),
        ("GTFO from voidzone!", "장판에서 나가세요!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D12MatoyasRelict/D121Mudman.cs
        ("Bait into a hole!", "구멍으로 유도!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D12MatoyasRelict/D122Nixie.cs
        ("Go to geyser!", "간헐천으로!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D13Paglthan/D131Amhuluk.cs
        ("Pass debuff!", "디버프 넘기기!"),
        // BossMod.Modules/Shadowbringers/Dungeon/D13Paglthan/D132MagitekCore.cs
        ("Go to launchpad!", "발사대로!"),
        // BossMod.Modules/Shadowbringers/FATE/Formidable.cs
        ("Extreme Caution on you! STOP everything or get launched into the air!", "행동 주의 상태! 모든 행동을 멈추지 않으면 공중으로 날아갑니다!"),
        ("Stop everything including auto attacks or get launched into the air", "자동 공격 포함 모든 행동을 멈추지 않으면 공중으로 날아갑니다"),
        // BossMod.Modules/Shadowbringers/Foray/CriticalEngagement/CE10PatriotGames.cs
        ("Detonate mines! {0}s remaining", "지뢰 폭파! {0}초 남음"),
        // BossMod.Modules/Shadowbringers/Foray/CriticalEngagement/CE20FromBeyondTheGrave.cs
        ("Apply doom & march debuffs", "죽음의 선고 & 강제 이동 디버프 부여"),
        // BossMod.Modules/Shadowbringers/Foray/CriticalEngagement/CE22HereComesTheCavalry.cs
        ("Go further away from boss!", "보스에게서 더 멀리!"),
        ("Proximity Tankbuster + Charge", "거리 감쇠 탱버 + 돌진"),
        // BossMod.Modules/Shadowbringers/Foray/DelubrumReginae/DRS2StygimolochWarrior/Entrapment.cs
        ("Matching patterns: {0}", "일치하는 패턴: {0}"),
        // BossMod.Modules/Shadowbringers/Foray/DelubrumReginae/DRS3Dahu/SpitFlame.cs
        ("Hit at least one add!", "쫄을 최소 하나 맞히세요!"),
        // BossMod.Modules/Shadowbringers/Foray/Duel/Duel5Menenius/BlueHiddenMines.cs
        ("Soak the mine!", "지뢰 밟기!"),
        // BossMod.Modules/Shadowbringers/Foray/Duel/Duel5Menenius/GunberdShot.cs
        ("Maintain Distance", "거리 유지"),
        ("Knockback", "넉백"),
        ("Dark Loaded", "지근탄 장전됨"),
        ("Windslicer Loaded", "충격탄 장전됨"),
        // BossMod.Modules/Shadowbringers/Foray/Duel/Duel6Lyon/Duel6LyonGenericAttacks.cs
        ("Applies On Fire to Lyon. Use Dispell to remove it", "라이언에게 '불도끼'를 부여합니다. 로스트 디스펠로 해제하세요"),
        ("Lyon has 'On Fire'. Use Dispell to remove it!", "라이언에게 '불도끼' 상태. 로스트 디스펠로 해제하세요!"),
        // BossMod.Modules/Shadowbringers/Foray/Duel/Duel6Lyon/TasteOfBloodAndDuelOrDie.cs
        ("Get hit by {0} Duel or Die Taste Of Blood", "결투 지목 대상 {0}명이 귀곡혈산참에 맞아야 함"),
        // BossMod.Modules/Shadowbringers/Hunt/RankA/Grassman.cs
        ("Raidwide x5", "전체 공격 x5"),
        // BossMod.Modules/Shadowbringers/Hunt/RankA/Maliktender.cs
        ("Getting hit by the needle attack will instantly kill you from now on!", "이제부터 바늘 공격에 맞으면 즉사합니다!"),
        // BossMod.Modules/Shadowbringers/Hunt/RankA/Supay.cs
        ("Bait away + look away!", "멀리 유도 + 시선 피하기!"),
        // BossMod.Modules/Shadowbringers/Hunt/RankA/TheMudman.cs
        ("Bait away or interrupt!", "멀리 유도하거나 시전 끊기!"),
        // BossMod.Modules/Shadowbringers/Hunt/RankS/Ixtab.cs
        ("You were doomed! Get healed to full fast.", "죽음의 선고에 걸렸습니다! 빨리 HP를 가득 회복하세요."),
        ("Heal yourself to full! (Doom).", "HP를 가득 회복하세요! (죽음의 선고)."),
        ("One or more players are affected by doom. Heal them to full.", "죽음의 선고에 걸린 플레이어가 있습니다. HP를 가득 회복시키세요."),
        // BossMod.Modules/Shadowbringers/Quest/ATearfulReunion.cs
        ("Pull lightning orb into black hole!", "번개 구슬을 블랙홀로 끌어오기!"),
        // BossMod.Modules/Shadowbringers/TreasureHunt/TheShiftingOubliettesOfLyheGhiah/SecretWorm.cs
        ("Bait bubble away!", "거품을 멀리 유도!"),
        // BossMod.Modules/Shadowbringers/Trial/T01Titania.cs
        ("Stand in puddles to get fire resist.", "장판 위에 서서 불 저항을 얻으세요."),
        // BossMod.Modules/Stormblood/Dungeon/D04DomaCastle/D043HypertunedGrynewaht.cs
        ("Bait marker away!", "표식을 멀리 유도!"),
        // BossMod.Modules/Stormblood/Dungeon/D13TheBurn/D131Hedetet.cs
        ("Hide behind obstacle!", "장애물 뒤에 숨으세요!"),
        // BossMod.Modules/Stormblood/Extreme/Ex9Seiryu.cs
        ("Return to the island!", "섬으로 돌아가세요!"),
        // BossMod.Modules/Stormblood/Foray/BaldesionArsenal/AbsoluteVirtue.cs
        ("Go to opposite color tower!", "반대 색 탑으로!"),
        // BossMod.Modules/Stormblood/Foray/BaldesionArsenal/ProtoOzma.cs
        ("Currently: star autos", "현재: 별 자동 공격"),
        ("Stand on a button!", "버튼 위에 서세요!"),
        // BossMod.Modules/Stormblood/Quest/TortoiseInTime.cs
        ("GTFO from Tataru!", "타타루에게서 떨어지세요!"),
        // BossMod.Modules/Stormblood/TreasureHunt/TheShiftingAltarsOfUznair/AltarDiresaur.cs
        ("Bait voidzone away! (3 times)", "장판을 멀리 유도! (3회)"),
        // BossMod.Modules/Stormblood/Trial/T01Susano.cs
        ("Soak lightning orbs!", "번개 구슬 밟기!"),
        // BossMod.Modules/Stormblood/Trial/T03Shinryu.cs
        ("Avoid puddles during lightning bolts.", "낙뢰 중에는 장판을 피하세요."),
        ("Stand in puddles during hellfire.", "지옥불 중에는 장판 위에 서세요."),
        // BossMod.Modules/Stormblood/Trial/T04Tsukuyomi.cs
        ("Swap sides to drop moon stacks!", "반대편으로 가서 달 중첩 해제!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P1CyclonicBreak.cs
        ("Prepare to stack", "쉐어 준비"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P1FallOfFaith.cs
        ("Help group {0}", "{0}그룹 지원"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P1PowderMarkTrail.cs
        ("Get closer to co-tank!", "다른 탱커에게 붙으세요!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P2DiamondDust.cs
        ("Stay farther away!", "더 멀리 떨어지세요!"),
        ("Stay closer to bait!", "유도 대상자에게 붙으세요!"),
        ("Bait cone away from raid!", "부채꼴을 공격대 밖으로 유도!"),
        ("GTFO from baited cone!", "유도된 부채꼴에서 나가세요!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P3Apocalypse.cs
        ("Group: {0}", "그룹: {0}"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P4CrystallizeTime.cs
        ("Position in group properly!", "그룹 내 위치를 맞추세요!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P4DarklitDragonsong.cs
        ("Water in {0} tower", "{0} 탑에 물"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P4MornAfah.cs
        ("Usurper HP: {0}{1}%", "시바 미트론 HP: {0}{1}%"),
        // BossMod.Ultimate/Dawntrail/Ultimate/FRU/P5PolarizingStrikes.cs
        ("Hide behind party!", "파티 뒤에 숨으세요!"),
        ("Stay in group!", "그룹 안에 있으세요!"),
        ("Go to correct group!", "올바른 그룹으로!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P1Gravitas.cs
        ("Prepare to spread!", "산개 준비!"),
        ("Bait away from puddles!", "장판에서 멀리 유도!"),
        ("Soak puddles!", "장판 밟기!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P1TelePortent.cs
        ("{0} => {1}", "{0} => {1}"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P2Forsaken.cs
        ("Current shape: {0}", "현재 모양: {0}"),
        ("Partner shape: {0}", "파트너 모양: {0}"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P3BlackHole.cs
        ("Accretions: {0}", "Accretion 대상: {0}"),
        ("Prepare to take tether!", "선 가져올 준비!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P3BowelsOfAgony.cs
        ("GTFO from crystal bait!", "크리스탈 유도에서 나가세요!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P3UltimaBlaster.cs
        ("Get away from clone!", "분신에게서 떨어지세요!"),
        // BossMod.Ultimate/Dawntrail/Ultimate/UMAD/P4KefkaSays.cs
        ("Gazes 1: ", "시선 1: "),
        ("Fire: ", "불: "),
        ("Gazes 2: ", "시선 2: "),
        ("Water: ", "물: "),
        ("Prepare to stack!", "쉐어 준비!"),
        ("You have gaze, be careful!", "시선 대상입니다, 조심하세요!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW1/Heavensflame.cs
        ("Cancel knockback immunity!", "넉백 방지 해제!"),
        ("Aim to break tether!", "선이 끊어지게 조준!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW1/HyperdimensionalSlash.cs
        ("Aim away from other tears!", "다른 틈새 반대쪽으로 조준!"),
        ("Stay closer to center!", "중앙 가까이 있으세요!"),
        ("GTFO from laser aoe!", "레이저 장판에서 나가세요!"),
        ("GTFO from cone aoe!", "부채꼴 장판에서 나가세요!"),
        ("Stack with others!", "다른 사람과 쉐어!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW1/ShiningBlade.cs
        ("About to be knocked into tear!", "틈새로 넉백될 예정!"),
        ("Aim away from boss!", "보스 반대쪽으로 조준!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P2SanctityOfTheWard1.cs
        ("Swap: {0}", "교대: {0}"),
        ("Move: {0} {1}", "이동: {0} {1}"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P2SanctityOfTheWard2.cs
        ("Prey distance: {0}deg", "표식 거리: {0}도"),
        ("Prey: {0}, swap {1}, prey towers {2}", "표식: {0}, 교대 {1}, 표식 탑 {2}"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P2StrengthOfTheWard2.cs
        ("Grab tether!", "선 가져오기!"),
        ("Move away from raid!", "공격대에서 떨어지세요!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P3DiveFromGrace.cs
        ("Spot: {0}", "위치: {0}"),
        ("Arrows for: {0}", "화살표: {0}"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P4Hatebound.cs
        ("Color: {0}", "색: {0}"),
        ("GTFO from baiters!", "유도 대상자에게서 떨어지세요!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P5DeathOfTheHeavens.cs
        ("Doom", "죽음의 선고"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P5WrathOfTheHeavens.cs
        ("Prepare for lightning!", "번개 대비!"),
        ("Prepare for divebomb!", "급강하 대비!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P6AkhAfah.cs
        ("Nidhogg HP: {0}{1}%", "니드호그 HP: {0}{1}%"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P6HallowedWings.cs
        ("Tankbuster {0}", "탱버 {0}"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P6Touchdown.cs
        ("GTFO from wrong side!", "잘못된 쪽에서 나가세요!"),
        // BossMod.Ultimate/Endwalker/Ultimate/DSW2/P6Wyrmsbreath.cs
        ("GTFO from baits!", "유도 대상자에게서 떨어지세요!"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P1HandOfPain.cs
        ("Hand HP: {0}{1}%", "손 HP: {0}{1}%"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P1JagdDolls.cs
        ("GTFO from exhaust intersection", "배기 장판 교차 지점에서 나가세요"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P2Flarethrower.cs
        ("Aim towards tornado!", "토네이도 쪽으로 조준!"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P2Nisi.cs
        ("Pass nisi!", "임시 판결 넘기기!"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P3Inception1.cs
        ("GTFO from other tethers!", "다른 선에서 떨어지세요!"),
        ("GTFO from other crystals!", "다른 크리스탈에서 떨어지세요!"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P3Inception3.cs
        ("Stay farther from partner!", "파트너와 더 멀리!"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P3TemporalStasis.cs
        ("Stay closer to partner!", "파트너와 더 가까이!"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P4FateCalibrationAlpha.cs
        ("Movement: {0} -> {1}", "이동: {0} -> {1}"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P4FateCalibrationBeta.cs
        ("{0} after jumps", "점프 후 {0}"),
        // BossMod.Ultimate/Shadowbringers/Ultimate/TEA/P4ForcedMarchDebuffs.cs
        ("Move away from light beacon!", "빛 표지에서 떨어지세요!"),
        ("Aim away from wall!", "벽 반대쪽으로 조준!"),
        ("Move closer to dark beacon!", "어둠 표지에 가까이!"),
        // BossMod.Ultimate/Stormblood/Ultimate/UCOB/Hatch.cs
        ("GTFO from neurolink!", "구속장치에서 나가세요!"),
        // BossMod.Ultimate/Stormblood/Ultimate/UCOB/P2BahamutsFavor.cs
        ("Doom {0}", "죽음의 선고 {0}"),
        // BossMod.Ultimate/Stormblood/Ultimate/UCOB/P2Cauterize.cs
        ("Bait {0}", "유도 {0}"),
        // BossMod.Ultimate/Stormblood/Ultimate/UCOB/P3GrandOctet.cs
        ("Move {0}", "이동 {0}"),
        // BossMod.Ultimate/Stormblood/Ultimate/UCOB/P3QuickmarchTrio.cs
        ("Hit by tankbuster!", "탱버에 맞음!"),
        // BossMod.Ultimate/Stormblood/Ultimate/UWU/P1MistralSong.cs
        ("Intercept charge!", "돌진 가로막기!"),
        // BossMod.Ultimate/Stormblood/Ultimate/UWU/P3Gaols.cs
        ("Gaols: {0}", "감옥: {0}"),
        // BossMod/Components/CastCounter.cs
        ("Casts of {0}:\n{1}\nTotal casts: {2}", "{0} 시전:\n{1}\n총 시전 횟수: {2}"),
        // BossMod/Components/DirectionalParry.cs
        ("Attack target from unshielded side!", "방패 없는 쪽에서 공격!"),
        // BossMod/Components/Dispel.cs
        ("Dispel {0}!", "{0} 해제!"),
        ("Dispel!", "해제!"),
        // BossMod/Components/Protean.cs
        ("GTFO from protean!", "8방향 장판에서 나가세요!"),
        // BossMod/Components/SharedTankbuster.cs
        ("Stack with other tanks!", "다른 탱커와 쉐어!"),
        ("Stack with tank!", "탱커와 쉐어!"),
        // BossMod/Components/StackTogether.cs
        ("Stack with other targets!", "다른 대상자와 쉐어!"),
        // BossMod/Components/StayMove.cs
        ("Do nothing!", "아무것도 하지 마세요!"),
        ("Stop moving!", "이동 멈추기!"),
        // BossMod/Components/TankbusterTether.cs
        ("Hit by tankbuster", "탱버에 맞음"),
        // BossMod/Components/WildCharge.cs
        ("Stay inside charge!", "돌진 범위 안에 있으세요!"),
        ("Stay in single charge!", "돌진 하나에만 맞으세요!"),
        ("GTFO from forbidden stack!", "금지된 쉐어에서 나가세요!"),
        ("GTFO from other stacks!", "다른 쉐어에서 나가세요!"),
        // BossMod/Modules/DemoModule.cs
        ("Hint", "힌트"),
        ("Risk", "위험"),
    ];
}
