using System.Collections.Generic;
using Verse.AI;

namespace FasterModdedInteractions
{
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
                        switch (FMISettings.studySpeed)
                        {
                            case Speed.Normal:
                                originalAction(delta);
                                break;
                            case Speed.Double:
                                originalAction(delta * 2);
                                break;
                            case Speed.Instant:
                                originalAction(delta * 20);
                                break;
                        }
                    };
                }
                yield return toil;
            }
        }
    }
}
