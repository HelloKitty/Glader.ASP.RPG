using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Enumerations of group creation response codes.
	/// </summary>
	public enum GroupCreationResponseCode
	{
		Success = GladerEssentialsModelConstants.RESPONSE_CODE_SUCCESS_VALUE,

		/// <summary>
		/// General/unknown error in creation.
		/// </summary>
		GeneralError = 2,

		/// <summary>
		/// The group creation is not authorized.
		/// (Ex. character is not associated with the authorization data0
		/// </summary>
		NotAuthorized = 3,

		/// <summary>
		/// Indicates that the character is already in a group.
		/// </summary>
		AlreadyGrouped = 4,

		/// <summary>
		/// Indicates that the name of group is invalid.
		/// </summary>
		InvalidName = 5,
	}
}
