data = open('input/day12_test.txt').read().splitlines()
graph = {}

for x in data:
    fr, to = x.split("-")
    if fr not in graph:
        graph[fr] = []
    if to not in graph:
        graph[to] = []

    graph[to].append(fr)
    graph[fr].append(to)


def dfs(node: str, visited: list, paths: list):
    visited.append(node)
    if node == "end":
        print(",".join(visited))
        paths.append(",".join(visited))

    n = graph[node]
    for x in n:
        if is_small(x) and x in visited:
            continue
        dfs(x, visited.copy(), paths)  # .copy is important here


def is_small(node: str):
    return node.islower()


# print(graph)
paths = []
dfs("start", [], paths)
print(len(paths))
