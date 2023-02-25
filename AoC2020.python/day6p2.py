answers = open('input/day6.txt').read().strip().split("\n\n")
y = []
for a in answers:
    persons = a.split("\n")
    ans = set(persons[0])
    for i in range(1, len(persons)):
        ans &= set(persons[i])
    y.append(len(ans))

print(sum(y))
