using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public class BubbleHandler: MonoBehaviour
    {
        public OSCListener oscListener;
        public CircleCollider2D collissionCollider;
        public void Init(OSCListener listener)
        {
            oscListener = listener;
            oscListener.SizeMessageRecieved += OnOscSizeChange;
            oscListener.PressMessageRecieved += OnOscPress;
            collissionCollider = GetComponent<CircleCollider2D>();
        }
        
        void OnOscSizeChange(float size)
        {
            size = Hutl.Map(size, 0, 1, 0f, 2);
            transform.localScale = new Vector3(size, size, size);
        }
        [Button]
        void OnOscPress()
        {
            gameObject.GetComponent<BubbleController>().Blomp();
        }

    }
}