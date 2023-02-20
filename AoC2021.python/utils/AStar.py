import heapq


class AStar:

    grid = {}
    width = 0
    height = 0

    def __init__(self, grid, width, height):
        self.grid = grid
        self.width = width
        self.height = height

    def solve(self, start, end):
        open_queue = []
        closed_queue = set()
        g_score = {}
        g_score[start] = 0
        heapq.heappush(open_queue, (self.__heuristic(start, end), start))

        while open_queue:
            _, node = heapq.heappop(open_queue)

            if node == end:
                return g_score[node]

            closed_queue.add(node)
            for neighbour in self.__neighbours(node[0], node[1]):
                if neighbour in closed_queue:
                    continue

                tentative_g = g_score[node] + self.grid[neighbour]

                if neighbour not in g_score:
                    f = self.__heuristic(neighbour, end) + tentative_g
                    heapq.heappush(open_queue, (f, neighbour))
                elif tentative_g >= g_score[neighbour]:
                    continue

                g_score[neighbour] = tentative_g

        return None

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

    def __heuristic(self, a, b):
        (x1, y1) = a
        (x2, y2) = b
        return abs(x1 - x2) + abs(y1 - y2)
