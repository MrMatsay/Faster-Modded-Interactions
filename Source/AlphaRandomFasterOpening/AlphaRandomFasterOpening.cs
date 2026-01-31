using HarmonyLib;
using Verse;
using Verse.AI;
using System.Collections.Generic;

namespace AlphaRandomFasterOpening
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.alpharandomfasteropening");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(AlphaRandom.LootBox), "OpenTicks", MethodType.Getter)]
    public static class Patch_LootBox_Faster
    {
        static void Postfix(ref int __result)
        {
            __result = 60;
        }
    }
}