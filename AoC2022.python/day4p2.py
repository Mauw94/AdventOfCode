t = 0

for line in open('input/day4.txt'):
    a, b, x, y = map(int, line.replace(",", "-").split("-"))
    if set(range(a, b + 1)) & set(range(x, y + 1)):
        t += 1

print(t)