using UnityEngine;

// Handles the current string that will be displayed by the anchor prefab
public static class GlobalTextState
{
    private static int _stringIdx = 0;
    private static readonly string[] Strings =
    {
       "There is at least 1 TRUE and 1 FALSE statement. Choose the right cup to get the prize.",
       "There is only one TRUE statement.",
       "A prize is in a cup with a FALSE statement",
       "There are two TRUE statements",
       ""
    };
    
    public static string CurrentString => Strings[_stringIdx];

    public static void Advance()
    {
        _stringIdx = (_stringIdx + 1) % Strings.Length;
    }

    public static void Reset()
    {
        _stringIdx = 0;
    }
}
