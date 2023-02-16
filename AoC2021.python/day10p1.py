data = open('input/day10_test.txt').read().splitlines()
pairs = {
    "(": ")",
    "{": "}",
    "[": "]",
    "<": ">",
}
s = []
c = []
t = 0

for d in data:
    for x in list(d):
        if x in pairs:
            s.append(x)
        else:
            v = s.pop()
            if pairs[v] != x:
                c.append(x)
                break

for corrupt in c:
    if corrupt == ")":
        t += 3
    elif corrupt == "]":
        t += 57
    elif corrupt == "}":
        t += 1197
    else:
        t += 25137

print(t)
