passwords = open('input/day2.txt').read().splitlines()
v = 0

for pw in passwords:
    min, max = map(int, pw[0:3].split("-"))
    c = pw[4]
    p = pw[7:]

    if p[min - 1] == c and not p[max - 1] == c:
        v += 1

print(v)
