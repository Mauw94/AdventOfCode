from utils.dijkstra import Dijkstra


data = open('input/day15_test.txt').read().splitlines()


grid = {}
width = len(data)
height = len(data[0])

for i, line in enumerate(data):
    for j, d in enumerate(line.strip()):
        grid[(i, j)] = int(d)

start = (0, 0)
end = (len(data) - 1, len(data[0]) - 1)

dijkstra = Dijkstra(grid, width, height)
distance = dijkstra.distance_start_to_end(start, end)

print(distance)
