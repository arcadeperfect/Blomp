using System;
using extOSC;
using UnityEngine;


public class OSCListener : MonoBehaviour
{
    private OSCReceiver Receiver;
    
    public event Action<float> MessageReceived;

    public event Action<float> SizeMessageRecieved;
    public event Action PressMessageRecieved;
    
    private float val;
    public string id;


    public int port;
    
    protected virtual void Start()
    {
        Receiver = gameObject.AddComponent<OSCReceiver>();
        Receiver.LocalPort = port;
        // Receiver.Bind("/test", TestMessage);
        Receiver.Bind("/size", SizeMessage);
        Receiver.Bind("/jump", JumpMessage);
        
    }

    // void TestMessage(OSCMessage message)
    // {
    //     val = (float)message.Values[0].Value;
    //     val = Hutl.Map(val, 0, 1, 0.5f, 2); 
    //     MessageReceived?.Invoke(val);
    // }
    
    void SizeMessage(OSCMessage message)
    {
        val = (float)message.Values[0].Value;
        // print(val);
        SizeMessageRecieved?.Invoke(val);
    }

    void JumpMessage(OSCMessage message)
    {
        print("jump");
        PressMessageRecieved?.Invoke();
    }
}