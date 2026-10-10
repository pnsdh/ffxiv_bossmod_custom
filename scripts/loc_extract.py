"""Lists UI strings that have no Korean translation yet.

Scans config attributes (PropertyDisplay labels/tooltips, PropertyCombo values, GroupDetails/GroupPreset,
SectionStart, ConfigDisplay names, config node names), UI text in BossMod (Loc.Tr calls, tab names, UIMisc.Button
reasons, About/changelog helpers, slash command descriptions), and optionally hint and timeline state strings in the
given module directories, and compares them against the tables in BossMod/Localization/LocKorean*.cs.

Usage: python scripts/loc_extract.py [--hints DIR ...] [--states DIR ...] [--all] [--json OUT]
  --hints DIR  also collect hints.Add(...) strings from .cs files under DIR (e.g. BossMod.Ultimate/Endwalker/Ultimate/TOP)
  --states DIR also collect timeline state/phase names (StateMachineBuilder name arguments) under DIR
  --all        list every string, not only untranslated ones
  --json OUT   write [{"en", "kind", "file"}] to OUT instead of printing
"""
import argparse
import json
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
CONFIG_DIRS = ['BossMod', 'BossMod.Modules', 'BossMod.Ultimate', 'BossMod.Autorotation']
ATTRS = ['PropertyDisplay', 'PropertyCombo', 'GroupDetails', 'GroupPreset', 'SectionStart', 'ConfigDisplay']

TOKEN = re.compile(r'''
    (?P<vstr>@"(?:[^"]|"")*")
  | (?P<istr>\$"(?:[^"\\]|\\.)*")
  | (?P<str>"(?:[^"\\]|\\.)*")
  | (?P<ident>[A-Za-z_]\w*)
  | (?P<punct>[()\[\]{},=:+])
  | (?P<ws>\s+)
  | (?P<other>.)
''', re.X | re.S)

ESCAPES = {'n': '\n', 't': '\t', '"': '"', '\\': '\\', '0': '\0', 'r': '\r'}


def unescape(lit):
    if lit.startswith('@'):
        return lit[2:-1].replace('""', '"')
    body = lit[1:-1]
    return re.sub(r'\\(u[0-9a-fA-F]{4}|.)', lambda m: chr(int(m.group(1)[1:], 16)) if m.group(1)[0] == 'u' else ESCAPES.get(m.group(1), m.group(1)), body)


def interp_to_pattern(lit):
    # $"Order: {ps.Order}, group: {ps.Group}" -> "Order: {0}, group: {1}"
    body = lit[2:-1]
    out, n, i = [], 0, 0
    while i < len(body):
        c = body[i]
        if c == '{' and body[i + 1:i + 2] == '{':
            out.append('{'); i += 2
        elif c == '}' and body[i + 1:i + 2] == '}':
            out.append('}'); i += 2
        elif c == '{':
            depth, j = 1, i + 1
            while depth:
                depth += {'{': 1, '}': -1}.get(body[j], 0)
                j += 1
            out.append('{%d}' % n); n += 1; i = j
        else:
            out.append(c); i += 1
    return unescape('"' + ''.join(out) + '"')


def tokens(text):
    for m in TOKEN.finditer(text):
        kind = m.lastgroup
        if kind not in ('ws', 'other'):
            yield kind, m.group(), m.start()


def balanced_args(toks, start):
    # toks[start] is '(' ; returns list of tokens inside and index after ')'
    depth, i, out = 0, start, []
    while i < len(toks):
        k, v, _ = toks[i]
        if v in '([{':
            depth += 1
        elif v in ')]}':
            depth -= 1
            if depth == 0:
                return out, i + 1
        if i != start:
            out.append(toks[i])
        i += 1
    return out, i


def split_args(args):
    # split top-level by ',' -> list of (name or None, tokens)
    res, cur, depth = [], [], 0
    for t in args:
        if t[1] in '([{':
            depth += 1
        elif t[1] in ')]}':
            depth -= 1
        if t[1] == ',' and depth == 0:
            res.append(cur); cur = []
        else:
            cur.append(t)
    if cur:
        res.append(cur)
    out = []
    for a in res:
        if len(a) >= 2 and a[0][0] == 'ident' and a[1][1] in (':', '='):
            out.append((a[0][1], a[2:]))
        else:
            out.append((None, a))
    return out


