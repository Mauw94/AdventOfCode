g, e = 0, 0

data = open('input/day3.txt').read().splitlines()

N = len(data[0])

for n in range(N):
    count0 = sum(1 for line in data if line[n] == '0')
    count1 = len(data) - count0
    g *= 2
    e *= 2
    if count0 < count1:
        g += 1
    else:
        e += 1

print(g * e)