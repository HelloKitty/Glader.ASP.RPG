using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Enumeration of response codes for group data.
	/// </summary>
	public enum GroupDataQueryResponseCode
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
		GroupDoesNotExist = 4,
	}
}
