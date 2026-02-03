using HarmonyLib;
using RimWorld;

namespace FasterModdedInteractions
{
    [HarmonyPatchCategory("VFEDeserters")]
    [HarmonyPatch]
    public static class Patch_Casket_OpenTicks
    {
        public static void Postfix(Building_Casket __instance, ref int __result)
        {
            var typeName = __instance.GetType().Name;

            if (typeName == "Building_SupplyCrate" || typeName == "Building_CrateBiosecured")
            {
                __result = 60;
            }
        }
    }
}
