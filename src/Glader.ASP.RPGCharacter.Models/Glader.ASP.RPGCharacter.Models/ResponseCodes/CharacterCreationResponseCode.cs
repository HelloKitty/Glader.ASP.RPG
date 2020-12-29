using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Enumerations of creation response codes.
	/// </summary>
	public enum CharacterCreationResponseCode
	{
		Success = GladerEssentialsModelConstants.RESPONSE_CODE_SUCCESS_VALUE,

		/// <summary>
		/// General/unknown error in creation.
		/// </summary>
		GeneralError = 2,

		/// <summary>
		/// The character data is data not authorized to query.
		/// </summary>
		NotAuthorized = 3,

		/// <summary>
		/// Indicates that no more available space exists for a new character.
		/// </summary>
		CharacterLimit = 4,

		/// <summary>
		/// Indicates that the name of the character is invalid.
		/// </summary>
		InvalidName = 5,
	}
}
