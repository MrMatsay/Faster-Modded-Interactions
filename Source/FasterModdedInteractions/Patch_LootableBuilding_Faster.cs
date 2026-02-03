using HarmonyLib;

namespace FasterModdedInteractions
{
    [HarmonyPatch(typeof(VEF.Buildings.LootableBuilding), "OpenTicks", MethodType.Getter)]
    public static class Patch_LootableBuilding_Faster
    {
        public static void Postfix(ref int __result) 
            => __result = 60;
    }
}
