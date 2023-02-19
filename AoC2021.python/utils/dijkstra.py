import heapq


class Dijkstra:
    grid = {}
    width = 0
    height = 0

    def __init__(self, grid, width, height):
        self.grid = grid
        self.width = width
        self.height = height

    def distance_start_to_end(self, start, end):
        distances = {node: float('inf') for node in self.grid}
        distances[start] = 0
        visited = set()
        queue = [(0, start)]

        while queue:
            cur_distance, cur_node = heapq.heappop(queue)

            if cur_node in visited:
                continue

            if cur_node == end:
                return distances[cur_node]

            if cur_distance > distances[cur_node]:
                continue

            for n in self.neighbours(cur_node[0], cur_node[1]):
                weight = self.grid[n]
                distance = cur_distance + weight
                if distance < distances[n]:
                    distances[n] = distance
                    heapq.heappush(queue, (distance, n))

    def neighbours(self, x, y):
        n = []
        n.append((x - 1, y))
        n.append((x + 1, y))
        n.append((x, y - 1))
        n.append((x, y + 1))

        return [x for x in n if x[0] >= 0
                and x[0] < self.width
                and x[1] >= 0
                and x[1] < self.height]
