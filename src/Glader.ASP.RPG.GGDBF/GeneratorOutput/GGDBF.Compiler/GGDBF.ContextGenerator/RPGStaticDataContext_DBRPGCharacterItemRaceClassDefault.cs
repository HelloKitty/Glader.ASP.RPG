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
    public partial class RPGStaticDataContext_DBRPGCharacterItemRaceClassDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : DBRPGCharacterItemRaceClassDefault<TRaceType, TClassType, TItemClassType, TQualityType, TQualityColorStructureType>, IGGDBFSerializable where TSkillType : System.Enum
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
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType> ItemTemplate
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>.Instance.ItemTemplate[base.ItemTemplateId];
        }
        public RPGStaticDataContext_DBRPGCharacterItemRaceClassDefault() { }

        public void Initialize(IGGDBFDataConverter converter)
        {
        }
    }
}