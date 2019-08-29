using System;

public interface IDeviceInput
{
    event EventHandler<SendDeviceDataEventArgs> SendDeviceDataEvent;

    void Update();
}