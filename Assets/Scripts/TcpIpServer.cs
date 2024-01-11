using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DefaultNamespace;
using UnityEngine;


public class TcpIpServer: MonoBehaviour
{
    private TcpListener _listener;
    private Dictionary<int,OSCListener> listeners = new Dictionary<int, OSCListener>();
    public int port;
    
    private void OnRecevedMessage(float val)
    {
        Debug.Log(val);
    }
    
    private void Start()
    {
        Init(port);
        AcceptClients();
        Debug.Log("initted");    }

    void Init(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port); // 'port' is the port number you want to listen on.
        _listener.Start(); // not a unity Start();
    }

    public void OnDestroy()
    {
        _listener.Stop();
    }
    
    async void AcceptClients()
    {
        while (true)
        {
            TcpClient client = await _listener.AcceptTcpClientAsync();
            HandleClient(client);
        }
    }

    async void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
                break; // The client has disconnected

            // Process the data sent by the client
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Debug.Log("Received: " + dataReceived);
            
            int port = Random.Range(8000, 9000);
            listeners.Add(port, GenerateOscListener(dataReceived, port));
            
            // Send a response
            string responseMessage = port.ToString();
            byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
            await stream.WriteAsync(responseData, 0, responseData.Length);
        }

        client.Close(); // Close the client connection
    }

    OSCListener GenerateOscListener(string id, int port)
    {
        var listener = gameObject.AddComponent<OSCListener>();
        listener.port = port;
        listener.id = id;
        listener.MessageReceived += OnRecevedMessage;
        BubbleController.BubbleFactory(listener);
        return listener;
    }
}