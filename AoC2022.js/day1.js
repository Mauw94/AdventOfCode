const fs = require('fs')
const data = fs.readFileSync('input/day1_test.txt', 'utf-8').trim().split('\r\n')

all_totals = []
total = 0
for (let i = 0; i < data.length; i++) {
  if (data[i] == '') {
    all_totals.push(total)
    total = 0
    continue
  }
  total += parseInt(data[i])
}

console.log(Math.max(...all_totals))
