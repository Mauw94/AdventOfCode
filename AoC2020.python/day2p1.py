passwords = open('input/day2.txt').read().splitlines()
v = 0

for pw in passwords:
    min, max = map(int, pw[0:3].split("-"))
    c = pw[4]
    p = pw[7:]
    r = p.count(c)
    if r >= min and r <= max:
        v += 1

print(v)
