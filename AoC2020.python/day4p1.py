passwords = open('input/day4.txt').read().split("\n\n")
keys = [
    "byr",
    "iyr",
    "eyr",
    "hgt",
    "hcl",
    "ecl",
    "pid"
]

valid_keys = sum([all([k in p for k in keys]) for p in passwords])

print(valid_keys)
