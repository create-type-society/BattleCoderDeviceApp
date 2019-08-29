using System.Linq;

public struct PacketData
{
    public PacketType Type { get; }
    public byte[] Data { get; }

    public static PacketData CreatePacketData(byte[] buffer)
    {
        return new PacketData((PacketType) buffer[0], buffer.Skip(1).ToArray());
    }

    public PacketData(PacketType type, byte[] data)
    {
        Type = type;
        Data = data;
    }

    public byte[] GetBuffer()
    {
        return new byte[1] {(byte) Type}.Concat(Data).ToArray();
    }
}