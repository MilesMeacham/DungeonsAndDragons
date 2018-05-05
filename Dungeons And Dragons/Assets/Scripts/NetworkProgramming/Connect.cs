using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviour {

    private string hostIpAddress;
    private int hostPort;
    private int port;
    private string playerName;

    public GameObject hostGameObject;
    public GameObject clientGameObject;
    public InputField hostIpAddressField;
    public InputField hostPortField;
    public InputField portField;
    public InputField playerNameField;
    
    public void ConnectToHost()
    {
        if (string.IsNullOrEmpty(hostIpAddressField.text) || string.IsNullOrEmpty(hostPortField.text) || string.IsNullOrEmpty(playerNameField.text))
            return;

        hostIpAddress = hostIpAddressField.text;
        hostPort = int.Parse(hostPortField.text);
        playerName = playerNameField.text;

        Client client = clientGameObject.GetComponent<Client>();

        client.HostIpAddress = hostIpAddress;
        client.Port = hostPort;
        client.playerName = playerName;

        client.Connect();

    }

    public void CreateHost()
    {
        if (string.IsNullOrEmpty(portField.text) || string.IsNullOrEmpty(playerNameField.text))
            return;

        port = int.Parse(portField.text);
        playerName = playerNameField.text;

        Server server = hostGameObject.GetComponent<Server>();

        server.Port = port;
        server.playerName = playerName;

        server.CreateHost();
    }
}
