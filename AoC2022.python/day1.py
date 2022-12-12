# all_totals = []
# temp = 0

# def part1():
#     global temp
#     with open('input/day1.txt') as f:
#         for line in f:
#             if line == '\n':
#                 all_totals.append(temp)
#                 temp = 0
#                 continue
#             temp += int(line)

#     print(max(all_totals))

# def part2():
#     all_totals = []
#     temp = 0
#     with open('input/day1.txt') as f:
#         for line in f:
#             if line == '\n':
#                 all_totals.append(temp)
#                 temp = 0
#                 continue
#             temp += int(line)

#     print(sum(sorted(all_totals, reverse=True)[:3]))


# all_totals = []
# t = 0

# for line in open('input/day1.txt'):
#     if line == "\n":
#         all_totals.append(t)
#         t = 0
#         continue
    
#     t += int(line)

# print(max(all_totals))

all_totals = []
t = 0

for line in open('input/day1.txt'):
    if line == "\n":
        all_totals.append(t)
        t = 0
        continue
    
    t += int(line)

# print(sum(sorted(all_totals)[-3:]))
print(sum(sorted(all_totals, reverse=True)[:3]))
