using FasterModdedInteractions;
using HarmonyLib;
using Verse;

namespace GeneratorFasterStudy
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.fastermoddedinteractions.generator");
            Log.Message("Patching VQEGenerator");
            harmony.Patch(AccessTools.Method("VanillaQuestsExpandedTheGenerator.JobDriver_StudyGenetron:MakeNewToils"),
                postfix: new HarmonyMethod(typeof(Patch_StudyGenetron_Faster).GetMethod(nameof(Patch_StudyGenetron_Faster.Postfix))));
        }
    }
}
