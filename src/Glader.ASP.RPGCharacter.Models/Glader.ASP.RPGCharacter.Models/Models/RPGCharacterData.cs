using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Contains the full character data for a character.
	/// </summary>
	[JsonObject]
	public sealed class RPGCharacterData
	{
		/// <summary>
		/// The entry data for the character.
		/// </summary>
		[JsonProperty]
		public RPGCharacterEntry Entry { get; private set; }

		/// <summary>
		/// The character's progress.
		/// </summary>
		[JsonProperty]
		public RPGCharacterProgress Progress { get; private set; }

		public RPGCharacterData(RPGCharacterEntry entry, RPGCharacterProgress progress)
		{
			Entry = entry ?? throw new ArgumentNullException(nameof(entry));
			Progress = progress ?? throw new ArgumentNullException(nameof(progress));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterData()
		{
			
		}
	}
}
