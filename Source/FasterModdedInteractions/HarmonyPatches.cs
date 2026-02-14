using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using VEF.Buildings;
using Verse;

namespace FasterModdedInteractions
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rosnok.fastermoddedinteractions");

            //Log.Message("Patching VFE.Buildings");
            harmony.Patch(AccessTools.PropertyGetter("VEF.Buildings.JobDriver_Loot:TotalTime"),
                postfix: new HarmonyMethod(typeof(Patch_Loot_Faster).GetMethod(nameof(Patch_Loot_Faster.Postfix))));
            harmony.Patch(AccessTools.PropertyGetter("VEF.Buildings.LootableBuilding:OpenTicks"),
                postfix: new HarmonyMethod(typeof(Patch_LootableBuilding_Faster).GetMethod(nameof(Patch_LootableBuilding_Faster.Postfix))));
            harmony.Patch(AccessTools.Method("VEF.Buildings.JobDriver_StudyBuilding:MakeNewToils"),
                postfix: new HarmonyMethod(typeof(Patch_StudyBuilding_Faster).GetMethod(nameof(Patch_StudyBuilding_Faster.Postfix))));
            if (ModsConfig.IsActive("oskarpotocki.vfe.deserters"))
            {
                //Log.Message("Patching VFEDeserters");
                harmony.Patch(AccessTools.Method("VFED.JobDriver_ExtractIntel:MakeNewToils"),
                    postfix: new HarmonyMethod(typeof(Patch_ExtractIntel_Faster).GetMethod(nameof(Patch_ExtractIntel_Faster.Postfix))));
                harmony.Patch(AccessTools.PropertyGetter("Building_Casket:OpenTicks"),
                    postfix: new HarmonyMethod(typeof(Patch_Casket_OpenTicks).GetMethod(nameof(Patch_Casket_OpenTicks.Postfix))));
            }
            //if (ModsConfig.IsActive("vanillaquestsexpanded.generator"))
            //{
            //    Log.Message("Patching VQEGenerator");
            //    harmony.Patch(AccessTools.Method("VanillaQuestsExpandedTheGenerator.JobDriver_StudyGenetron:MakeNewToils"),
            //        transpiler: new HarmonyMethod(typeof(Patch_StudyGenetron_Faster).GetMethod(nameof(Patch_StudyGenetron_Faster.Transpiler))));
            //}
            if (ModsConfig.IsActive("vanillaquestsexpanded.cryptoforge"))
            {
                //Log.Message("Patching VQECryptoforge");
                List<ThingDef> defs = new List<ThingDef>
                {
                    DefDatabase<ThingDef>.GetNamed("VQE_TwistedMetal"),
                    DefDatabase<ThingDef>.GetNamed("VQE_LargeTwistedMetal"),
                    DefDatabase<ThingDef>.GetNamed("VQE_HugeTwistedMetal"),
                    DefDatabase<ThingDef>.GetNamed("VQE_TwistedComponents"),
                    DefDatabase<ThingDef>.GetNamed("VQE_TwistedAdvancedComponents"),
                    DefDatabase<ThingDef>.GetNamed("VQE_TwistedPlasteel"),
                    DefDatabase<ThingDef>.GetNamed("VQE_GoldPile"),
                    DefDatabase<ThingDef>.GetNamed("VQE_FrozenCrateOfMedicine"),
                    DefDatabase<ThingDef>.GetNamed("VQE_FrozenCrateOfRations")
                };
                foreach (ThingDef def in defs)
                {
                    switch (FMISettings.lootSpeed)
                    {
                        case Speed.Normal:
                            break;
                        case Speed.Double:
                            def.SetStatBaseValue(StatDefOf.WorkToBuild, def.GetStatValueAbstract(StatDefOf.WorkToBuild) / 2);
                            break;
                        case Speed.Instant:
                            def.SetStatBaseValue(StatDefOf.WorkToBuild, 30f);
                            break;
                    }
                }
            }
            if (ModsConfig.IsActive("xmb.ancienturbanruins.mo"))
            {
                //Log.Message("Patching UrbanRuins");
                harmony.Patch(AccessTools.PropertyGetter("Building_Casket:OpenTicks"),
                    postfix: new HarmonyMethod(typeof(Patch_Casket_OpenTicks_UrbanRuins).GetMethod(nameof(Patch_Casket_OpenTicks_UrbanRuins.Postfix))));
            }
            if (ModsConfig.IsActive("sarg.alpharandom"))
            {
                //Log.Message("Patching AlphaRandom");
                harmony.Patch(AccessTools.PropertyGetter("AlphaRandom.LootBox:OpenTicks"),
                    postfix: new HarmonyMethod(typeof(Patch_LootBox_Faster).GetMethod(nameof(Patch_LootBox_Faster.Postfix))));
            }
            if (ModsConfig.IsActive("sarg.alphaprefabs"))
            {
                //Log.Message("Patching AlphaPrefabs");
                harmony.Patch(AccessTools.Method("AlphaPrefabs.JobDriver_UsePrefab:MakeNewToils"),
                    postfix: new HarmonyMethod(typeof(Patch_UsePrefab_Faster).GetMethod(nameof(Patch_UsePrefab_Faster.Postfix))));
            }
            if (ModsConfig.IsActive("sarg.alphabooks"))
            {
                //Log.Message("Patching AlphaBooks");
                harmony.Patch(AccessTools.Method("JobDriver_Reading:MakeNewToils"),
                    postfix: new HarmonyMethod(typeof(Patch_Reading_Faster).GetMethod(nameof(Patch_Reading_Faster.Postfix))));
            }
            List<ThingDef> doors = (from def in DefDatabase<ThingDef>.AllDefsListForReading
                                    where def.HasComp(typeof(CompJammedAirlock)) || def.HasComp(typeof(CompLabyrinthDoor))
                                    select def).ToList();
            if (!doors.NullOrEmpty())
            {
                //Log.Message("Patching doors");
                foreach (ThingDef def in doors)
                {
                    var props = (def.GetCompProperties<CompProperties_JammedAirlock>() != null) ?
                                def.GetCompProperties<CompProperties_JammedAirlock>() as CompProperties_Interactable
                                : def.GetCompProperties<CompProperties_LabyrinthDoor>();
                    if (props != null)
                    {
                        switch (FMISettings.doorSpeed)
                        {
                            case Speed.Normal:
                                break;
                            case Speed.Double:
                                props.ticksToActivate /= 2;
                                break;
                            case Speed.Instant:
                                props.ticksToActivate = 30;
                                break;
                        }
                    }
                }
            }
        }
    }
}
