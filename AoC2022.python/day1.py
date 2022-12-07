all_totals = []
temp = 0

def part1():
    global temp
    with open('input/2022/day1.txt') as f:
        for line in f:
            if line == '\n':
                all_totals.append(temp)
                temp = 0
                continue
            temp += int(line)

    print(max(all_totals))

def part2():
    all_totals = []
    temp = 0
    with open('input/2022/day1.txt') as f:
        for line in f:
            if line == '\n':
                all_totals.append(temp)
                temp = 0
                continue
            temp += int(line)

    print(sum(sorted(all_totals, reverse=True)[:3]))
