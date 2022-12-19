rocks = []
maxY = 0
unique_points = set()

for line in open('input/day14_test.txt'):
    rocks = [list(map(int, p.split(","))) for p in line.strip().split(" -> ")]
    for (x1, y1), (x2, y2) in zip(rocks, rocks[1:]):
        x1, x2 = sorted([x1, x2])
        y1, y2 = sorted([y1, y2])
        for x in range(x1, x2 + 1):
            for y in range(y1, y2 + 1):
                unique_points.add(x + y * 1j)
                maxY = max(maxY, y + 1)

total = 0

while True:
    start = 500
    while True:
        if start.imag >= maxY:
            print(total)
            exit(0)
        if start + 1j not in unique_points:
            start += 1j
            continue
        if start + 1j - 1 not in unique_points:
            start += 1j - 1
            continue
        if start + 1j + 1 not in unique_points:
            start += 1j + 1
            continue

        unique_points.add(start)
        total += 1
        break
