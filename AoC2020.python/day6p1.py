answers = open('input/day6.txt').read().strip().split("\n\n")
s = 0

for a in answers:
    x = a.replace("\n", "")
    s += len(set(x))

print(s)