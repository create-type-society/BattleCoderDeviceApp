using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class LocalTcpClient
{
    private const int ServerPort = 5700;

    private TcpClient _client;

    private Task _task;

    public bool TryConnect { get; private set; }
    public bool IsConnect { get; private set; }

    public LocalTcpClient()
    {
        _client = new TcpClient();
    }

    public async void Connect()
    {
        try
        {
            TryConnect = true;
            _task = _client.ConnectAsync(IPAddress.Loopback, ServerPort);
            await _task;
            TryConnect = false;

            IsConnect = true;
        }
        catch (SocketException e)
        {
            TryConnect = false;
            Debug.Log(e);
        }
    }

    public void Send(PacketData packetData)
    {
        try
        {
            byte[] buf = packetData.GetBuffer();
            _client.GetStream().Write(buf, 0, buf.Length);
        }
        catch (InvalidOperationException e)
        {
            Debug.Log(e);
            _client.Close();
            _client = new TcpClient();
            IsConnect = false;
        }
        catch (SocketException e)
        {
            Debug.Log(e);
        }
    }

    public void Disconnect()
    {
        try
        {
            _task.Wait();
        }
        catch (AggregateException e)
        {
            Debug.Log(e);
        }

        _client.Dispose();
    }
}