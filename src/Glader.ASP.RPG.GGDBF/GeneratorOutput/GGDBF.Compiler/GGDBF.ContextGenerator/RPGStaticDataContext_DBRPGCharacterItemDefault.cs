using Glader.ASP.RPG;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.1.40.0")]
    [DataContractAttribute]
    public partial class RPGStaticDataContext_DBRPGCharacterItemDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : DBRPGCharacterItemDefault<TItemClassType, TQualityType, TQualityColorStructureType>, IGGDBFSerializable where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
       where TItemClassType : System.Enum
       where TQualityType : System.Enum

    {
        [IgnoreDataMemberAttribute]
        public override Glader.ASP.RPG.DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType> ItemTemplate
        {
            get => RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>.Instance.ItemTemplate[base.ItemTemplateId];
        }
        public RPGStaticDataContext_DBRPGCharacterItemDefault() { }

        public void Initialize(IGGDBFDataConverter converter)
        {
        }
    }
}