using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;
using Glader.ASP.RPG;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.1.35.0")]
    public interface IRPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : IGGDBFContext where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
       where TItemClassType : System.Enum
       where TQualityType : System.Enum

    {
        public IReadOnlyDictionary<TSkillType, DBRPGSkill<TSkillType>> Skill { get; }

        public IReadOnlyDictionary<TRaceType, DBRPGRace<TRaceType>> Race { get; }

        public IReadOnlyDictionary<TClassType, DBRPGClass<TClassType>> @Class { get; }

        public IReadOnlyDictionary<TCustomizableSlotType, DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>> CharacterCustomizationSlotType { get; }

        public IReadOnlyDictionary<TProportionSlotType, DBRPGCharacterProportionSlotType<TProportionSlotType>> CharacterProportionSlotType { get; }

        public IReadOnlyDictionary<TStatType, DBRPGStat<TStatType>> Stat { get; }

        public IReadOnlyDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>> CharacterStatDefault { get; }

        public IReadOnlyDictionary<TItemClassType, DBRPGItemClass<TItemClassType>> ItemClass { get; }

        public IReadOnlyDictionary<DBRPGSItemSubClassKey<TItemClassType>, DBRPGSItemSubClass<TItemClassType>> ItemSubClass { get; }

        public IReadOnlyDictionary<TQualityType, DBRPGQuality<TQualityType, TQualityColorStructureType>> Quality { get; }

    }

    [GeneratedCodeAttribute("GGDBF", "0.1.35.0")]
    public partial class RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> : IRPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> where TSkillType : System.Enum
        where TRaceType : System.Enum
        where TClassType : System.Enum
        where TProportionSlotType : System.Enum
        where TCustomizableSlotType : System.Enum
        where TStatType : System.Enum
        where TItemClassType : System.Enum
        where TQualityType : System.Enum

    {
        public static RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> Instance { get; private set; }

        public static async Task Initialize(IGGDBFDataSource source)
        {
            Instance = new()
            {
                Skill = await source.RetrieveTableAsync<TSkillType, DBRPGSkill<TSkillType>>(new NameOverrideTableRetrievalConfig<TSkillType, DBRPGSkill<TSkillType>>("Skill")),
                Race = await source.RetrieveTableAsync<TRaceType, DBRPGRace<TRaceType>>(new NameOverrideTableRetrievalConfig<TRaceType, DBRPGRace<TRaceType>>("Race")),
                @Class = await source.RetrieveTableAsync<TClassType, DBRPGClass<TClassType>>(new NameOverrideTableRetrievalConfig<TClassType, DBRPGClass<TClassType>>("Class")),
                CharacterCustomizationSlotType = await source.RetrieveTableAsync<TCustomizableSlotType, DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>>(new NameOverrideTableRetrievalConfig<TCustomizableSlotType, DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>>("CharacterCustomizationSlotType")),
                CharacterProportionSlotType = await source.RetrieveTableAsync<TProportionSlotType, DBRPGCharacterProportionSlotType<TProportionSlotType>>(new NameOverrideTableRetrievalConfig<TProportionSlotType, DBRPGCharacterProportionSlotType<TProportionSlotType>>("CharacterProportionSlotType")),
                Stat = await source.RetrieveTableAsync<TStatType, DBRPGStat<TStatType>>(new NameOverrideTableRetrievalConfig<TStatType, DBRPGStat<TStatType>>("Stat")),
                CharacterStatDefault = await source.RetrieveTableAsync<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>, RPGStaticDataContext_DBRPGCharacterStatDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>(new NameOverrideTableRetrievalConfig<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>>("CharacterStatDefault") { KeyResolutionFunction = m => new DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>(m.Level, m.RaceId, m.ClassId) }),
                ItemClass = await source.RetrieveTableAsync<TItemClassType, DBRPGItemClass<TItemClassType>, RPGStaticDataContext_DBRPGItemClass<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>(new NameOverrideTableRetrievalConfig<TItemClassType, DBRPGItemClass<TItemClassType>>("ItemClass")),
                ItemSubClass = await source.RetrieveTableAsync<DBRPGSItemSubClassKey<TItemClassType>, DBRPGSItemSubClass<TItemClassType>, RPGStaticDataContext_DBRPGSItemSubClass<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>(new NameOverrideTableRetrievalConfig<DBRPGSItemSubClassKey<TItemClassType>, DBRPGSItemSubClass<TItemClassType>>("ItemSubClass") { KeyResolutionFunction = m => new DBRPGSItemSubClassKey<TItemClassType>(m.ItemClassId, m.SubClassId) }),
                Quality = await source.RetrieveTableAsync<TQualityType, DBRPGQuality<TQualityType, TQualityColorStructureType>>(new NameOverrideTableRetrievalConfig<TQualityType, DBRPGQuality<TQualityType, TQualityColorStructureType>>("Quality")),
            };
        }
    }
}