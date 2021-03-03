using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contains the full character data for a character.
	/// </summary>
	[JsonObject]
	public sealed class RPGCharacterData<TRaceType, TClassType>
		where TRaceType : Enum
		where TClassType : Enum
	{
		/// <summary>
		/// The entry data for the character.
		/// </summary>
		[JsonProperty]
		public RPGCharacterEntry Entry { get; private set; }
		
		/// <summary>
		/// Details about the creation of the character.
		/// </summary>
		[JsonProperty]
		public RPGCharacterCreationDetails CreationDetails { get; private set; }

		/// <summary>
		/// The character's progress.
		/// </summary>
		[JsonProperty]
		public RPGCharacterProgress Progress { get; private set; }

		/// <summary>
		/// The race of the character.
		/// </summary>
		[JsonProperty]
		public TRaceType Race { get; private set; }

		/// <summary>
		/// The class of the character.
		/// </summary>
		[JsonProperty(PropertyName = @"Class")]
		public TClassType ClassType { get; private set; }

		public RPGCharacterData(RPGCharacterEntry entry, RPGCharacterCreationDetails creationDetails, RPGCharacterProgress progress, TRaceType race, TClassType classType)
		{
			Entry = entry ?? throw new ArgumentNullException(nameof(entry));
			CreationDetails = creationDetails ?? throw new ArgumentNullException(nameof(creationDetails));
			Progress = progress ?? throw new ArgumentNullException(nameof(progress));
			Race = race ?? throw new ArgumentNullException(nameof(race));
			ClassType = classType ?? throw new ArgumentNullException(nameof(classType));
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
