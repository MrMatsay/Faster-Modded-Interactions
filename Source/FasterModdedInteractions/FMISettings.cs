using UnityEngine;
using Verse;

namespace FasterModdedInteractions
{
    public enum Speed : byte { Normal, Double, Instant }
    public class FMISettings : ModSettings
    {
        public static Speed doorSpeed;
        public static Speed lootSpeed;
        public static Speed studySpeed;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref doorSpeed, "doorSpeed", Speed.Normal);
            Scribe_Values.Look(ref lootSpeed, "lootSpeed", Speed.Normal);
            Scribe_Values.Look(ref studySpeed, "studySpeed", Speed.Normal);
        }
        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(inRect);
            string tooltip = "applies to various jammed doors from VQE and Anomaly";
            ls.AddLabeledSlider<Speed>("Door unlocking speed **Requires game restart**", ref doorSpeed, 0.5f, tooltip);
            ls.GapLine(24f);
            string tooltip2 = "applies to lootable containers such as supply crates or pallets";
            ls.AddLabeledSlider<Speed>("Loot opening speed", ref lootSpeed, 0.5f, tooltip2);
            ls.GapLine(24f);
            string tooltip3 = "research terminals, intel extraction and alpha books reading";
            ls.AddLabeledSlider<Speed>("Building study speed", ref studySpeed, 0.5f, tooltip3);
            ls.End();
        }
    }
}
