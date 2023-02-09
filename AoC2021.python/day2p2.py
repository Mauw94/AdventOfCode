forward, depth, aim = 0, 0, 0

for input in open('input/day2.txt').read().splitlines():
    command = input.split()
    if command[0] == 'forward':
        forward += int(command[1])
        depth += aim * int(command[1])
    elif command[0] == 'down':
        aim += int(command[1])
    elif command[0] == 'up':
        aim -= int(command[1])

print(forward * depth)
