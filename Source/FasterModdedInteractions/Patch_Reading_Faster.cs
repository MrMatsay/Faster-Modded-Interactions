using System.Collections.Generic;
using RimWorld;
using Verse.AI;

namespace FasterModdedInteractions
{
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
                }
                yield return toil;
            }
        }
    }
}
