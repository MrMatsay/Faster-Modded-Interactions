using HarmonyLib;
using Verse;
using Verse.AI;
using System.Collections.Generic;
using RimWorld;

namespace AlphaBooksFasterReading
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.alphabooksfasterreading");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(JobDriver_Reading), "MakeNewToils")]
    public static class Patch_Reading_Faster
    {
        static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils, JobDriver_Reading __instance)
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