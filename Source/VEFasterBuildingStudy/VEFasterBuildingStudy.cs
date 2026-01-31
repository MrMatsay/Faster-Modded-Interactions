using HarmonyLib;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace VEFasterBuildingStudy
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.vefasterbuildingstudy");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(VEF.Buildings.JobDriver_StudyBuilding), "MakeNewToils")]
    public static class Patch_StudyBuilding_Faster
    {
        static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils)
        {
            foreach (var toil in toils)
            {
                if (toil.tickIntervalAction != null)
                {
                    var originalAction = toil.tickIntervalAction;
                    toil.tickIntervalAction = (delta) =>
                    {
                        originalAction(delta * 20);
                    };
                }
                yield return toil;
            }
        }
    }
}