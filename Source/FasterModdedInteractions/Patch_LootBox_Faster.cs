using HarmonyLib;

namespace FasterModdedInteractions
{
    [HarmonyPatchCategory("AlphaRandom")]
    [HarmonyPatch]
    public static class Patch_LootBox_Faster
    {
        public static void Postfix(ref int __result) 
            => __result = 60;
    }
}
