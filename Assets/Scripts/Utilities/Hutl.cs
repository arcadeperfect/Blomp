using System.Text.RegularExpressions;
using UnityEngine;

public static class Hutl
{
    
    
    /// <summary>
    /// Find which side of an infinite line a point falls on
    /// positive = left
    /// negative = right
    /// zero = on the line
    /// </summary>
    /// <param name="line1"></param>
    /// <param name="line2"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static float FindDeterminant(Vector2 line1, Vector2 line2, Vector2 point)
    {
        Vector2 u = line2 - line1;
        Vector2 v = point - line1;
        return u.x * v.y - u.y * v.x;
    }
    public static float FindDistanceFromPointToLine(Vector2 line1, Vector2 line2, Vector2 point)
    {
        float A = line2.y - line1.y;
        float B = line1.x - line2.x;
        float C = line2.x * line1.y - line1.x * line2.y;

        float distance = Mathf.Abs(A * point.x + B * point.y + C) / Mathf.Sqrt(A * A + B * B);
        return distance;
    }

    public static float Map(float value, float start1, float stop1, float start2, float stop2, bool clamp)
    {
        if (!clamp) return Map(value, start1, stop1, start2, stop2);
        
        float l = Mathf.Min(start2, stop2);
        float h = Mathf.Max(start2, stop2);
        
        return Mathf.Clamp(Map(value, start1, stop1, start2, stop2), l, h);
        
    }

