namespace BossMod;

// Korean translations: options added by this fork (e.g. 09STOP strat support); listed last so they override earlier tables
static partial class LocKorean
{
    private static readonly (string, string)[] Custom =
    [
        // BossMod.Ultimate/Endwalker/Ultimate/TOP/TOPConfig.cs
        ("09STOP (global): H1>T1>T2>D1>D2>D3>D4>H2", "09STOP (전체 우선순위): H1>T1>T2>D1>D2>D3>D4>H2"),
        ("P1 Pantokrator: group positions if first flames are N/S", "P1 전지전능: 첫 화염이 남북일 때 그룹 위치"),
        ("G1 east, G2 west (LPDU)", "G1 동쪽, G2 서쪽 (LPDU)"),
        ("G1 west, G2 east (09STOP)", "G1 서쪽, G2 동쪽 (09STOP)"),
        ("P2 Party Synergy: marker order from the eye (for G2 with Remote Glitch, reversed according to the option below)", "P2 연계 프로그램[파티]: 눈에서부터 마커 순서 (결함: 원거리일 때 G2는 아래 옵션에 따라 뒤집힘)"),
        ("Cross, square, circle, triangle (LPDU, NA)", "엑스, 네모, 동그라미, 세모 (LPDU, NA)"),
        ("Circle, triangle, square, cross (09STOP)", "동그라미, 세모, 네모, 엑스 (09STOP)"),
        ("GPOB (only B and G swap) (NA)", "엑스와 세모만 교체 (NA)"),
        ("GOPB (reverse order) (LPDU)", "전체 역순 (LPDU, 09STOP)"),
        ("Northernmost pair (NA)", "눈에 가까운 짝 (NA, 09STOP)"),
        ("Southernmost pair (LPDU)", "눈에서 먼 짝 (LPDU)"),
        ("North/South", "북/남 (LPDU, 09STOP)"),
        ("NE/SW", "북동/남서"),
        ("Stacks S, spreads N", "쉐어 남, 산개 북"),
        ("Stacks N, spreads S", "쉐어 북, 산개 남 (LPDU, 09STOP)"),

        ("09STOP (HTTDDDDH)", "09STOP (HTTDDDDH)"),
        ("09STOP (HHTTDDDD)", "09STOP (HHTTDDDD)"),
        ("09STOP (TRHM)", "09STOP (TRHM)"),
        ("P5 Delta: layout", "P5 델타: 배치"),
        ("P5 Sigma: layout", "P5 시그마: 배치"),
        ("Monitors (P3, P5 Delta): automatically face correct direction before mechanic resolves", "모니터 (P3, P5 델타): 기믹 처리 전 자동으로 올바른 방향 바라보기"),
        ("P5: show candidate spots for players whose exact spot isn't known yet (3 or more spots)", "P5: 정확한 자리를 알 수 없는 사람에게 후보 자리 표시 (3곳 이상)"),
        ("E.g. Hello World baiters, whose roles depend on head markers placed by the party. Up to 2 spots are always shown.", "예: 헬로월드 유도자처럼 파티가 찍는 머리 징에 따라 역할이 정해지는 경우입니다. 2곳 이하는 항상 표시합니다."),
        ("09STOP: line up between the arms (circle, triangle, square, cross from left), left double pair N/S, right double pair W/E, single pairs at their fist.\nTower and Hello World spots are not shown for 09STOP yet.", "09STOP: 아카이브 암 사이에 왼쪽부터 동그라미, 세모, 네모, 엑스로 정렬하고, 징 2개 왼쪽 조는 12시/6시, 오른쪽 조는 9시/3시, 징 1개 조는 각자 주먹 쪽으로 산개합니다.\n탑과 헬로월드 자리는 09STOP에서 아직 표시하지 않습니다."),
        ("LPDU", "LPDU"),
        ("09STOP", "09STOP"),
        ("P3 Monitors: layout", "P3 모니터: 배치"),
        ("Monitors on the boss monitor side, safe side monitor north (LPDU)", "모니터 대상이 보스 모니터 쪽, 안전한 쪽 모니터 북쪽 (LPDU)"),
        ("Monitors on the boss monitor side, safe side monitor south (Aether)", "모니터 대상이 보스 모니터 쪽, 안전한 쪽 모니터 남쪽 (Aether)"),
        ("Monitors opposite the boss monitor, facing E/W, N, S (09STOP)", "모니터 대상이 보스 모니터 반대편, 동서/북/남으로 유도 (09STOP)"),

        // BossMod.Ultimate/Endwalker/Ultimate/TOP/TOPConfig.09STOP.cs
        ("Apply 09STOP (Korean strat) to all TOP options", "TOP 전체를 09STOP(한국 공략)으로 설정"),
        ("Sets every TOP option that has a 09STOP choice. Options without one are left unchanged.", "09STOP 선택지가 있는 TOP 옵션을 모두 09STOP 값으로 바꿉니다. 09STOP 선택지가 없는 옵션은 그대로 둡니다."),
    ];
}
