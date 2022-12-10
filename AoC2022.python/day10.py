x = 1
s = [0]

for line in open('input/day10.txt'):
    if line == "noop\n":
        s.append(x)
    else:
        v = int(line.split()[1])
        s.append(x)
        s.append(x)
        x += v

s.append(x)

print(sum([x * y for x, y in list(enumerate(s))[20::40]]))
