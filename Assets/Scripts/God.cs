using System.IO;
using Device;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField] private IDeviceInput deviceInput = new AccelerometerDeviceInput();
    private SendDeviceDataEventArgs e;

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
        int i = 0;
        this.e = e;
    }

    private void OnGUI()
    {
        if (e == null)
            return;

        var m = new MemoryStream(e.Buffer);
        var br = new BinaryReader(m);

        GUI.Label(new Rect(10, 50, 200, 100), "X:" + br.ReadSingle(), new GUIStyle {fontSize = 80});
        GUI.Label(new Rect(10, 150, 200, 100), "Y:" + br.ReadSingle(), new GUIStyle {fontSize = 80});
        GUI.Label(new Rect(10, 250, 200, 100), "Z:" + br.ReadSingle(), new GUIStyle {fontSize = 80});

        br.Close();
        m.Close();
    }
}