using HarmonyLib;
using Verse;

namespace VEFasterBuildingLootTwo
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.vefasterbuildingloottwo");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(VEF.Buildings.LootableBuilding), "OpenTicks", MethodType.Getter)]
    public static class Patch_LootableBuilding_Faster
    {
        static void Postfix(ref int __result)
        {
            __result = 60;
        }
    }
}