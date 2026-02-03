using System.Collections.Generic;
using HarmonyLib;
using Verse.AI;

namespace FasterModdedInteractions
{
    [HarmonyPatch(typeof(VEF.Buildings.JobDriver_StudyBuilding), "MakeNewToils")]
    public static class Patch_StudyBuilding_Faster
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils)
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
