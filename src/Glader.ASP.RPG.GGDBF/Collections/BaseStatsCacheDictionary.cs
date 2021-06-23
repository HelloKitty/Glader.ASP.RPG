using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	internal sealed class BaseStatsCacheDictionary<TStatType> : IReadOnlyDictionary<TStatType, RPGStatValue<TStatType>>
		where TStatType : Enum
	{
		public static int MaxSize { get; } = Enum.GetValues(typeof(TStatType)).Length;

		public Dictionary<TStatType, RPGStatValue<TStatType>> InternalMap { get; }

		public BaseStatsCacheDictionary(Dictionary<TStatType, RPGStatValue<TStatType>> internalMap)
		{
			InternalMap = internalMap;
		}

		public BaseStatsCacheDictionary()
			: this(new Dictionary<TStatType, RPGStatValue<TStatType>>(MaxSize))
		{

		}

		/// <summary>
		/// Do not call this method externally ever.
		/// (NOT THREAD-SAFE)
		/// </summary>
		/// <param name="statDefinition"></param>
		public void AddStats(RPGStatValue<TStatType> statDefinition)
		{
			if (statDefinition == null) throw new ArgumentNullException(nameof(statDefinition));

			if(this.ContainsKey(statDefinition.StatType))
				InternalMap[statDefinition.StatType] = new RPGStatValue<TStatType>(statDefinition.StatType, InternalMap[statDefinition.StatType].Value + statDefinition.Value);
			else
				InternalMap[statDefinition.StatType] = statDefinition;
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TStatType, RPGStatValue<TStatType>>> GetEnumerator()
		{
			return InternalMap.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) InternalMap).GetEnumerator();
		}

		/// <inheritdoc />
		public int Count => InternalMap.Count;

		/// <inheritdoc />
		public bool ContainsKey(TStatType key)
		{
			return InternalMap.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(TStatType key, out RPGStatValue<TStatType> value)
		{
			if (ContainsKey(key))
				return InternalMap.TryGetValue(key, out value);

			//Kinda crappy we have to allocate but it's likely Gen0
			value = RPGStatValue<TStatType>.Empty[key];
			return true;
		}

		/// <inheritdoc />
		public RPGStatValue<TStatType> this[TStatType key] => InternalMap[key];

		/// <inheritdoc />
		public IEnumerable<TStatType> Keys => InternalMap.Keys;

		/// <inheritdoc />
		public IEnumerable<RPGStatValue<TStatType>> Values => InternalMap.Values;
	}
}
