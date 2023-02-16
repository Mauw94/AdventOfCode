data = open('input/day10_test.txt').read().splitlines()
pairs = {
    "(": ")",
    "{": "}",
    "[": "]",
    "<": ">",
}

s = []
t = []

for d in data:
    s = []
    corrupt = False
    for x in list(d):
        if x in pairs:
            s.append(x)
        else:
            v = s.pop()
            if pairs[v] != x:
                corrupt = True
                break

    if len(s) > 0 and not corrupt:
        print("left in s")
        tt = 0
        while len(s) > 0:
            v = pairs[s.pop()]
            if v == ")":
                tt *= 5
                tt += 1
            elif v == "]":
                tt *= 5
                tt += 2
            elif v == "}":
                tt *= 5
                tt += 3
            else:
                tt *= 5
                tt += 4
        t.append(tt)

t.sort()
r = t[round(len(t) / 2)]
print(r)
