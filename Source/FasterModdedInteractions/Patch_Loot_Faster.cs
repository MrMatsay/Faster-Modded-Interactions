using HarmonyLib;

namespace FasterModdedInteractions
{
    [HarmonyPatch(typeof(VEF.Buildings.JobDriver_Loot), "TotalTime", MethodType.Getter)]
    public static class Patch_Loot_Faster
    {
        public static void Postfix(ref int __result) 
            => __result = (int)(__result * 0.1f); // Makes it 5x faster (20% of original time)
    }
}
