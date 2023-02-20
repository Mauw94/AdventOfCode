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

b = []
for c in data[0].strip():
    b.append(hexa[c])

# 38006F45291200

# D2FE28
# 110 100 1011 1111 1000 1010 00

print("".join(b))
bits = "".join(b)

packets = []
version = int(bits[0:3], 2)
type = int(bits[3:6], 2)

print("version: ", version)
print("type ID: ", type)

if type != 4:
    # operator packet
    # get next 15 bits
    # get next bit at index 7
    # if it's 0
    # -> get next 15 bits
    # else get next 11
    length_type = int(bits[6:7], 2)
    print(length_type)

    if length_type == 0:
        sub_packets = bits[8:8+14]
        print(int(sub_packets, 2)) # is 27
        # get next 11 and 16 bits -> they're the subpackets
        # how to determine the 11 and 16??
        # => the next bits contain a new packet (literal or operator) -> do recursize stuff
    else:
        # get the next 11 bits
        print(11)
else:
    literal_bits = []
    bits_count = 0
    last = False
    s = 6
    while not last:
        b = bits[s:s+5]
        if b[0] == "0":
            last = True
        literal_bits.append(b[1:])
        bits_count += 5
        s += 5

    literal_value = "".join(literal_bits)
    packet = LiteralPacktet(version, int(literal_value, 2), bits_count)
    packets.append(packet)

# print(packet.size())
print(packets)

# test
packet = OperatorPacket(5)

t = 0
for p in packets:
    t += p.size()
print(t)
