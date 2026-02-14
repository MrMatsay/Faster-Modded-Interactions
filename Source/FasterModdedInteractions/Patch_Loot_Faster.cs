namespace FasterModdedInteractions
{
    public static class Patch_Loot_Faster
    {
        public static void Postfix(ref int __result)
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
