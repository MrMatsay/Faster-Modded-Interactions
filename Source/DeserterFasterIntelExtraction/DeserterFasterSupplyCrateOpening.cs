using HarmonyLib;
using Verse;
using RimWorld;

namespace DeserterFasterSupplyCrateOpening
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.deserterfastersupplycrateopening");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Building_Casket), "OpenTicks", MethodType.Getter)]
    public static class Patch_Casket_OpenTicks
    {
        static void Postfix(Building_Casket __instance, ref int __result)
        {
            var typeName = __instance.GetType().Name;

            if (typeName == "Building_SupplyCrate" || typeName == "Building_CrateBiosecured")
            {
                __result = 60;
            }
        }
    }
}