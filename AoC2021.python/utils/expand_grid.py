class GridExpander:
    grid = {}
    initial_width = 0
    initial_height = 0

    def __init__(self, grid, width, height, ):
        self.grid = grid
        self.initial_width = width
        self.initial_height = height

    def expand(self, expansion_rate):
        # expand right
        for x in range(1, expansion_rate):
            for j in range(self.initial_height):
                for i in range(self.initial_width * x, ((self.initial_width * x) + self.initial_width)):
                    self.grid[(j, i)] = self.grid[(
                        j, i - self.initial_width)] + 1
                    if self.grid[(j, i)] > 9:
                        self.grid[(j, i)] = 1

        # expand down
        for y in range(1, expansion_rate):
            for j in range(self.initial_height * y, ((self.initial_height * y) + self.initial_height)):
                for i in range(self.initial_width * expansion_rate):
                    self.grid[(j, i)] = self.grid[(
                        j - self.initial_height, i)] + 1
                    if self.grid[(j, i)] > 9:
                        self.grid[(j, i)] = 1

        new_width = self.initial_width * expansion_rate
        new_height = self.initial_height * expansion_rate

        return self.grid, new_width, new_height
