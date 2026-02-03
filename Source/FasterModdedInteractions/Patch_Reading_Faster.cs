using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse.AI;

namespace FasterModdedInteractions
{
    [HarmonyPatchCategory("AlphaBooks")]
    [HarmonyPatch(typeof(JobDriver_Reading), "MakeNewToils")]
    public static class Patch_Reading_Faster
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils, JobDriver_Reading __instance)
        {
            foreach (var toil in toils)
            {
                if (toil.defaultDuration > 0)
                {
                    var book = __instance.Book;
                    if (book?.def?.HasModExtension<AlphaBooks.BookDefModExtension>() == true)
                    {
                        toil.defaultDuration = 60;
                    }
                }
                yield return toil;
            }
        }
    }
}
