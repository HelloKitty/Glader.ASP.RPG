using Glader.ASP.RPG;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.0.13.0")]
    public record DBRPGStatDefaultKey<TStatType, TRaceType, TClassType>(System.Int32 Level, TRaceType RaceId, TClassType ClassId);
}