using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	[JsonObject]
	public sealed class RPGGroupCreationRequest
	{
		/// <summary>
		/// Requested name of the group.
		/// </summary>
		[JsonProperty]
		public string Name { get; private set; }

		/// <summary>
		/// Represents the requesting creator of the group (leader)
		/// </summary>
		[JsonProperty]
		public int CharacterId { get; private set; }

		public RPGGroupCreationRequest(string name, int characterId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			Name = name ?? throw new ArgumentNullException(nameof(name));
			CharacterId = characterId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGGroupCreationRequest()
		{
			
		}
	}
}
