using HarmonyLib;
using MelonLoader;
using Process;
using UnityEngine;

namespace SinmaiLegacyAchievementFrame;


[HarmonyPatch(typeof(MusicSelectProcess), "GetOptionValueSprite")]
internal static class OptB13RestoreHook
{
    private static readonly HashSet<string> Replaced = new(StringComparer.Ordinal);

    [HarmonyPostfix]
    private static void Postfix(string key, ref Sprite __result)
    {
        if (key != "UI_OPT_B_13_01" && key != "UI_OPT_B_13_02")
        {
            return;
        }

        if (!LegacyAchievementAssets.TryGetSprite(key, out var legacy) || legacy == null)
        {
            return;
        }

        if (__result != legacy)
        {
            __result = legacy;
            if (Replaced.Add(key))
            {
                MelonLogger.Msg($"[LegacyAchievement] option sprite replaced: {key}");
            }
        }
    }
}
