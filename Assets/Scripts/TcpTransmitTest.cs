using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TcpTransmitTest
{
    private int port;
    
    public TcpTransmitTest(int port )
    {
        this.port = port;
    }
    
    ~TcpTransmitTest()
    {
        
    }
    
    public async Task TestAsync(string message)
    {
        string serverIP = "127.0.0.1"; // Server IP address
        int serverPort = port; // Server port
    
        TcpClient client = new TcpClient();
        await client.ConnectAsync(serverIP, serverPort);
    
        NetworkStream stream = client.GetStream();
    
        // Sending a message
        string messageToSend = message;
        
        byte[] dataToSend = Encoding.ASCII.GetBytes(messageToSend);
        await stream.WriteAsync(dataToSend, 0, dataToSend.Length);

        // Receiving a response asynchronously
        byte[] buffer = new byte[1024];
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Debug.Log("Received: " + response);

        // Don't forget to close the stream and client
        stream.Close();
        client.Close();
    }

    
    public async void Test()
    {
        string serverIP = "127.0.0.1"; // Server IP address
        int serverPort = port; // Server port
    
        TcpClient client = new TcpClient();
        client.Connect(serverIP, serverPort);
    
        NetworkStream stream = client.GetStream();
    
        // Sending a message
        string messageToSend = "Hello, Server!";
        byte[] dataToSend = Encoding.ASCII.GetBytes(messageToSend);
        stream.Write(dataToSend, 0, dataToSend.Length);
    
        // Receiving a response (if expected)
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Debug.Log("Received: " + response);

        // Don't forget to close the stream and client
        stream.Close();
        client.Close();
    }

    
    // public void Test()
    // {
    //     string serverIP = "127.0.0.1"; // Server IP address
    //     int serverPort = port; // Server port
    //     
    //     TcpClient client = new TcpClient();
    //     client.Connect(serverIP, serverPort);
    //     
    //     NetworkStream stream = client.GetStream();
    //     
    //     // Sending a message
    //     string messageToSend = "Hello, Server!";
    //     byte[] dataToSend = Encoding.ASCII.GetBytes(messageToSend);
    //     stream.Write(dataToSend, 0, dataToSend.Length);
    //     
    //     // Receiving a response (if expected)
    //     byte[] buffer = new byte[1024];
    //     int bytesRead = stream.Read(buffer, 0, buffer.Length);
    //     string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
    //     Debug.Log("Received: " + response);
    // }
}