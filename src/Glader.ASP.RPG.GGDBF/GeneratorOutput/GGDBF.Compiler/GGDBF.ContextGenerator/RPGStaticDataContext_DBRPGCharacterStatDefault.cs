using Glader.ASP.RPG;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.1.34.0")]
    [DataContractAttribute]
    public partial class RPGStaticDataContext_DBRPGCharacterStatDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> : DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>, IGGDBFSerializable where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
    {
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGRace<TRaceType> Race
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>.Instance.Race[base.RaceId];
        }
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGClass<TClassType> Class
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>.Instance.@Class[base.ClassId];
        }
        [DataMemberAttribute(Order = 1)]
        public RPGStaticDataContext_DBRPGCharacterStatDefault_RPGStatValue<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>[] _SerializedStats;

        [IgnoreDataMemberAttribute]
        public override ICollection<RPGStatValue<TStatType>> Stats
        {
            get => _SerializedStats != null ? _SerializedStats : base.Stats;
        }
        public RPGStaticDataContext_DBRPGCharacterStatDefault() { }

        public void Initialize(IGGDBFDataConverter converter)
        {
            _SerializedStats = converter.Convert<Glader.ASP.RPG.RPGStatValue<TStatType>, RPGStaticDataContext_DBRPGCharacterStatDefault_RPGStatValue<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>>(Stats);
        }
    }

    [GeneratedCodeAttribute("GGDBF", "0.1.34.0")]
    [DataContractAttribute]
    public partial record RPGStaticDataContext_DBRPGCharacterStatDefault_RPGStatValue<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> : RPGStatValue<TStatType>, IGGDBFSerializable where TSkillType : System.Enum
        where TRaceType : System.Enum
        where TClassType : System.Enum
        where TProportionSlotType : System.Enum
        where TCustomizableSlotType : System.Enum
        where TStatType : System.Enum
    {
        public void Initialize(IGGDBFDataConverter converter)
        {
        }
    }
}