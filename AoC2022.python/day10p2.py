x = 1
s = []

for line in open('input/day10p2.txt'):
    if line == "noop\n":
        s.append(x)
    else:
        v = int(line.split()[1])
        s.append(x)
        s.append(x)
        x += v

for i in range(0, len(s), 40):
    for j in range(40):
        print("##" if abs(s[i + j] - j) <= 1 else "  ", end="")
    print()
