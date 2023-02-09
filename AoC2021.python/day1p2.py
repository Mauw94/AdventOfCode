data = open('input/day1.txt').read().splitlines()
depths = [int(x) for x in data]
measurements = []

for i, _ in enumerate(depths):
    measurements.append(sum(depths[i:][:3]))
    
increases = sum(x < y for x,y in zip(measurements, measurements[1:]))
print(increases)