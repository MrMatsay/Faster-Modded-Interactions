using System.Collections.Generic;
using Verse.AI;

namespace FasterModdedInteractions
{
    public static class Patch_UsePrefab_Faster
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> toils)
        {
            foreach (var toil in toils)
            {
                if (toil.defaultDuration > 0)
                {
                    switch (FMISettings.lootSpeed)
                    {
                        case Speed.Normal:
                            break;

                        case Speed.Double:
                            toil.defaultDuration = (int)(toil.defaultDuration / 2f);
                            break;

                        case Speed.Instant:
                            toil.defaultDuration = 30;
                            break;
                    }
                }
                yield return toil;
            }
        }
    }
}
