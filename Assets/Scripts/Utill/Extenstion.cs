using UnityEngine;

public static class Extension
{
    public static string ToClickerString(this float number, string format)
    {
        return Util.ToClickerString(number, format);
    }
}
