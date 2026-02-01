using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace FasterModdedInteractions
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.fastermoddedinteractions");
            harmony.PatchAllUncategorized();
            if (ModsConfig.IsActive("vanillaquestsexpanded.generator"))
            {
                Log.Message("Patching VQEGenerator");
                harmony.PatchCategory("VQEGenerator");
            }
            if (ModsConfig.IsActive("oskarpotocki.vfe.deserters"))
            {
                Log.Message("Patching VFEDeserters");
                harmony.PatchCategory("VFEDeserters");
            }
            if (ModsConfig.IsActive("xmb.ancienturbanruins.mo"))
            {
                Log.Message("Patching UrbanRuins");
                harmony.PatchCategory("UrbanRuins");
            }
            if (ModsConfig.IsActive("sarg.alpharandom"))
            {
                Log.Message("Patching AlphaRandom");
                harmony.PatchCategory("AlphaRandom");
            }
            if (ModsConfig.IsActive("sarg.alphaprefabs"))
            {
                Log.Message("Patching AlphaPrefabs");
                harmony.PatchCategory("AlphaPrefabs");
            }
            if (ModsConfig.IsActive("sarg.alphabooks"))
            {
                Log.Message("Patching AlphaBooks");
                harmony.PatchCategory("AlphaBooks");
            }
        }
    }

    [HarmonyPatch(typeof(VEF.Buildings.LootableBuilding), "OpenTicks", MethodType.Getter)]
    public static class Patch_LootableBuilding_Faster
    {
        public static void Postfix(ref int __result) => __result = 60;
    }

    [HarmonyPatch(typeof(VEF.Buildings.JobDriver_Loot), "TotalTime", MethodType.Getter)]
    public static class Patch_Loot_Faster
    {
        public static void Postfix(ref int __result) => __result = (int)(__result * 0.1f); // Makes it 5x faster (20% of original time)
    }

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

    [HarmonyPatchCategory("VQEGenerator")]
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

    [HarmonyPatchCategory("VFEDeserters")]
    [HarmonyPatch(typeof(VFED.JobDriver_ExtractIntel), "MakeNewToils")]
    public static class Patch_ExtractIntel_Faster
    {
        public static void Postfix(ref IEnumerable<Toil> __result) => __result = ModifyToils(__result);

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

    [HarmonyPatchCategory("VFEDeserters")]
    [HarmonyPatch(typeof(Building_Casket), "OpenTicks", MethodType.Getter)]
    public static class Patch_Casket_OpenTicks
    {
        public static void Postfix(Building_Casket __instance, ref int __result)
        {
            var typeName = __instance.GetType().Name;

            if (typeName == "Building_SupplyCrate" || typeName == "Building_CrateBiosecured")
            {
                __result = 60;
            }
        }
    }

    [HarmonyPatchCategory("UrbanRuins")]
    [HarmonyPatch(typeof(Building_Casket), "OpenTicks", MethodType.Getter)]
    public static class Patch_Casket_OpenTicks_UrbanRuins
    {
        public static void Postfix(Building_Casket __instance, ref int __result)
        {
            if (__instance.GetType().Name == "Lootbox")
            {
                __result = 30;
            }
        }
    }

    [HarmonyPatchCategory("AlphaRandom")]
    [HarmonyPatch(typeof(AlphaRandom.LootBox), "OpenTicks", MethodType.Getter)]
    public static class Patch_LootBox_Faster
    {
        public static void Postfix(ref int __result) => __result = 60;
    }

    [HarmonyPatchCategory("AlphaPrefabs")]
    [HarmonyPatch(typeof(AlphaPrefabs.JobDriver_UsePrefab), "MakeNewToils")]
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
