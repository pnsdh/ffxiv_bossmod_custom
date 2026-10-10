namespace BossMod;

// Korean translations: options added by this fork (e.g. 09STOP strat support); listed last so they override earlier tables
static partial class LocKorean
{
    private static readonly (string, string)[] Custom =
    [
        // BossMod.Ultimate/Endwalker/Ultimate/TOP/TOPConfig.cs
        ("09STOP (global): H1>T1>T2>D1>D2>D3>D4>H2", "09STOP (전체 우선순위): H1>T1>T2>D1>D2>D3>D4>H2"),
        ("P1 Pantokrator: group positions if first flames are N/S", "P1 전지전능: 첫 화염이 남북일 때 그룹 위치"),
        ("G1 east, G2 west", "G1 동쪽, G2 서쪽"),
        ("G1 west, G2 east (09STOP)", "G1 서쪽, G2 동쪽 (09STOP)"),
        ("P2 Party Synergy: marker order from the eye (for G2 with Remote Glitch, reversed according to the option below)", "P2 연계 프로그램[파티]: 눈에서부터 마커 순서 (결함: 원거리일 때 G2는 아래 옵션에 따라 뒤집힘)"),
        ("Cross, square, circle, triangle", "엑스, 네모, 동그라미, 세모"),
        ("Circle, triangle, square, cross (09STOP)", "동그라미, 세모, 네모, 엑스 (09STOP)"),
        ("GOPB (reverse order) (LPDU)", "GOPB (역순) (LPDU, 09STOP)"),
        ("Northernmost pair (NA)", "눈에 가까운 쪽 짝 (NA, 09STOP)"),
        ("Southernmost pair (LPDU)", "눈에서 먼 쪽 짝 (LPDU)"),

        ("09STOP (HTTDDDDH)", "09STOP (HTTDDDDH)"),
        ("09STOP (HHTTDDDD)", "09STOP (HHTTDDDD)"),
        ("09STOP (TRHM)", "09STOP (TRHM)"),
        ("P5 Delta: layout", "P5 델타: 배치"),
        ("LPDU", "LPDU"),
        ("09STOP", "09STOP"),
        ("P3 Monitors: layout", "P3 모니터: 배치"),
        ("Monitors on the boss monitor side (LPDU/NA, see the option below)", "모니터 대상이 보스 모니터 쪽 (LPDU/NA, 아래 옵션 참고)"),
        ("09STOP: monitors on the opposite side, M1 away from the boss / M2 north / M3 south", "09STOP: 모니터 대상이 반대편, 모니터1은 보스 반대쪽 / 모니터2는 북 / 모니터3은 남으로 유도"),
        ("P3 Monitors: position for safe side monitor (non-09STOP layout)", "P3 모니터: 안전한 쪽 모니터 위치 (09STOP이 아닌 배치)"),

        // BossMod.Ultimate/Endwalker/Ultimate/TOP/TOPConfig.09STOP.cs
        ("Apply 09STOP (Korean strat) to all TOP options", "TOP 전체를 09STOP(한국 공략)으로 설정"),
        ("Sets every TOP option that has a 09STOP choice. Options without one are left unchanged.", "09STOP 선택지가 있는 TOP 옵션을 모두 09STOP 값으로 바꿉니다. 09STOP 선택지가 없는 옵션은 그대로 둡니다."),
    ];
}
