def part1():
    t = 0

    for line in open('input/day3.txt'):
        x = len(line) // 2
        a = line[:x]
        b = line[x:]
        k, = set(a) & set(b)
        if k >= "a":
            t += ord(k) - ord("a") + 1
        else:
            t += ord(k) - ord("A") + 27

    print(t)


def part2():
    t = 0

    while True:
        try:
            x = input()
            y = input()
            z = input()
        except:
            break

    k, = set(x) & set(y) & set(z)
    if k >= "a":
        t += ord(k) - ord("a") + 1
    else:
        t += ord(k) - ord("A") + 27