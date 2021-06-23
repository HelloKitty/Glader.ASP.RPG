using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using GGDBF;

namespace Glader.ASP.RPG
{
	[OwnedTypeHint]
	public record RPGStatValue<TStatType>(TStatType StatType = default, int Value = default)
		where TStatType : Enum
	{
		public static IReadOnlyDictionary<TStatType, RPGStatValue<TStatType>> Empty { get; }

		//Foreign key is setup in code (due to issues with annotations when trying to generate)
		[ForeignKeyHint(nameof(StatType), nameof(Stat))]
		public virtual DBRPGStat<TStatType> Stat { get; private set; }

		static RPGStatValue()
		{
			var map = new Dictionary<TStatType, RPGStatValue<TStatType>>();
			foreach (var stat in Enum.GetValues(typeof(TStatType)).Cast<TStatType>())
				map[stat] = new RPGStatValue<TStatType>(stat, default);

			Empty = map;
		}
	};

	/// <summary>
	/// Table for for default stats gained based on race/class/level.
	/// Each entry represents the stats added when reaching the level (not summed)
	/// </summary>
	/// <typeparam name="TStatType">The stat type.</typeparam>
	/// <typeparam name="TRaceType">The race type.</typeparam>
	/// <typeparam name="TClassType">The class type.</typeparam>
	[DataContract]
	[CompositeKeyHint(nameof(Level), nameof(RaceId), nameof(ClassId))]
	[Table("character_stat_default")]
	public class DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>
		where TStatType : Enum 
		where TRaceType : Enum
		where TClassType : Enum
	{
		/// <summary>
		/// The level for the default stat.
		/// </summary>
		[DataMember(Order = 1)]
		public int Level { get; private set; }

		[DataMember(Order = 2)]
		public TRaceType RaceId { get; private set; }

		[IgnoreDataMember]
		[ForeignKey(nameof(RaceId))]
		public virtual DBRPGRace<TRaceType> Race { get; private set; }

		[DataMember(Order = 3)]
		public TClassType ClassId { get; private set; }

		[IgnoreDataMember]
		[ForeignKey(nameof(ClassId))]
		public virtual DBRPGClass<TClassType> @Class { get; private set; }

		//Serializable OwnedType collection of stats
		[OwnedTypeHint]
		[IgnoreDataMember]
		public virtual ICollection<RPGStatValue<TStatType>> Stats { get; private set; }

		public DBRPGCharacterStatDefault(int level, TRaceType raceId, TClassType classId)
		{
			if (level <= 0) throw new ArgumentOutOfRangeException(nameof(level));
			Level = level;
			RaceId = raceId ?? throw new ArgumentNullException(nameof(raceId));
			ClassId = classId ?? throw new ArgumentNullException(nameof(classId));
		}

		public DBRPGCharacterStatDefault()
		{
			
		}
	}
}
