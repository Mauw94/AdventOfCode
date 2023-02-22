input = open('input/day3.txt').read().splitlines()
map = []

for i in range(len(input)):
    arr = []
    for j, d in enumerate((input[i])):
        arr.append(d)
    map.append(arr)

w = len(map)
x,t = 0, 0

for i in range(len(map)):
    if map[i][x] == "#":
        t += 1
    x = (x + 3) % w

print(t)