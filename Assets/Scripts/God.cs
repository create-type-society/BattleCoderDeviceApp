﻿using System.IO;
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
    [SerializeField] private ShotButton shotButton;

    void Start()
    {
        _deviceInput.SendDeviceDataEvent += DeviceInputOnSendDeviceDataEvent;

        shotButton.AddEventHandler(SendButtonData);
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
        _localTcpClient.Send(new PacketData(PacketType.InputDeviceData, e.Buffer));
    }

    private void SendButtonData()
    {
        if (_localTcpClient.IsConnect)
        {
            _localTcpClient.Send(new PacketData(PacketType.InputDeviceButtonData, new byte[0]));
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (_eventArgs == null)
        {
            return;
        }

        var m = new MemoryStream(_eventArgs.Buffer);
        var br = new BinaryReader(m);

        GUI.Label(new Rect(10, 50, 200, 100), "Mag:" + br.ReadSingle(), new GUIStyle {fontSize = 80});

        br.Close();
        m.Close();
    }
#endif
}