    /// <summary>
    ///    Map a value from one range to another
    /// </summary>
    /// <param name="value"></param>
    /// <param name="start1"></param>
    /// <param name="stop1"></param>
    /// <param name="start2"></param>
    /// <param name="stop2"></param>
    /// <returns></returns>
    public static float Map(float value, float start1, float stop1, float start2, float stop2)
    {

        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
    
    

    public static bool checkVec(Vector2 v1, Vector2 v2, float _tollerance)
    {
        return Mathf.Abs(v1.x - v2.x) < _tollerance && Mathf.Abs(v1.y - v2.y) < _tollerance;
    }

    public static bool checkVec(Vector2 v1, Vector2 v2)
    {
        var tollerance = 0.0001f;

        return Mathf.Abs(v1.x - v2.x) < tollerance && Mathf.Abs(v1.y - v2.y) < tollerance;
    }

    public static bool checkVec(Vector3 v1, Vector3 v2, float _tollerance)
    {
        return Mathf.Abs(v1.x - v2.x) < _tollerance && Mathf.Abs(v1.y - v2.y) < _tollerance &&
               Mathf.Abs(v1.z - v2.z) < _tollerance;
    }

    public static bool checkVec(Vector3 v1, Vector3 v2)
    {
        var tollerance = 0.0001f;

        return Mathf.Abs(v1.x - v2.x) < tollerance && Mathf.Abs(v1.y - v2.y) < tollerance &&
               Mathf.Abs(v1.z - v2.z) < tollerance;
    }

    public static float AbsAngle(Vector2 v)
    {
        //Vector2 _editorFloat = v.normalized.Rotate(180);
        return Mathf.Rad2Deg * Mathf.Atan2(v.x, v.y);
    }

    public static float AngleFrom(Vector2 v, Vector2 from)
    {
        var a = AbsAngle(v);
        var b = AbsAngle(from);
        return Wrap(a - b);
        //return AbsAngle(rotateVector2(v, b));
    }

    public static Vector2 rotateVector2(Vector2 v, float degrees)
    {
        var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        var tx = v.x;
        var ty = v.y;
        v.x = cos * tx - sin * ty;
        v.y = sin * tx + cos * ty;

        return v;
    }

    public static void DrawCross(Vector2 pos, Color clr, float Size = 0.1f)
    {
        var offset = new Vector2(Size, Size);
        Debug.DrawLine(pos + offset, pos - offset, clr);
        offset *= new Vector2(-1, 1);
        Debug.DrawLine(pos + offset, pos - offset, clr);
    }

    /// <summary>
    ///     Wrap values around min and max
    /// </summary>
    /// <param name="v">value to wrap</param>
    /// <param name="min">minimum</param>
    /// <param name="max">maxture</param>
    /// <returns>you know what it returns</returns>
    public static float Wrap(float v, float min = 0f, float max = 360f)
    {
        float range = max - min;
        if (range == 0.0f) 
            return min; // Avoid division by zero

        float wrappedValue = (v - min) % range;

        if (wrappedValue < 0)  // C#'s % operator isn't a true modulo for negative numbers
            wrappedValue += range;

        return wrappedValue + min;
        
        // if (v >= min && v < max)
        // {
        //     if (v == 360f)
        //     {
        //         v = 0;
        //         Debug.Log("botty");
        //     }
        //
        //     return v;
        // }
        //
        // if (v > max)
        // {
        //     var mult = Mathf.Floor(v / max);
        //     if (v == 360f)
        //     {
        //         v = 0;
        //         Debug.Log("batty");
        //     }
        //
        //     return v - max;
        //     //return (v - max * mult) ;
        //     //return wrap(v - max);
        // }
        // else
        // {
        //     var mult = Mathf.Floor(v / max);
        //     if (v == 360f)
        //     {
        //         v = 0;
        //         Debug.Log("bitty");
        //     }
        //
        //     return v + max;
        //     //return v + max * mult;
        //     //return wrap(v + max);
        // }
    }

    public static void DrawDebugNumber(Vector2 pos, int number, Color cl)
    {
        Debug.Log("haven't made this yet");
        // draw a number;
    }

    public static bool checkFloat(float f1, float f2, float _tollerance)
    {
        return Mathf.Abs(f1 - f2) < _tollerance;
    }

    public static bool checkFloat(float f1, float f2)
    {
        var tollerance = 0.0001f;
        return Mathf.Abs(f1 - f2) < tollerance;
    }
    
    public static string SplitCamelCase(string input)
    {
        return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
    }

    //public static Vector2? isIntersecting(WallLine wl, Vector2 pos, Vector2 dir)
    //{

    //    // test if a ray defined by a point and a direction intersects with an edge
    //    // return the point of intersection or null

    //    float x1 = wl.p1.x;      // rename variables to match those of algorithm aka Dan Schiffman 
    //    float y1 = wl.p1.y;      // for convenience
    //    float x2 = wl.p2.x;
    //    float y2 = wl.p2.y;

    //    float x3 = pos.x;
    //    float y3 = pos.y;
    //    float x4 = pos.x + dir.x;
    //    float y4 = pos.y + dir.y;

    //    float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

    //    if (den == 0)
    //    {
    //        return null;
    //    }
    //    else
    //    {

    //        float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
    //        float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

    //        if (t > 0 && t < 1 && u > 0)
    //        {                        // intersection found

    //            Vector2 impact = new Vector2();
    //            impact.x = x1 + t * (x2 - x1);
    //            impact.y = y1 + t * (y2 - y1);
    //            return impact;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //}

    public static float DecimalMinutesToSeconds(float f)
    {
        var minutes = Mathf.Floor(f);
        var seconds = f - minutes;
        return minutes * 60 + seconds * 100;
    }

    public static float NormalizedPow(float n, float pow, float low, float high)
    {
        if(pow == 0) throw new System.Exception("pow cannot be 0");
        if(pow == 1) return n;
        
        float f = Map(n, low, high, 0, 1);
        f = Mathf.Pow(f, pow);
        return Map(f, 0, 1, low, high);
    }
    
    public static float InverseNormalizedPow(float n, float pow, float low, float high)
    {
        if(pow == 0) throw new System.Exception("pow cannot be 0");
        if(pow == 1) return n;
        
        float f = Map(n, low, high, 0, 1);
        f = Mathf.Pow(f, 1f / pow);
        return Map(f, 0, 1, low, high);
    }

    public static bool RandBool()
    {
        return Random.value >= 0.5f;
    }
    
}