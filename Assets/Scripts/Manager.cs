using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Manager : MonoBehaviour
    {
        public float blompPower;
        public float gravConstant;
        public float centralize;
        public float drag;
        // public static List<BubbleController> bubbles = new List<BubbleController>();

        public GameObject cube;

        private void Start()
        {
            QualitySettings.vSyncCount = 1;
            Time.fixedDeltaTime = 1 / 120f;
            
            BubblesSimulator.Simulatables.Add(cube.GetComponent<Rigidbody2D>());
        }


        [Button]
        void GenerateBubble()
        {
            BubbleController bubble = BubbleController.BubbleFactory(null);
            BubblesSimulator.Simulatables.Add(bubble.RigidBody);
        }
    }
}