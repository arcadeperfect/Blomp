using UnityEngine;

namespace Utilities
{
    public static class Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
        
            float tx = v.x;
            float ty = v.y;

            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }
        
        public static Vector3 RotateAroundAxis(this Vector3 v, float degrees)
        {
            
            var axis = Vector3.forward;
            
            // Calculate quaternion rotation
            Quaternion q = Quaternion.AngleAxis(degrees, axis);
            // Apply rotation
            return q * v;
        }
        
        public static Vector3 RotateAroundAxis(this Vector3 v, Vector3 axis, float degrees)
        {
            // Normalize axis
            axis.Normalize();
            // Calculate quaternion rotation
            Quaternion q = Quaternion.AngleAxis(degrees, axis);
            // Apply rotation
            return q * v;
        }
        
        public static SignType Sign(this float f)
        {
            if (f > 0) return SignType.Positive;
            if (f < 0) return SignType.Negative;
            return SignType.Zero;
        }
        
        public enum SignType
        {
            Positive,
            Negative,
            Zero
        }
    }
}