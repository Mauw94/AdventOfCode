# def part1():
#     t = 0

#     for line in open('input/day3.txt'):
#         x = len(line) // 2
#         a = line[:x]
#         b = line[x:]
#         k, = set(a) & set(b)
#         if k >= "a":
#             t += ord(k) - ord("a") + 1
#         else:
#             t += ord(k) - ord("A") + 27

#     print(t)


# def part2():
#     t = 0

#     while True:
#         try:
#             x = input()
#             y = input()
#             z = input()
#         except:
#             break

#     k, = set(x) & set(y) & set(z)
#     if k >= "a":
#         t += ord(k) - ord("a") + 1
#     else:
#         t += ord(k) - ord("A") + 27

s = 0

for line in open('input/day3.txt'):
    l = len(line) // 2
    a = line[:l]
    b = line[l:]

    k, = set(a) & set(b)
    if k > 'a':
        s += ord(k) - ord('a') + 1
    else:
        s += ord(k) - ord('A') + 27


print(s)

s = 0
c = 0
lst = open('input/day3.txt').readlines()
groups = [lst[i:i+3] for i in range(0, len(lst), 3)]

for group in groups:

    a = set(group[0].strip())
    b = set(group[1].strip())
    c = set(group[2].strip())
    k, = a & b & c
    
    if k > 'a':
        s += ord(k) - ord('a') + 1
    else:
        s += ord(k) - ord('A') + 27

print(s) 