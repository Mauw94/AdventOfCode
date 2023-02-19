import heapq

data = open('input/day15_test.txt').read().splitlines()
grid = {}

for i, line in enumerate(data):
    for j, d in enumerate(line.strip()):
        grid[(i, j)] = int(d)

width = len(data)
height = len(data[0])
end_node = (width - 1, height - 1)


def neighbours(x, y):
    n = []
    n.append((x, y + 1))
    n.append((x, y - 1))
    n.append((x + 1, y))
    n.append((x - 1, y))

    return [x for x in n if x[0] >= 0 and x[0] < width and x[1] >= 0 and x[1] < height]


def dijkstra(start):
    distances = {node: float('inf') for node in grid}
    distances[start] = 0
    queue = [(0, start)]

    visited = set()

    while queue:
        cur_distance, cur_node = heapq.heappop(queue)

        if cur_node in visited:
            continue

        if cur_node == end_node:
            return distances[cur_node]

        visited.add(cur_node)

        if cur_distance > distances[cur_node]:
            continue

        for n in neighbours(cur_node[0], cur_node[1]):
            weight = grid[n]
            distance = cur_distance + weight

            if distance < distances[n]:
                distances[n] = distance
                heapq.heappush(queue, (distance, n))

    return distances


distance = dijkstra((0, 0))
print(distance)
