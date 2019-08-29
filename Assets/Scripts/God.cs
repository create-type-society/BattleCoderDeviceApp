using System.IO;
using Device;
using Ui;
using UnityEngine;

public class God : MonoBehaviour
{
    private IDeviceInput _deviceInput = new AccelerometerDeviceInput();
    private SendDeviceDataEventArgs _eventArgs;

    private LocalTcpClient _localTcpClient = new LocalTcpClient();

    private int _tick;

    [SerializeField] private MessageText messageText;

    void Start()
    {
        _deviceInput.SendDeviceDataEvent += DeviceInputOnSendDeviceDataEvent;
    }

    void Update()
    {
        if (_localTcpClient.IsConnect)
        {
            messageText.SetText("接続完了!");
            _deviceInput.Update();
        }
        else
        {
            if (_tick++ % 60 == 0 && !_localTcpClient.TryConnect)
                _localTcpClient.Connect();

            messageText.SetText("ゲームと接続中...");
        }
    }

    private void OnDestroy()
    {
        _localTcpClient.Disconnect();
    }

    private void DeviceInputOnSendDeviceDataEvent(object sender, SendDeviceDataEventArgs e)
    {
        _eventArgs = e;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (_eventArgs == null)
            return;

        var m = new MemoryStream(_eventArgs.Buffer);
        var br = new BinaryReader(m);

        GUI.Label(new Rect(10, 50, 200, 100), "Mag:" + br.ReadSingle(), new GUIStyle {fontSize = 80});

        br.Close();
        m.Close();
    }
#endif
}