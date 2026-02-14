using UnityEngine;
using Verse;

namespace FasterModdedInteractions
{
    public class FasterModdedInteractionsMod : Mod
    {
        public static FMISettings settings;
        public FasterModdedInteractionsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<FMISettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
            => Content.Name;
    }
}
