using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	[JsonObject]
	public sealed class RPGCharacterCreationResult
	{
		/// <summary>
		/// The created character id.
		/// </summary>
		[JsonProperty]
		public int Id { get; private set; }

		public RPGCharacterCreationResult(int id)
		{
			if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
			Id = id;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterCreationResult()
		{
			
		}
	}
}
