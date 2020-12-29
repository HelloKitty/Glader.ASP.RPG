using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Contract for a data model that represents a character's progress.
	/// </summary>
	public interface IRPGCharacterProgress
	{
		/// <summary>
		/// Represents the signed experience value of the character.
		/// </summary>
		int Experience { get; }

		/// <summary>
		/// Represents the level of the character.
		/// </summary>
		int Level { get; }

		/// <summary>
		/// Represents a duration of time that the character has played.
		/// </summary>
		TimeSpan PlayTime { get; }
	}
}
