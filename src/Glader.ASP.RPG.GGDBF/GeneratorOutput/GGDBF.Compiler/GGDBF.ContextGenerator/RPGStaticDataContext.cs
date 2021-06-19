using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using GGDBF;
using Glader.ASP.RPG;

namespace Glader.ASP.RPG
{
    [GeneratedCodeAttribute("GGDBF", "0.0.19.0")]
    public interface IRPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> : IGGDBFContext where TSkillType : System.Enum
       where TRaceType : System.Enum
       where TClassType : System.Enum
       where TProportionSlotType : System.Enum
       where TCustomizableSlotType : System.Enum
       where TStatType : System.Enum
    {
        public IReadOnlyDictionary<TSkillType, DBRPGSkill<TSkillType>> Skill { get; init; }

        public IReadOnlyDictionary<TRaceType, DBRPGRace<TRaceType>> Race { get; init; }

        public IReadOnlyDictionary<TClassType, DBRPGClass<TClassType>> @Class { get; init; }

        public IReadOnlyDictionary<TCustomizableSlotType, DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>> CharacterCustomizationSlotType { get; init; }

        public IReadOnlyDictionary<TProportionSlotType, DBRPGCharacterProportionSlotType<TProportionSlotType>> CharacterProportionSlotType { get; init; }

        public IReadOnlyDictionary<TStatType, DBRPGStat<TStatType>> Stat { get; init; }

        public IReadOnlyDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>> CharacterStatDefault { get; init; }

    }

    [GeneratedCodeAttribute("GGDBF", "0.0.19.0")]
    public partial class RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> : IRPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> where TSkillType : System.Enum
        where TRaceType : System.Enum
        where TClassType : System.Enum
        where TProportionSlotType : System.Enum
        where TCustomizableSlotType : System.Enum
        where TStatType : System.Enum
    {
        public static RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> Instance { get; private set; }

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
                CharacterStatDefault = await source.RetrieveTableAsync<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>, RPGStaticDataContext_DBRPGCharacterStatDefault<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType>>(new NameOverrideTableRetrievalConfig<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>>("CharacterStatDefault") { KeyResolutionFunction = m => new DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>(m.Level, m.RaceId, m.ClassId) }),
            };
        }
    }
}