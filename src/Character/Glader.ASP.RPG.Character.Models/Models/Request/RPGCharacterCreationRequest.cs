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
	public sealed class RPGCharacterCreationRequest<TRaceType, TClassType>
		where TRaceType : Enum
		where TClassType : Enum
	{
		/// <summary>
		/// Requested name of the characters.
		/// </summary>
		[JsonProperty]
		public string Name { get; private set; }

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

		public RPGCharacterCreationRequest(string name, TRaceType race, TClassType classType)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Race = race ?? throw new ArgumentNullException(nameof(race));
			ClassType = classType ?? throw new ArgumentNullException(nameof(classType));
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