def literals(arg_tokens):
    # concatenates "a" + "b"; returns all string literals found (for arrays: each element)
    out, cur = [], None
    for k, v, _ in arg_tokens:
        if k in ('str', 'vstr'):
            cur = (cur or '') + unescape(v)
        elif v == '+':
            continue
        else:
            if cur is not None:
                out.append(cur); cur = None
    if cur is not None:
        out.append(cur)
    return out


def collect_config(path, rel, found):
    text = path.read_text(encoding='utf-8-sig')
    toks = list(tokens(text))
    for i, (k, v, _) in enumerate(toks):
        if k != 'ident' or v not in ATTRS or i + 1 >= len(toks) or toks[i + 1][1] != '(':
            continue
        args, _ = balanced_args(toks, i + 1)
        parts = split_args(args)
        if v == 'PropertyDisplay':
            pos = [a for n, a in parts if n is None]
            if pos:
                for s in literals(pos[0]):
                    found.append((s, 'label', rel))
            for n, a in parts:
                if n == 'tooltip':
                    for s in literals(a):
                        found.append((s, 'tooltip', rel))
        elif v in ('PropertyCombo', 'GroupDetails'):
            for n, a in parts:
                for s in literals(a):
                    found.append((s, 'value', rel))
        elif v in ('GroupPreset', 'SectionStart'):
            pos = [a for n, a in parts if n is None]
            if pos:
                for s in literals(pos[0]):
                    found.append((s, 'label', rel))
        elif v == 'ConfigDisplay':
            for n, a in parts:
                if n == 'Name':
                    for s in literals(a):
                        found.append((s, 'node', rel))
    # config node names generated from class names (ConfigUI.GenerateNodeName)
    for m in re.finditer(r'class\s+(\w+?)(Config)?\s*(?:\(\s*\))?\s*:\s*ConfigNode\b', text):
        found.append((m.group(1), 'node', rel))


def read_interpolated(text, i):
    # text[i:i+2] == '$"'; returns the literal with interpolation holes (which may contain nested strings) intact
    j, depth = i + 2, 0
    while j < len(text):
        c = text[j]
        if depth == 0:
            if c == '\\':
                j += 2; continue
            if c == '"':
                return text[i:j + 1]
            if c == '{':
                if text[j + 1:j + 2] == '{':
                    j += 2; continue
                depth = 1
        else:
            if c == '"':  # nested regular string inside a hole
                j += 1
                while text[j] != '"':
                    j += 2 if text[j] == '\\' else 1
            elif c == '{':
                depth += 1
            elif c == '}':
                depth -= 1
        j += 1
    return None


def collect_hints(path, rel, found):
    text = path.read_text(encoding='utf-8-sig')
    for m in re.finditer(r'hints\.Add\(\s*(?=\$?")', text):
        i = m.end()
        if text[i] == '$':
            lit = read_interpolated(text, i)
            if lit:
                found.append((interp_to_pattern(lit), 'hint', rel))
        else:
            lm = re.match(r'"(?:[^"\\]|\\.)*"', text[i:])
            found.append((unescape(lm.group()), 'hint', rel))


# calls whose arguments are UI text: every top-level string literal inside the call is collected
# (Loc.Tr in UI code, tab names, About/changelog helpers, slash command descriptions)
UI_CALLS = re.compile(r'(?:\bLoc\.Tr|_tabs\.Add|\bDrawSection|\bBullet|\.SetSimpleHandler|\.SetComplexHandler)\(')


def call_literals(text, i, stop_at_lambda=True):
    # text[i] is just after '('; yields literals until the matching ')', skipping interpolation holes
    depth = 0
    while i < len(text):
        c = text[i]
        if c == '$' and text[i + 1:i + 2] == '"':
            lit = read_interpolated(text, i)
            yield interp_to_pattern(lit)
            i += len(lit)
            continue
        if c == '"':
            lm = re.match(r'"(?:[^"\\]|\\.)*"', text[i:])
            yield unescape(lm.group())
            i += len(lm.group())
            continue
        if c == '/' and text[i + 1:i + 2] == '/':
            i = text.index('\n', i)
            continue
        if stop_at_lambda and c == '=' and text[i + 1:i + 2] == '>' and depth == 0:
            return  # lambda argument (e.g. command handler body) is not UI text
        if c in '([{':
            depth += 1
        elif c in ')]}':
            if depth == 0:
                return
            depth -= 1
        i += 1


