import time
from utils.expand_grid import GridExpander
from utils.dijkstra import Dijkstra
from utils.AStar import AStar


data = open('input/day15_test.txt').read().splitlines()
grid = {}
width = len(data)
height = len(data[0])


for i, line in enumerate(data):
    for j, d in enumerate(line.strip()):
        grid[(i, j)] = int(d)

grid_expander = GridExpander(grid, width, height)
grid, width, height = grid_expander.expand(5)

start = (0, 0)
end = (width - 1, height - 1)

print("--- DIJKSTRA ---")
start_time = time.time()
dijkstra = Dijkstra(grid, width, height)
distance = dijkstra.distance_start_to_end(start, end)
d_time = (time.time() - start_time)
print(distance)
print("%s seconds" % d_time)

print("\n")
print("--- A* ---")
start_time = time.time()
a_start = AStar(grid, width, height)
distance = a_start.solve(start, end)
a_time = (time.time() - start_time)
print(distance)
print("\n")
print("%s seconds" % a_time)

print(round(((1 - (a_time / d_time)) * 100), 2), "% increase")
