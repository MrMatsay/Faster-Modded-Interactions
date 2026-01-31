using HarmonyLib;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace DeserterFasterIntelExtraction
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.deserterfasterintelextraction");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(VFED.JobDriver_ExtractIntel), "MakeNewToils")]
    public static class Patch_ExtractIntel_Faster
    {
        static void Postfix(ref IEnumerable<Toil> __result)
        {
            __result = ModifyToils(__result);
        }

        static IEnumerable<Toil> ModifyToils(IEnumerable<Toil> toils)
        {
            foreach (var toil in toils)
            {
                if (toil.defaultDuration == 3600)
                {
                    toil.defaultDuration = 60;
                }
                yield return toil;
            }
        }
    }
}