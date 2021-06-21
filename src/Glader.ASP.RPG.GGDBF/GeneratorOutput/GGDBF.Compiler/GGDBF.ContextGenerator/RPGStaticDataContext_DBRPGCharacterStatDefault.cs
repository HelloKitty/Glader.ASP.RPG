using Glader.ASP.RPG;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.0.30.0")]
    [DataContractAttribute]
    public partial class RPGStaticDataContext_DBRPGCharacterStatDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> : DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>, IGGDBFSerializable where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
    {
        [IgnoreDataMemberAttribute]
        public override DBRPGRace<TRaceType> Race
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>.Instance.Race[base.RaceId];
        }
        [IgnoreDataMemberAttribute]
        public override DBRPGClass<TClassType> Class
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>.Instance.@Class[base.ClassId];
        }
        public RPGStaticDataContext_DBRPGCharacterStatDefault() { }

        public void Initialize()
        {
        }
    }
}