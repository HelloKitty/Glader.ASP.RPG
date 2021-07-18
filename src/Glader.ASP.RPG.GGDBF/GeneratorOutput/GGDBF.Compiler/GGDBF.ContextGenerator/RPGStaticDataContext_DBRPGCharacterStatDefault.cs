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
    [DataContractAttribute]
    public partial class RPGStaticDataContext_DBRPGCharacterStatDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>, IGGDBFSerializable where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
       where TItemClassType : System.Enum
       where TQualityType : System.Enum

    {
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGRace<TRaceType> Race
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>.Instance.Race[base.RaceId];
        }
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGClass<TClassType> Class
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>.Instance.@Class[base.ClassId];
        }
        [DataMemberAttribute(Order = 1)]
        public RPGStaticDataContext_DBRPGCharacterStatDefault_RPGStatValue<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>[] _SerializedStats;

        [IgnoreDataMemberAttribute]
        public override ICollection<RPGStatValue<TStatType>> Stats
        {
            get => _SerializedStats != null ? _SerializedStats : base.Stats;
        }
        public RPGStaticDataContext_DBRPGCharacterStatDefault() { }

        public void Initialize(IGGDBFDataConverter converter)
        {
            _SerializedStats = converter.Convert<Glader.ASP.RPG.RPGStatValue<TStatType>, RPGStaticDataContext_DBRPGCharacterStatDefault_RPGStatValue<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>(Stats);
        }
    }

    [GeneratedCodeAttribute("GGDBF", "0.1.42.0")]
    [DataContractAttribute]
    public partial record RPGStaticDataContext_DBRPGCharacterStatDefault_RPGStatValue<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : RPGStatValue<TStatType>, IGGDBFSerializable where TSkillType : System.Enum
        where TRaceType : System.Enum
        where TClassType : System.Enum
        where TProportionSlotType : System.Enum
        where TCustomizableSlotType : System.Enum
        where TStatType : System.Enum
        where TItemClassType : System.Enum
        where TQualityType : System.Enum

    {
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGStat<TStatType> Stat
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>.Instance.Stat[base.StatType];
        }

        public void Initialize(IGGDBFDataConverter converter)
        {
        }
    }
}