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
    }
}
