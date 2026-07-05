using UnityEngine;
using System.Collections.Generic;

public class CharacterProgress
{
    private static HashSet<string> failedCharacters = new HashSet<string>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        failedCharacters.Clear();

        TimeManager.OnDayChanged -= ClearAll;
        TimeManager.OnDayChanged += ClearAll;
    }


    public static void MarkFailed(string characterId)
    {
        if (!string.IsNullOrEmpty(characterId))
            failedCharacters.Add(characterId);
    }

    public static bool IsFailed(string characterId)
    {
        return failedCharacters.Contains(characterId);
    }

    public static void ClearAll()
    {
        failedCharacters.Clear();
    }
}
