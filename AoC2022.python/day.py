import day1
import time

st = time.time()

day1.part1()
day1.part2()

et = time.time()
elapsed_time = et - st
print('\n')
print(elapsed_time * 1000, 'ms')
