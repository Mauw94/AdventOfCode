known = {}

x = [line.strip() for line in open('input/day21_test.txt')]

for a in x:
    lhs, rhs = a.split(": ")
    if lhs in known:
        continue
    if rhs.isnumeric():
        known[lhs] = int(rhs)
    else:
        left, op, right = rhs.split()
        if (left in known and right in known):
            known[lhs] = eval(str(known[left]) + " " +
                              op + " " + str(known[right]))
        else:
            x.append(a)

print(known["root"])
