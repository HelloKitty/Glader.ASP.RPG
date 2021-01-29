using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Enumerations of data query response codes.
	/// </summary>
	public enum CharacterDataQueryResponseCode
	{
		Success = GladerEssentialsModelConstants.RESPONSE_CODE_SUCCESS_VALUE,

		/// <summary>
		/// General/unknown error in query.
		/// </summary>
		GeneralError = 2,

		/// <summary>
		/// The character data is data not authorized to query.
		/// </summary>
		NotAuthorized = 3,

		/// <summary>
		/// The requested character does not exist.
		/// </summary>
		CharacterDoesNotExist = 4,
	}
}
