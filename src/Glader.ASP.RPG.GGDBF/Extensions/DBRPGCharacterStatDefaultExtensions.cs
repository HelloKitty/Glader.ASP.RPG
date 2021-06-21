using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	internal static class DBRPGCharacterStatDefaultCache<TStatType, TRaceType, TClassType> 
		where TStatType : Enum
	{
		public static IDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>> BaseStatsCache { get; } = new ConcurrentDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>>();
	}

	public static class DBRPGCharacterStatDefaultExtensions
	{
		/// <summary>
		/// Calculates the total base stats a specified <see cref="race"/> and <see cref="@class"/> combo would have at the specified <see cref="level"/>.
		/// </summary>
		/// <typeparam name="TStatType">The stat type.</typeparam>
		/// <typeparam name="TRaceType">The race type.</typeparam>
		/// <typeparam name="TClassType">The class type.</typeparam>
		/// <param name="table">DB table.</param>
		/// <param name="level">The level to calculate the base stats for.</param>
		/// <param name="race">The race.</param>
		/// <param name="class">The class.</param>
		/// <returns>Readonly map of stat types and values.</returns>
		public static IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>> CalculateBaseStats<TStatType, TRaceType, TClassType>(this IReadOnlyDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>> table,
			int level, TRaceType race, TClassType @class)
			where TRaceType : Enum
			where TClassType : Enum
			where TStatType : Enum
		{
			var cacheKey = new DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>(level, race, @class);
			if (DBRPGCharacterStatDefaultCache<TStatType, TRaceType, TClassType>.BaseStatsCache.ContainsKey(cacheKey))
				return DBRPGCharacterStatDefaultCache<TStatType, TRaceType, TClassType>.BaseStatsCache[cacheKey];

			BaseStatsCacheDictionary<TStatType> results = new();

			//For every level we sum up the total of the default (base) stats a character would have.
			table.Where(entry => entry.Value.Level <= level)
				.SelectMany(entry => entry.Value.Stats)
				.ForEach(results.AddStats);

			//Add to cache so we don't have to calculate every time.
			return DBRPGCharacterStatDefaultCache<TStatType, TRaceType, TClassType>.BaseStatsCache[cacheKey] = results;
		}
	}
}
