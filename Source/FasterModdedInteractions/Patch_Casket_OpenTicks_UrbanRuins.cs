using HarmonyLib;
using RimWorld;

namespace FasterModdedInteractions
{
    [HarmonyPatchCategory("UrbanRuins")]
    [HarmonyPatch(typeof(Building_Casket), "OpenTicks", MethodType.Getter)]
    public static class Patch_Casket_OpenTicks_UrbanRuins
    {
        public static void Postfix(Building_Casket __instance, ref int __result)
        {
            if (__instance.GetType().Name == "Lootbox")
            {
                __result = 30;
            }
        }
    }
}
