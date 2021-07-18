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
    public partial class RPGStaticDataContext_DBRPGItemClass<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : DBRPGItemClass<TItemClassType>, IGGDBFSerializable where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
       where TItemClassType : System.Enum
       where TQualityType : System.Enum

    {
        [DataMemberAttribute(Order = 1)]
        public SerializableGGDBFCollection<DBRPGSItemSubClassKey<TItemClassType>, DBRPGSItemSubClass<TItemClassType>> _SerializedSubClasses;

        [IgnoreDataMemberAttribute]
        public override ICollection<DBRPGSItemSubClass<TItemClassType>> SubClasses
        {
            get => _SerializedSubClasses != null ? _SerializedSubClasses.Load(RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>.Instance.ItemSubClass) : base.SubClasses;
        }
        public RPGStaticDataContext_DBRPGItemClass() { }

        public void Initialize(IGGDBFDataConverter converter)
        {
            _SerializedSubClasses = GGDBFHelpers.CreateSerializableCollection(m => new DBRPGSItemSubClassKey<TItemClassType>(m.ItemClassId, m.SubClassId), SubClasses);
        }
    }
}