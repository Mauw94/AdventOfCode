from collections import defaultdict


data = open('input/day14_test.txt').read().splitlines()
polymer = data[0]
rules = {}
occurences = defaultdict(int)


def set_rules():
    for pair in data[2:]:
        p, e = pair.split(" -> ")
        rules[p] = e


def get_pairs():
    pairs = []
    for i, c in enumerate(polymer):
        if i + 1 < (len(polymer)):
            pairs.append(polymer[i] + polymer[i + 1])
    return pairs


def create_new_polymer(pairs: list):
    polymer = []

    for i, p in enumerate(pairs):
        if p in rules:
            chars = [*p]
            polymer.append(chars[0])
            polymer.append(rules[p])
            if i == len(pairs) - 1:
                polymer.append(chars[1])

    p = "".join(polymer)
    return p


def count_occurences(polymer: str):
    for p in [*polymer]:
        occurences[p] += 1


set_rules()

steps = 10
while steps > 0:
    pairs = get_pairs()
    polymer = create_new_polymer(pairs)
    steps -= 1

count_occurences(polymer)

print(max(occurences.values()) - min(occurences.values()))
