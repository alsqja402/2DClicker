using UnityEngine;

public static class Util
{
    static string[] _fixedUnits = { "", "K", "M", "B", "T" };
    public static string ToClickerString(float number, string format)
    {
        if (number < 1000)
        {
            return string.Format(format, number);
        }

        int unitIndex = 0;
        while (number >= 1000)
        {
            number /= 1000;
            unitIndex++;
        }

        string suffix = string.Empty;
        if (unitIndex < _fixedUnits.Length)
        {
            suffix = _fixedUnits[unitIndex];
        }
        else
        {
            unitIndex -= _fixedUnits.Length;
            char first = (char)('a' + (unitIndex / 26));
            char second = (char)('a' + (unitIndex % 26));
            suffix = $"{first}{second}";
        }

        return string.Format(format, $"{number:N2}{suffix}");
    }
}