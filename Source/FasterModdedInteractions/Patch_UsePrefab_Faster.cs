using System.Collections.Generic;
using HarmonyLib;
using Verse.AI;

namespace FasterModdedInteractions
{
    [HarmonyPatchCategory("AlphaPrefabs")]
    [HarmonyPatch]
    public static class Patch_UsePrefab_Faster
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils)
        {
            foreach (var toil in toils)
            {
                if (toil.defaultDuration > 0)
                {
                    toil.defaultDuration = 60;
                }
                yield return toil;
            }
        }
    }
}
