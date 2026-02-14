using System.Collections.Generic;
using Verse.AI;

namespace FasterModdedInteractions
{
    public static class Patch_ExtractIntel_Faster
    {
        public static void Postfix(ref IEnumerable<Toil> __result)
            => __result = ModifyToils(__result);

        static IEnumerable<Toil> ModifyToils(IEnumerable<Toil> toils)
        {
            foreach (var toil in toils)
            {
                if (toil.defaultDuration == 3600)
                {
                    switch (FMISettings.studySpeed)
                    {
                        case Speed.Normal:
                            break;
                        case Speed.Double:
                            toil.defaultDuration = 1800;
                            break;
                        case Speed.Instant:
                            toil.defaultDuration = 60;
                            break;
                    }
                }
                yield return toil;
            }
        }
    }
}
