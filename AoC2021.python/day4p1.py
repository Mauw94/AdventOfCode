# import numpy as np

data = open('input/day4_test.txt').read().splitlines()
numbers = [int(x) for x in data[0].split(',')]
board_count = (len(data) - 1) // 6
boards = []

for i in range(board_count):
    board = [(list(int(x) for x in data[i*6+j+2].split())) for j in range(5)]
    boards.append(board)
    
print(boards[0])
