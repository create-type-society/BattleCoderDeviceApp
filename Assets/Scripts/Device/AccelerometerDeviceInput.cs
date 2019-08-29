using System;
using System.IO;
using UnityEngine;

namespace Device
{
    public class AccelerometerDeviceInput : IDeviceInput
    {
        public event EventHandler<SendDeviceDataEventArgs> SendDeviceDataEvent;

        public void Update()
        {
            var acceleration = Input.acceleration;

            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);
            bw.Write(acceleration.x);
            bw.Write(acceleration.y);
            bw.Write(acceleration.z);

            OnSendDeviceDataEvent(this, new SendDeviceDataEventArgs(DeviceType.Accelerometer, ms.ToArray()));

            bw.Dispose();
            ms.Dispose();
        }

        public void OnSendDeviceDataEvent(object sender, SendDeviceDataEventArgs args)
        {
            SendDeviceDataEvent?.Invoke(sender, args);
        }
    }
}