using Glader.ASP.RPG;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.1.42.0")]
    public record DBRPGSItemSubClassKey<TItemClassType>(TItemClassType ItemClassId, System.Int32 SubClassId);
}