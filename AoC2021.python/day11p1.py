
data = open('input/day11_test.txt').read().splitlines()
board = []


def flash():
    flash = []
    seen = set()
    for i in range(len(board)):
        for j in range(len(board[i])):
            board[i][j] += 1
            if board[i][j] > 9:
                flash.append([i, j])
                seen.add((i, j))

    flash_neigbours(flash, seen)
    return set_flashed_to_zero(seen)


def set_flashed_to_zero(seen):
    flashes = 0
    for val in enumerate(seen):
        x = val[1][0]
        y = val[1][1]
        board[x][y] = 0
        flashes += 1

    return flashes


def flash_neigbours(flash: list, seen: set):
    while len(flash) > 0:
        f = flash.pop()
        nb = neighbours(f[0], f[1])
        for xi, n in enumerate(nb):
            board[n[0]][n[1]] += 1
            if board[n[0]][n[1]] > 9 and (n[0], n[1]) not in seen:
                flash.append([n[0], n[1]])
                seen.add((n[0], n[1]))


def neighbours(xi, yi):
    n = []
    n.append([xi - 1, yi])
    n.append([xi + 1, yi])
    n.append([xi, yi - 1])
    n.append([xi, yi + 1])
    n.append([xi + 1, yi + 1])
    n.append([xi - 1, yi - 1])
    n.append([xi - 1, yi + 1])
    n.append([xi + 1, yi - 1])

    return [x for x in n if x[0] >= 0 and x[0] < width and x[1] >= 0 and x[1] < height]


for idx, line in enumerate(data):
    arr = []
    for d in line.strip():
        arr.append(int(d))
    board.append(arr)

    width = len(board)
    height = len(board[0])

# part1
# t = 0
# for _ in range(10):
#     t += flash()
# print(t)

# part2
c = 0
while (True):
    c += 1
    if flash() == (width * height):
        print(c)
        break
