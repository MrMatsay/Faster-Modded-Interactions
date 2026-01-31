using HarmonyLib;
using Verse;
using Verse.AI;
using System.Collections.Generic;

namespace AlphaPrefabsFasterUsage
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.alphaprefabsfasterusage");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(AlphaPrefabs.JobDriver_UsePrefab), "MakeNewToils")]
    public static class Patch_UsePrefab_Faster
    {
        static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils)
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