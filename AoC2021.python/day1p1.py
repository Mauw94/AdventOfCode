data = open('input/day1.txt').read().splitlines()
depths = [int(x) for x in data]

increases = sum(x < y for x, y in zip(depths, depths[1:]))
print(increases)
