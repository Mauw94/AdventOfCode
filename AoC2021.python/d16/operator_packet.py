from packet import Packet


class OperatorPacket(Packet):
    def __init__(self, version):
        super().__init__(version)