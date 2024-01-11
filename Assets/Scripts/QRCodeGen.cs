using System;
using System.Net;
using QRCodeGenerator23;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI; 
[ExecuteAlways]
public class QrCodeGen : MonoBehaviour
{
    public Material material;
    private int port = 8000;
    
    Image QRCodePlaceHolder;
    

    public static Texture2D encoded;


    private void Start()
    {
        GenerateQrCode();
    }

    [Button]
    void GenerateQrCode()
    {
        QRCodePlaceHolder = gameObject.GetComponent<Image>();
        
        
        encoded = new Texture2D(312, 312);
        // var textForEncoding = message;
        if (GetLocalIPAddress() != null)
        {
            var message = $"{GetLocalIPAddress()},{port}";
            print(message);
            var color32 = CreateQRCode.Encode(message, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
        }

        QRCodePlaceHolder.sprite = Sprite.Create(encoded, new Rect(0, 0, encoded.width, encoded.height), Vector2.zero);
        material.mainTexture = encoded;
    }
    
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}
