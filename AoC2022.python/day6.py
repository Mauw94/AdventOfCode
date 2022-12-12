
x = open('input/day6.txt').read()

for i in range(4, len(x)):
    if len(set(x[i - 4: i])) == 4:
        print(i)
        break

for i in range(4, len(x)):
    if len(set(x[i - 14:i])) == 14:
        print(i)
        break
