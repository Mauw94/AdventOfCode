const fs = require('fs')
const data = fs.readFileSync('input/day2.txt', 'utf-8').split('\r\n')

let total = 0

for (let i = 0; i < data.length; i++) {
    total += score(data[i])
}

function score(rps) {
    switch (rps) {
        case 'A X':
            return 4
        case 'A Y':
            return 8
        case 'A Z':
            return 3
        case 'B X':
            return 1
        case 'B Y':
            return 5
        case 'B Z':
            return 9
        case 'C X':
            return 7
        case 'C Y':
            return 2
        case 'C Z':
            return 6
    }
}

console.log(total)
