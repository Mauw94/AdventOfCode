import sympy

known = {}
known["humn"] = sympy.Symbol("x")

x = [line.strip() for line in open('input/day21.txt')]

ops = {
    "+": lambda x, y: x + y,
    "-": lambda x, y: x - y,
    "*": lambda x, y: x * y,
    "/": lambda x, y: x / y,
}

for a in x:
    name, expr = a.split(": ")
    if name in known:
        continue
    if expr.isnumeric():
        known[name] = sympy.Integer(expr)
    else:
        left, op, right = expr.split()
        if left in known and right in known:
            if name == "root":
                print(sympy.solve(known[left] - known[right]))
                break
            known[name] = ops[op](known[left], known[right])
        else:
            x.append(a)
