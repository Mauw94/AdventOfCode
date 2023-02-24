import math

passes = open('input/day5.txt').read().splitlines()
boarding_passes = []

for p in passes:
    row, col, seat_id = 0, 0, 0
    range_r = [0, 127]
    range_c = [0, 7]
    rows = p[:7]
    cols = p[7:]
    for i, r in enumerate(rows):
        if i == len(rows) - 1:
            if r == "F":
                row = min(range_r[0], range_r[1])
            else:
                row = max(range_r[0], range_r[1])
        elif r == "F":
            range_r = [range_r[0], math.floor(
                range_r[1] / 2) + (range_r[0] / 2)]
        else:
            range_r = [math.ceil(range_r[1] / 2) +
                       (range_r[0] / 2), range_r[1]]

    for i, c in enumerate(cols):
        if i == len(cols) - 1:
            if c == "R":
                col = max(range_c[0], range_c[1])
            else:
                col = min(range_c[0], range_c[1])
        elif c == "L":
            range_c = [range_c[0], math.floor(
                range_c[1] / 2) + (range_c[0] / 2)]
        else:
            range_c = [math.ceil(range_c[1] / 2) +
                       (range_c[0] / 2), range_c[1]]

    seat_id = row * 8 + col
    boarding_passes.append([row, col, seat_id])

m = 0
for b in boarding_passes:
    if b[2] > m:
        m = b[2]
print(m)