# StateMachineBuilder methods whose `name` argument is shown in the mechanic timeline ("Cur: ... Then: ...")
STATE_CALLS = re.compile(r'\b(?:SimplePhase|SimpleState|Timeout|Condition|ConditionFork|ComponentCondition|ComponentConditionFork'
                         r'|ActorCastStart|CastStart|ActorCastStartMulti|CastStartMulti|ActorCastStartFork|CastStartFork|ActorCastEnd|CastEnd'
                         r'|ActorCast|Cast|ActorCastMulti|CastMulti|ActorTargetable|Targetable)(?:<[^()]*?>)?\(')


def collect_states(path, rel, found):
    text = path.read_text(encoding='utf-8-sig')
    for m in STATE_CALLS.finditer(text):
        for s in call_literals(text, m.end(), stop_at_lambda=False):
            if re.search(r'[A-Za-z]', s):
                found.append((s, 'state', rel))


def collect_ui(path, rel, found):
    text = path.read_text(encoding='utf-8-sig')
    for m in UI_CALLS.finditer(text):
        for s in call_literals(text, m.end()):
            if re.search(r'[A-Za-z]{2}', s):
                found.append((s, 'ui', rel))
    # UIMisc.Button disabled reasons: every string literal argument after the label
    toks = list(tokens(text))
    for i, (k, v, pos) in enumerate(toks):
        if k == 'ident' and v in ('Button', 'DangerousButton') and i >= 1 and toks[i - 1][1] == 'UIMisc' and toks[i + 1][1] == '(':
            args, _ = balanced_args(toks, i + 1)
            for _, a in split_args(args)[1:]:
                for t in a:
                    if t[0] == 'str':
                        found.append((unescape(t[1]), 'ui', rel))


def known_translations():
    known = {}
    pair = re.compile(r'\(\s*("(?:[^"\\]|\\.)*")\s*,\s*("(?:[^"\\]|\\.)*")\s*\)')
    for f in (ROOT / 'BossMod' / 'Localization').glob('LocKorean*.cs'):
        for m in pair.finditer(f.read_text(encoding='utf-8-sig')):
            known[unescape(m.group(1))] = unescape(m.group(2))
    return known


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('--hints', nargs='*', default=[])
    ap.add_argument('--states', nargs='*', default=[])
    ap.add_argument('--all', action='store_true')
    ap.add_argument('--json')
    args = ap.parse_args()

    found = []
    for d in CONFIG_DIRS:
        for f in sorted((ROOT / d).rglob('*.cs')):
            if 'obj' in f.parts or 'bin' in f.parts:
                continue
            collect_config(f, f.relative_to(ROOT).as_posix(), found)
    for f in sorted((ROOT / 'BossMod').rglob('*.cs')):
        if 'obj' in f.parts or 'bin' in f.parts or 'Localization' in f.parts:
            continue
        collect_ui(f, f.relative_to(ROOT).as_posix(), found)
    for d in args.hints:
        for f in sorted((ROOT / d).rglob('*.cs')):
            collect_hints(f, f.relative_to(ROOT).as_posix(), found)
    for d in args.states:
        for f in sorted((ROOT / d).rglob('*.cs')):
            collect_states(f, f.relative_to(ROOT).as_posix(), found)

    known = known_translations()
    seen, result = set(), []
    for en, kind, rel in found:
        if not en.strip() or en in seen or (not args.all and en in known):
            continue
        seen.add(en)
        result.append({'en': en, 'kind': kind, 'file': rel})

    if args.json:
        Path(args.json).write_text(json.dumps(result, ensure_ascii=False, indent=1), encoding='utf-8')
        print(f'{len(result)} strings -> {args.json}')
    else:
        for r in result:
            print(f"[{r['kind']}] {r['file']}: {r['en']!r}")
        print(f'{len(result)} strings')


if __name__ == '__main__':
    main()
