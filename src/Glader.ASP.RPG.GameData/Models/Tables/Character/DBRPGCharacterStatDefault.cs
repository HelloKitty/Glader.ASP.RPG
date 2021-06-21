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
	//TODO: Find a way to configure as owned without referencing EF Core.
	public record RPGStatDefinition<TStatType>(TStatType Stat, int Value)
		where TStatType : Enum
	{
		public static IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>> Empty { get; }

		static RPGStatDefinition()
		{
			var map = new Dictionary<TStatType, RPGStatDefinition<TStatType>>();
			foreach (var stat in Enum.GetValues(typeof(TStatType)).Cast<TStatType>())
				map[stat] = new RPGStatDefinition<TStatType>(stat, default);

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
		[DataMember(Order = 4)]
		public ICollection<RPGStatDefinition<TStatType>> Stats { get; private set; }

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
