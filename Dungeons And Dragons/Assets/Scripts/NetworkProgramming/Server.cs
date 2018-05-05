using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    private const int MAX_CONNECTIONS = 100;
    private int port = 5701;
    private int hostId;
    private int webHostId;
    private int reliableChannel;
    private int unreliableChannel;
    private bool isStarted = false;
    private byte error;

    public string playerName;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CreateHost()
    {
        NetworkTransport.Init();
        ConnectionConfig cc = new ConnectionConfig();

        reliableChannel = cc.AddChannel(QosType.Reliable);
        unreliableChannel = cc.AddChannel(QosType.Unreliable);

        HostTopology hostTopology = new HostTopology(cc, MAX_CONNECTIONS);
        hostId = NetworkTransport.AddHost(hostTopology, port, null);
        webHostId = NetworkTransport.AddWebsocketHost(hostTopology, port, null);

        isStarted = true;

        Debug.Log("Server Has Started");
    }

    public void Update()
    {
        if (!isStarted)
            return;

        Debug.Log("Update");
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
            case NetworkEventType.Nothing:
                Debug.Log("Nothing");
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("Player " + connectionId + " has connected");
                break;
            case NetworkEventType.DataEvent: break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("Player " + connectionId + " has disconnected");
                break;

            case NetworkEventType.BroadcastEvent:

                break;
        }
    }

    public int Port
    {
        set { port = value; }
    }
}
