data = list(map(int, open('input/day1.txt')))

for i in range(len(data)):
    for j in range(len(data)):
        for k in range(len(data)):
            if data[i] + data[j] + data[k] == 2020:
                print(data[i] * data[j] * data[k])
