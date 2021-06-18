﻿using System;
using System.Collections.Generic;
using GGDBF;

namespace Glader.ASP.RPG
{
	[RequiredDataModel(typeof(DBRPGSkill<>))]
	[RequiredDataModel(typeof(DBRPGRace<>))]
	[RequiredDataModel(typeof(DBRPGClass<>))]
	[RequiredDataModel(typeof(DBRPGCharacterCustomizableSlotType<>))]
	[RequiredDataModel(typeof(DBRPGCharacterProportionSlotType<>))]
	[RequiredDataModel(typeof(DBRPGStat<>))]
	[RequiredDataModel(typeof(DBRPGStatDefault<,,>))]
	public partial class RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType> 
		where TSkillType : Enum 
		where TRaceType : Enum
		where TClassType : Enum
		where TCustomizableSlotType : Enum
		where TProportionSlotType : Enum
		where TStatType : Enum
	{
		public IReadOnlyDictionary<TSkillType, DBRPGSkill<TSkillType>> Skill { get; init; }

		public IReadOnlyDictionary<TRaceType, DBRPGRace<TRaceType>> Race { get; init; }

		public IReadOnlyDictionary<TClassType, DBRPGClass<TClassType>> @Class { get; init; }

		public IReadOnlyDictionary<TCustomizableSlotType, DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>> CharacterCustomizationSlotType { get; init; }

		public IReadOnlyDictionary<TProportionSlotType, DBRPGCharacterProportionSlotType<TProportionSlotType>> CharacterProportionSlotType { get; init; }

		public IReadOnlyDictionary<TStatType, DBRPGStat<TStatType>> Stat { get; init; }

		public IReadOnlyDictionary<DBRPGStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGStatDefault<TStatType, TRaceType, TClassType>> StatDefault { get; init; }
	}
}
