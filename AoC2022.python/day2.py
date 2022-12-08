
def part1():
    t = 0
    for line in open('input/day2_test.txt').read().splitlines():
        x, y = line.split()

        x = ord(x) - 65
        y = ord(y) - ord("X")

        if x == y:
            t += 3
        elif (y - x) % 3 == 1:
            t += 6

        t += y + 1

    print(t)


def part2():
    t = 0
    for line in open('input/day2_test.txt').read().splitlines():
        x, y = line.split()

        x = ord(x) - 65

        if y == "X":
            t += (x - 1) % 3 + 1
        elif y == "Y":
            t += 3 + x + 1
        else:
            t += 6 + (x + 1) % 3 + 1

    print(t)
