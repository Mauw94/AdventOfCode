visited = set([(0, 0)])

H = [0, 0]
T = [0, 0]

for line in open('input/day9_test.txt'):
    x, y = line.split()
    y = int(y)
    for s in range(y):
        dx = 1 if x == "R" else -1 if x == "L" else 0
        dy = 1 if x == "U" else -1 if x == "D" else 0

        H[0] += dx
        H[1] += dy

        sx = H[0] - T[0]
        sy = H[1] - T[1]

        if (abs(sx) > 1 or abs(sy) > 1):
            if sx == 0:
                T[1] += sy // 2
            elif sy == 0:
                T[0] += sx // 2
            else:
                T[0] += 1 if sx > 0 else -1
                T[1] += 1 if sy > 0 else -1
        
        visited.add(tuple(T))
        
print(len(visited))
