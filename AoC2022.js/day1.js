const fs = require('fs')

const data = fs.readFileSync('input/day1_test.txt', 'utf-8').trim().split('\n')
numbers = data.map(x => parseInt(x))

all_totals = []
total = 0
for (let i = 0; i < data.length; i++) {
  if (isNaN(data[i])) {
    console.log('nan')
    all_totals.push(total)
    total = 0
    continue
  }
  total += data[i]
}

console.log(all_totals)
