using System;
using System.Collections;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BubbleController: MonoBehaviour
    {
        public Disc disc;
        public CircleCollider2D collider;
        public Rigidbody2D rb;
        public BubbleHandler bubbleHandler;
        public Rigidbody2D RigidBody => rb;
        public CircleCollider2D popCollider;
        public PointEffector2D popEffector;
        
        public float Mass => rb.mass;
        
        public float radius = 1.25f;

        private void OnValidate()
        {
            if(disc != null)
                disc.Radius = radius;
        }

        [Button]
        public void Blomp()
        {
            StartCoroutine(BlompRoutine());
        }


        IEnumerator BlompRoutine()
        {
            rb.isKinematic = true;
            popEffector.forceMagnitude = FindObjectOfType<Manager>().blompPower;
            popEffector.enabled = true;
            popCollider.enabled = true;
            yield return new WaitForSeconds(0.1f);
            rb.isKinematic = false;
            popEffector.enabled = false;
            popCollider.enabled = false;
        }
        
        public static BubbleController BubbleFactory(OSCListener listener)
        {
            var newBubble = new GameObject("Bubble");
            var bubbleController = newBubble.AddComponent<BubbleController>();
            
            bubbleController.disc = newBubble.AddComponent<Disc>();
            bubbleController.collider = newBubble.AddComponent<CircleCollider2D>();
            bubbleController.rb = newBubble.AddComponent<Rigidbody2D>();
            if(listener != null)
            {
                bubbleController.bubbleHandler = newBubble.AddComponent<BubbleHandler>();
                bubbleController.bubbleHandler.Init(listener);
            }
            bubbleController.collider.radius = bubbleController.radius;
            bubbleController.disc.Radius = bubbleController.radius;
            bubbleController.transform.position = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            bubbleController.disc.Color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            BubblesSimulator.Simulatables.Add(bubbleController.rb);
            bubbleController.rb.drag = FindObjectOfType<Manager>().drag;
            
            bubbleController.popCollider = newBubble.AddComponent<CircleCollider2D>();
            bubbleController.popCollider.radius = bubbleController.radius * 2;
            bubbleController.popCollider.isTrigger = true;
            bubbleController.popCollider.usedByEffector = true;
            bubbleController.popEffector = newBubble.AddComponent<PointEffector2D>();
            bubbleController.popEffector.enabled = false;
            bubbleController.popCollider.enabled = false;
            // bubbleController.popEffector.forceMagnitude = FindObjectOfType<Manager>().blompPower;
            
            return bubbleController;
        }
    }
}