using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour {

    private const int MAX_CONNECTIONS = 100;
    private int hostId;
    private int webHostId;
    private int reliableChannel;
    private int unreliableChannel;
    private bool isConnected = false;
    private bool isStarted = false;
    private float connectionTime;
    private byte error;
    private int connectionId;
    private string hostIpAddress;
    private int port = 5701;

    public string playerName;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Connect()
    {
        NetworkTransport.Init();
        ConnectionConfig cc = new ConnectionConfig();

        reliableChannel = cc.AddChannel(QosType.Reliable);
        unreliableChannel = cc.AddChannel(QosType.Unreliable);

        HostTopology hostTopology = new HostTopology(cc, MAX_CONNECTIONS);
        hostId = NetworkTransport.AddHost(hostTopology, port);

        connectionId = NetworkTransport.Connect(hostId, hostIpAddress, port, 0, out error);

        connectionTime = Time.time;
        isConnected = true;

        Debug.Log("Connecting to host");
    }

    private void Update()
    {
        if (!isConnected)
            return;

        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recData)
        {
            case NetworkEventType.Nothing: break;
            case NetworkEventType.ConnectEvent: break;
            case NetworkEventType.DataEvent: break;
            case NetworkEventType.DisconnectEvent: break;

            case NetworkEventType.BroadcastEvent:

                break;
        }
    }

    public string HostIpAddress
    {
        set { hostIpAddress = value; }
    }

    public int Port
    {
        set { port = value; }
    }
}
