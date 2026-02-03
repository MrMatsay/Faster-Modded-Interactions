using System.Collections.Generic;
using HarmonyLib;
using Verse.AI;

namespace FasterModdedInteractions
{
    [HarmonyPatchCategory("VFEDeserters")]
    [HarmonyPatch]
    public static class Patch_ExtractIntel_Faster
    {
        public static void Postfix(ref IEnumerable<Toil> __result) 
            => __result = ModifyToils(__result);

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
