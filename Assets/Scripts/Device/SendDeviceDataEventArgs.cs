using System;

public class SendDeviceDataEventArgs : EventArgs
{
    public DeviceType DeviceType { get; }
    public byte[] Buffer { get; }

    public SendDeviceDataEventArgs(DeviceType type, byte[] buffer)
    {
        DeviceType = type;
        Buffer = buffer;
    }
}