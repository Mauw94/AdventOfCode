class Node:
    def __init__(self, value, prv=None, nxt=None):
        self.value = value
        self.prv = prv
        self.nxt = nxt


r = []
for line in open('input/day20.txt').read().splitlines():
    n = int(line) * 811589153
    r.append(Node(n))

for a, b in zip(r, r[1:]):
    a.nxt = b
    b.prv = a

r[-1].next = r[0]
r[0].prv = r[-1]

# for n in r:
#     print(n.value)

for _ in range(10):
    for x in r:
        x.prv.nxt = x.nxt
        x.nxt.prv = x.prv
        a, b = x.prv, x.nxt
        move = x.value % (len(r) - 1)
        if move > 0:
            for _ in range(move):
                a = a.nxt
                b = b.nxt
        else:
            for _ in range(-move):
                a = a.prv
                b = b.prv
        a.nxt = x
        x.prv = a
        b.prv = x
        x.nxt = b

for x in r:
    if x.value == 0:
        t = 0
        y = x
        for _ in range(3):
            for _ in range(1000):
                y = y.nxt
            t += y.value
        print(t)
