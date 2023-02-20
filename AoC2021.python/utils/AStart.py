from queue import PriorityQueue


class AStar:

    grid = {}
    width = 0
    height = 0

    def __init__(self, grid, width, height):
        self.grid = grid
        self.width = width
        self.height = height

    def solve(self, start, end):
        frontier = PriorityQueue()
        frontier.put(start, 0)
        came_from = {}
        cost_so_far = {}
        came_from[start] = None
        cost_so_far[start] = 0

        while not frontier.empty():
            current = frontier.get()

            if current == end:
                break

            for next_node in self.__neighbours(current[0], current[1]):
                new_cost = cost_so_far[current] + self.grid[next_node]
                if next_node not in cost_so_far or new_cost < cost_so_far[next_node]:
                    cost_so_far[next_node] = new_cost
                    priority = new_cost + self.__heuristic(end, next_node)
                    frontier.put(next_node, priority)
                    came_from[next_node] = current

        # path = []
        # current = end
        # while current != start:
        #     path.append(current)
        #     current = came_from[current]
        # path.append(start)
        # path.reverse()

        # return path
        return cost_so_far[end]

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
        return ((x1 - x2) ** 2 + (y1 - y2) ** 2) ** 0.5
