// using System;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// [ExecuteAlways]
// public class TcpTesting: MonoBehaviour
// {
//     private TcpIpServer server;
//     // private TcpTransmitTest client;
//     
//     [Button]
//     void Start()
//     {
//         server = new TcpIpServer(8000);
//         // client = new TcpTransmitTest(8000);
//     }
//
//     // [Button]
//     // void Transmit(string message)
//     // {
//     //     if(server == null || client == null)
//     //         Start();
//     //     
//     //     client.TestAsync(message);
//     // }
//
//     // private void OnDestroy()
//     // {
//     //     if(server != null)
//     //         server.DeInit();
//     // }
// }