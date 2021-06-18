using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	internal sealed class BaseStatsCacheDictionary<TStatType> : IReadOnlyDictionary<TStatType, RPGStatDefinition<TStatType>>
		where TStatType : Enum
	{
		public static int MaxSize { get; } = Enum.GetValues(typeof(TStatType)).Length;

		public Dictionary<TStatType, RPGStatDefinition<TStatType>> InternalMap { get; }

		public BaseStatsCacheDictionary(Dictionary<TStatType, RPGStatDefinition<TStatType>> internalMap)
		{
			InternalMap = internalMap;
		}

		public BaseStatsCacheDictionary()
			: this(new Dictionary<TStatType, RPGStatDefinition<TStatType>>(MaxSize))
		{

		}

		/// <summary>
		/// Do not call this method externally ever.
		/// (NOT THREAD-SAFE)
		/// </summary>
		/// <param name="statDefinition"></param>
		public void AddStats(RPGStatDefinition<TStatType> statDefinition)
		{
			if (statDefinition == null) throw new ArgumentNullException(nameof(statDefinition));

			if(this.ContainsKey(statDefinition.Id))
				InternalMap[statDefinition.Id] = new RPGStatDefinition<TStatType>(statDefinition.Id, InternalMap[statDefinition.Id].Value + statDefinition.Value);
			else
				InternalMap[statDefinition.Id] = statDefinition;
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TStatType, RPGStatDefinition<TStatType>>> GetEnumerator()
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
		public bool TryGetValue(TStatType key, out RPGStatDefinition<TStatType> value)
		{
			if (ContainsKey(key))
				return InternalMap.TryGetValue(key, out value);

			//Kinda crappy we have to allocate but it's likely Gen0
			value = RPGStatDefinition<TStatType>.Empty[key];
			return true;
		}

		/// <inheritdoc />
		public RPGStatDefinition<TStatType> this[TStatType key] => InternalMap[key];

		/// <inheritdoc />
		public IEnumerable<TStatType> Keys => InternalMap.Keys;

		/// <inheritdoc />
		public IEnumerable<RPGStatDefinition<TStatType>> Values => InternalMap.Values;
	}
}
