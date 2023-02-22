import math

data = open('input/day3.txt').read().splitlines()
map = []

for i in range(len(data)):
    arr = []
    for j, d in enumerate(data[i]):
        arr.append(d)
    map.append(arr)


def count_trees(dx, dy):
    w = len(map)
    x, t = 0, 0

    for i in range(0, len(map), dy):
        if map[i][x] == "#":
            t += 1
        x = (x + dx) % w
    return t


slopes = [[1, 1], [3, 1], [5, 1], [7, 1], [1, 2]]
t = []
for dx, dy in slopes:
    t.append(count_trees(dx, dy))

print(math.prod(t))
