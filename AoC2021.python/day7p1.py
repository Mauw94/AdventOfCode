n = [int(x) for x in open('input/day7_test.txt').read().split(',')]
n.sort()
mid = int(len(n) / 2)

if len(n) % 2 == 1:
    median = n[mid]
else:
    median = (n[mid] + n[mid - 1]) / 2

t = 0
for x in n:
    f = x - median
    if f < 0:
        f *= -1
    t += f
    
print(t)
