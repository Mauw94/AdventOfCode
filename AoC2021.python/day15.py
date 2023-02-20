import time
from utils.expand_grid import GridExpander
from utils.dijkstra import Dijkstra


data = open('input/day15_test.txt').read().splitlines()
grid = {}
width = len(data)
height = len(data[0])
start_time = time.time()

for i, line in enumerate(data):
    for j, d in enumerate(line.strip()):
        grid[(i, j)] = int(d)

grid_expander = GridExpander(grid, width, height)
grid, width, height = grid_expander.expand(5)

start = (0, 0)
end = (width - 1, height - 1)

dijkstra = Dijkstra(grid, width, height)
distance = dijkstra.distance_start_to_end(start, end)
# distances = dijkstra.return_distances(start)

print(distance)
print("--- %s seconds ---" % (time.time() - start_time))
# print(distances)
