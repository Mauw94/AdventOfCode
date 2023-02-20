from packet import Packet


class LiteralPacktet(Packet):
    lit_value = int()
    bits = int()
    def __init__(self, version, lit_value, bits):
        super().__init__(version)
        self.lit_value = lit_value
        self.bits = bits
    
    def size(self):
        return self.lit_value
    