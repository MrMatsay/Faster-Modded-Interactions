using System.Collections.Generic;
using HarmonyLib;
using Verse.AI;

namespace GeneratorFasterStudy
{
    [HarmonyPatch(typeof(VanillaQuestsExpandedTheGenerator.JobDriver_StudyGenetron), "MakeNewToils")]
    public static class Patch_StudyGenetron_Faster
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils, VanillaQuestsExpandedTheGenerator.JobDriver_StudyGenetron __instance)
        {
            foreach (var toil in toils)
            {
                if (toil.tickAction != null)
                {
                    var originalAction = toil.tickAction;
                    toil.tickAction = () =>
                    {
                        __instance.totalTimer += 9; // Increment by 5 instead of 1 (5x faster)
                        originalAction();
                    };
                }
                yield return toil;
            }
        }
    }
}
