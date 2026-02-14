using System;
using UnityEngine;
using Verse;

namespace FasterModdedInteractions
{
    public static class Utils
    {
        public static void AddLabeledSlider<T>(this Listing_Standard ls, string label, ref T value, float labelPct = 0.5f, string tooltip = null) where T : Enum
        {
            Enum enu = value as Enum;
            Rect rect = ls.GetRect(30f, 1f);
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect.LeftPart(labelPct), label);
            if (tooltip != null)
            {
                TooltipHandler.TipRegion(rect.LeftPart(labelPct), tooltip);
            }
            Rect rect2 = ls.GetRect(30f, 1f);
            Text.Anchor = TextAnchor.UpperLeft;
            float bufferVal = Convert.ToInt32(enu);
            float tempVal = Widgets.HorizontalSlider(rect2.RightPart(1f - labelPct), bufferVal, 0f, Enum.GetValues(typeof(T)).Length - 1, true, Enum.GetName(typeof(T), value), roundTo: 1);

            value = (T)Enum.ToObject(typeof(T), (int)tempVal);
        }
    }
}
