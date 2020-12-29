using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPGCharacter
{
	[JsonObject]
	public sealed class RPGCharacterEntry : IRPGCharacterEntry
	{
		/// <inheritdoc />
		[JsonProperty]
		public int Id { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public string Name { get; private set; }


		public RPGCharacterEntry(int id, string name)
		{
			Id = id;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterEntry()
		{
			
		}
	}
}
