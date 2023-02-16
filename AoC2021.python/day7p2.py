n = [int(x) for x in open('input/day7.txt').read().split(',')]
n.sort()
mean = round(sum(n) / len(n))
t = 0

for x in n:
    f = x - mean
    if f < 0:
        f *= -1
    
    temp = 0
    adjust = 0
    for i in range(f):
        temp += 1
        adjust += temp
    
    t += adjust
    
print(t)
        