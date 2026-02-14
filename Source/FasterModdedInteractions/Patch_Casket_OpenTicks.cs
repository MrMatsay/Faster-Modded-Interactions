using RimWorld;

namespace FasterModdedInteractions
{
    public static class Patch_Casket_OpenTicks
    {
        public static void Postfix(Building_Casket __instance, ref int __result)
        {
            var typeName = __instance.GetType().Name;

            if (typeName == "Building_SupplyCrate" || typeName == "Building_CrateBiosecured")
            {
                switch (FMISettings.lootSpeed)
                {
                    case Speed.Normal:
                        break;

                    case Speed.Double:
                        __result = (int)(__result / 2f);
                        break;

                    case Speed.Instant:
                        __result = 30;
                        break;
                }
            }
        }
    }
}
