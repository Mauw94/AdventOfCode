import re

data = open('input/day13_test.txt').read().splitlines()
coords = set()
folds = []

for input in data:
    if "," in input:
        x, y = map(int, input.split(","))
        coords.add((x, y))
    if input == "\n":
        continue
    if "x" in input:
        n = re.findall(r'\d+', input)
        folds.append([True, int(n[0])])
    if "y" in input:
        n = re.findall(r'\d+', input)
        folds.append([False, int(n[0])])


def fold(coords, f):
    coords_copy = set()
    for coord in coords:
        if f[0]:
            if coord[0] > f[1]:
                off = f[1] - (coord[0] - f[1])
                coords_copy.add((off, coord[1]))
            else:
                coords_copy.add((coord[0], coord[1]))
        else:
            if coord[1] > f[1]:
                off = f[1] - (coord[1] - f[1])
                coords_copy.add((coord[0], off))
            else:
                coords_copy.add((coord[0], coord[1]))

    return coords_copy


# p1
# f = folds[0]
# coords = fold(coords, f)
# print(len(coords))

# p2
while len(folds) > 0:
    f = folds.pop(0)
    coords = fold(coords, f)

mx = min(x for x, y in coords)
my = min(y for x, y in coords)

coords = {(x - mx, y - my) for x, y in coords}

mx = max(x for x, y in coords)
my = max(y for x, y in coords)

g = [["  "] * (mx + 1) for _ in range(my + 1)]

for x, y in coords:
    g[y][x] = "##"

for r in g:
    print("".join(r))

# print(len(coords))
