import heapq


class Dijkstra:
    grid = {}
    width = 0
    height = 0

    def __init__(self, grid, width, height):
        self.grid = grid
        self.width = width
        self.height = height

    # returns shortest path (distance) from start -> end
    def distance_start_to_end(self, start, end):
        i = 0
        distances = {node: float('inf') for node in self.grid}
        distances[start] = 0
        visited = set()
        queue = [(0, start)]

        while queue:
            i += 1
            cur_distance, cur_node = heapq.heappop(queue)

            if cur_node in visited:
                continue

            if cur_node == end:
                return distances[cur_node], i

            if cur_distance > distances[cur_node]:
                continue

            visited.add(cur_node)

            for n in self.__neighbours(cur_node[0], cur_node[1]):
                weight = self.grid[n]
                distance = cur_distance + weight
                if distance < distances[n]:
                    distances[n] = distance
                    heapq.heappush(queue, (distance, n))

    # returns shortest path (distance) to every node
    def return_distances(self, start):
        distances = {node: float('inf') for node in self.grid}
        distances[start] = 0
        visited = set()
        queue = [(0, start)]

        while queue:
            cur_distance, cur_node = heapq.heappop(queue)

            if cur_node in visited:
                continue
            if cur_distance > distances[cur_node]:
                continue

            visited.add(cur_node)

            for n in self.__neighbours(cur_node[0], cur_node[1]):
                weight = self.grid[n]
                distance = cur_distance + weight
                if distance < distances[n]:
                    distances[n] = distance
                    heapq.heappush(queue, (distance, n))

        return distances

    def __neighbours(self, x, y):
        n = []
        n.append((x - 1, y))
        n.append((x + 1, y))
        n.append((x, y - 1))
        n.append((x, y + 1))

        return [x for x in n if x[0] >= 0
                and x[0] < self.width
                and x[1] >= 0
                and x[1] < self.height]
