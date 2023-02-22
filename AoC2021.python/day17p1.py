data = open('input/day17.txt').read().splitlines()
coords = data[0].split(", ")

x_range = list(map(int, coords[0][15::].split("..")))
y_range = list(map(int, coords[1][2::].split("..")))


def shoot(dx, dy):
    highest = 0
    x = 0
    y = 0

    while True:
        x += dx
        y += dy

        if highest < y:
            highest = y

        if check_target(x, y):
            return (True, highest)

        if no_hit(x, y):
            return (False, highest)

        if dx > 0:
            dx -= 1
        dy -= 1


def check_target(x, y):
    return x >= x_range[0] and x <= x_range[1] and y >= y_range[0] and y <= y_range[1]


def no_hit(x, y):
    return x > x_range[1] or y < y_range[0]


dx = 1
dy = y_range[0]
h = 0
hits = 0

for i in range(100000):
    (hit, highest) = shoot(dx, dy)

    if hit:
        hits += 1
        if h < highest:
            h = highest

    dx += 1
    if dx > x_range[1]:
        dx = 1
        dy += 1

print(highest)
print(hits)
