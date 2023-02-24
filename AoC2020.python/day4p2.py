import re


data = open('input/day4.txt').read().strip().split("\n\n")
keys = [
    "byr",
    "iyr",
    "eyr",
    "hgt",
    "hcl",
    "ecl",
    "pid"
]

passports = []
for d in data:
    passport = {}
    for e in d.replace("\n", " ").split(" "):
        k, v = e.split(":")
        passport[k] = v
    passports.append(passport)


def check_byr(byr):
    return byr >= 1920 and byr <= 2002


def check_iyr(iyr):
    return iyr >= 2010 and iyr <= 2020


def check_eyr(eyr):
    return eyr >= 2020 and eyr <= 2030


def check_hgt(hgt):
    m = hgt[-2:]
    height = int(hgt[:-2])
    if m == "cm":
        return height >= 150 and height <= 193
    elif m == "in":
        return height >= 59 and height <= 76


def check_hcl(hcl):
    return re.match(r"^#[0-9a-f]{6}$", hcl)


def check_ecl(ecl):
    k = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]
    return ecl in k


def check_pid(pid):
    return len(pid) == 9 and pid.isdigit()


valid = 0
for p in passports:
    if all(k in p for k in keys):
        if check_byr(int(p["byr"])):
            if check_iyr(int(p["iyr"])):
                if check_eyr(int(p["eyr"])):
                    if check_hgt(p["hgt"]):
                        if check_hcl(p["hcl"]):
                            if check_ecl(p["ecl"]):
                                if check_pid(p["pid"]):
                                    valid += 1

print(valid)
