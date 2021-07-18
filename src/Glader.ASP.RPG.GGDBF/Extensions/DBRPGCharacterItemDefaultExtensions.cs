using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	public static class DBRPGCharacterItemDefaultExtensions
	{
		/// <summary>
		/// Enumerates the default items for the specified race and class combination.
		/// </summary>
		/// <typeparam name="TRaceType"></typeparam>
		/// <typeparam name="TClassType"></typeparam>
		/// <typeparam name="TItemClassType"></typeparam>
		/// <typeparam name="TQualityType"></typeparam>
		/// <typeparam name="TQualityColorStructureType"></typeparam>
		/// <param name="table">Table.</param>
		/// <param name="race">The race.</param>
		/// <param name="class">The class.</param>
		/// <returns>Enumerable of item templates.</returns>
		public static IEnumerable<DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType>> EnumerateDefaultItems<TRaceType, TClassType, TItemClassType, TQualityType, TQualityColorStructureType>(this IReadOnlyDictionary<int, DBRPGCharacterItemDefault<TRaceType, TClassType, TItemClassType, TQualityType, TQualityColorStructureType>> table, TRaceType race, TClassType @class) 
			where TItemClassType : Enum 
			where TQualityType : Enum
			where TRaceType : Enum
			where TClassType : Enum
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (race == null) throw new ArgumentNullException(nameof(race));
			if (@class == null) throw new ArgumentNullException(nameof(@class));

			foreach(var entry in table.Values)
				if (Equals(entry.ClassId, @class) && Equals(entry.RaceId, race))
					yield return entry.ItemTemplate;
		}
	}
}
