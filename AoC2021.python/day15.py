from utils.dijkstra import Dijkstra


data = open('input/day15_test.txt').read().splitlines()
dijkstra = Dijkstra(data)

start = (0, 0)
end = (len(data) - 1, len(data[0]) - 1)
distance = dijkstra.distance_start_to_end(start, end)
print(distance)
