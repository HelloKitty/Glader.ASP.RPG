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
		where TClassType : Enum 
		where TRaceType : Enum
	{
		private static object SyncObj = new object();

		private static WeakReference<IReadOnlyDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>>> TableWeakReference { get; set; } = new (null, false);

		private static IDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>> _BaseStatsCache { get; set; } = new ConcurrentDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>>();

		internal static IDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>> RetrieveStatsCache(IReadOnlyDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>> table)
		{
			//This should GENERALLY be really fast, just a ref compare and a return.
			//Slow case the cache is dirty and re recreate it.
			lock (SyncObj)
			{
				//Concept here is to check if it's the SAME table
				if(TableWeakReference.TryGetTarget(out var storedTable))
					if(Object.ReferenceEquals(storedTable, table))
						return _BaseStatsCache;

				//The table was out of date so we should clear the cache and update the weak reference for comparing.
				_BaseStatsCache = new ConcurrentDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>>();
				TableWeakReference = new WeakReference<IReadOnlyDictionary<DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>, DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>>>(table, false);
				return _BaseStatsCache;
			}
		}
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
			var cache = DBRPGCharacterStatDefaultCache<TStatType, TRaceType, TClassType>.RetrieveStatsCache(table);
			var cacheKey = new DBRPGCharacterStatDefaultKey<TStatType, TRaceType, TClassType>(level, race, @class);
			if (cache.ContainsKey(cacheKey))
				return cache[cacheKey];

			BaseStatsCacheDictionary<TStatType> results = new();

			//For every level we sum up the total of the default (base) stats a character would have.
			table.Where(entry => entry.Value.Level <= level)
				.SelectMany(entry => entry.Value.Stats)
				.ForEach(results.AddStats);

			//Add to cache so we don't have to calculate every time.
			return cache[cacheKey] = results;
		}
	}
}
