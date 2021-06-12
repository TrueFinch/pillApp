using pillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.Services
{
    public static class Globals
    {
        public static Dictionary<eCourseType, string> eCourseTypeToString =
            new Dictionary<eCourseType, string>
            {
                { eCourseType.PILL     , "Pill"      },
                { eCourseType.BIG_PILL , "Big pill"  },
                { eCourseType.DROPS    , "Drops"     },
                { eCourseType.POTION   , "Potion"    },
                { eCourseType.SALVE    , "Salve"     },
                { eCourseType.INJECTION, "Injection" },
                { eCourseType.PROCEDURE, "Procedure" },
            };
        public static Dictionary<string, eCourseType> eCourseTypeFromString =
            new Dictionary<string, eCourseType>
            {
                { "Pill"     , eCourseType.PILL      },
                { "Big pill" , eCourseType.BIG_PILL  },
                { "Drops"    , eCourseType.DROPS     },
                { "Potion"   , eCourseType.POTION    },
                { "Salve"    , eCourseType.SALVE     },
                { "Injection", eCourseType.INJECTION },
                { "Procedure", eCourseType.PROCEDURE },
            };
        public static Dictionary<eCourseFreq, string> eCourseFreqToString = 
            new Dictionary<eCourseFreq, string>
            {
                { eCourseFreq.EVERYDAY    , "Everyday"    },
                { eCourseFreq.EVERY_N_DAY , "Every N day" },
            };
        public static Dictionary<string, eCourseFreq> eCourseFreqFromString = 
            new Dictionary<string, eCourseFreq>
            {
                { "Everyday"    , eCourseFreq.EVERYDAY    },
                { "Every N day" , eCourseFreq.EVERY_N_DAY },
            };
        public static Dictionary<eCourseDuration, string> eCourseDurationToString =
            new Dictionary<eCourseDuration, string>
            {
                { eCourseDuration.ENDLESS     , "Endless"      },
                { eCourseDuration.N_DAYS      , "N days"       },
                { eCourseDuration.N_RECEPTIONS, "N receptions" },
            };
        public static Dictionary<string, eCourseDuration> eCourseDurationFromString =
            new Dictionary<string, eCourseDuration>
            {
                {  "Endless"     , eCourseDuration.ENDLESS      },
                {  "N days"      , eCourseDuration.N_DAYS       },
                {  "N receptions", eCourseDuration.N_RECEPTIONS },
            };
    }
}
