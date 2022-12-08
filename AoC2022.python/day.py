import day1, day2, day3
import time

st = time.time()

day1.part1()
day1.part2()
day2.part1()
day2.part2()

day3.part1()
#day3.part2()

et = time.time()
elapsed_time = et - st
print('\n')
print(elapsed_time * 1000, 'ms')
