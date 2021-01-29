using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Represents the minimum required data to create a character.
	/// </summary>
	[JsonObject]
	public sealed class RPGCharacterCreationRequest
	{
		/// <summary>
		/// Requested name of the characters.
		/// </summary>
		[JsonProperty]
		public string Name { get; private set; }

		public RPGCharacterCreationRequest(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterCreationRequest()
		{
			
		}
	}
}
