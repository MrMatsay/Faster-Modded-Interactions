using HarmonyLib;
using Verse;
using RimWorld;

namespace AURFasterSalvageLoot
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.aurfastersalvageloot");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Building_Casket), "OpenTicks", MethodType.Getter)]
    public static class Patch_Casket_OpenTicks
    {
        static void Postfix(Building_Casket __instance, ref int __result)
        {
            if (__instance.GetType().Name == "Lootbox")
            {
                __result = 30;
            }
        }
    }
}