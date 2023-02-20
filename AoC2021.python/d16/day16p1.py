from operator_packet import OperatorPacket
from literal_packet import LiteralPacktet


data = open('../input/day16_test.txt').read().splitlines()
hexa = {
    "0": "0000",
    "1": "0001",
    "2": "0010",
    "3": "0011",
    "4": "0100",
    "5": "0101",
    "6": "0110",
    "7": "0111",
    "8": "1000",
    "9": "1001",
    "A": "1010",
    "B": "1011",
    "C": "1100",
    "D": "1101",
    "E": "1110",
    "F": "1111"
}

bits = []
for c in data[0].strip():
    bits.append(hexa[c])

print("".join(bits))

# todo parse bits to packets
packet = LiteralPacktet(2)
packet2 = OperatorPacket(5)
