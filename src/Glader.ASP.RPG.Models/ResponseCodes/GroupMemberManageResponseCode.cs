using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	public enum GroupMemberManageResponseCode
	{
		Success = GladerEssentialsModelConstants.RESPONSE_CODE_SUCCESS_VALUE,

		/// <summary>
		/// General/unknown error in query.
		/// </summary>
		GeneralError = 2,

		/// <summary>
		/// Not authorized for action.
		/// </summary>
		NotAuthorized = 3,

		/// <summary>
		/// The group does not exist.
		/// </summary>
		GroupDoesNotExist = 4,

		/// <summary>
		/// The potential member cannot join because they are already grouped.
		/// </summary>
		AlreadyGrouped = 5,
	}
}
