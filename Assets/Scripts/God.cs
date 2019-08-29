using Device;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField] private IDeviceInput deviceInput = new AccelerometerDeviceInput();

    void Start()
    {
        deviceInput.SendDeviceDataEvent += DeviceInputOnSendDeviceDataEvent;
    }

    void Update()
    {
        deviceInput.Update();
    }

    private void DeviceInputOnSendDeviceDataEvent(object sender, SendDeviceDataEventArgs e)
    {
        foreach (byte b in e.Buffer)
        {
            print(b);
        }
    }
}