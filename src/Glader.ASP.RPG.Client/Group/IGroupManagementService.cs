using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Refit;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for REST service that provides
	/// group management.
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface IGroupManagementService
	{
		//TODO: Move this to a Data Query service.
		/// <summary>
		/// Endpoint that retrieves group data for the group with id <see cref="groupId"/>.
		/// </summary>
		/// <param name="groupId">The group id to query.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		[Get("/api/Group/{id}")]
		Task<ResponseModel<RPGGroupData, GroupDataQueryResponseCode>> RetrieveGroupAsync([AliasAs("id")] int groupId, CancellationToken token = default);

		//TODO: This method should only be directly called by a SERVER. We need a way to define SERVER roles in authorization reqs.
		/// <summary>
		/// Attempts to create a group for the specified user's character.
		/// The specified characterId must be owned by the account associated with the authorization data.
		/// </summary>
		/// <param name="request">Group creation request data.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		[Post("/api/Group")]
		Task<ResponseModel<RPGGroupData, GroupCreationResponseCode>> CreateGroupAsync([JsonBody] RPGGroupCreationRequest request, CancellationToken token = default);

		//TODO: This method should only be directly called by a SERVER. We need a way to define SERVER roles in authorization reqs.
		/// <summary>
		/// Attempts to add a new member of the specified group with id <see cref="groupId"/>.
		/// </summary>
		/// <param name="groupId">The group to add <see cref="characterId"/> to.</param>
		/// <param name="characterId">The id of the character to add.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		[Put("/api/Group/{gid}/Member/{cid}")]
		Task<GroupMemberManageResponseCode> AddMemberAsync([AliasAs("gid")] int groupId, [AliasAs("cid")] int characterId, CancellationToken token = default);

		//TODO: This method should only be directly called by a SERVER. We need a way to define SERVER roles in authorization reqs.
		/// <summary>
		/// Attempts to remove a new member from their group.
		/// Group id is not required to remove them from their group.
		/// </summary>
		/// <param name="characterId">The id of the character to remove from their group.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		[Delete("/api/Group/Member/{cid}")]
		Task<GroupMemberManageResponseCode> RemoveMemberAsync([AliasAs("cid")] int characterId, CancellationToken token = default);

		//TODO: This method should only be directly called by a SERVER. We need a way to define SERVER roles in authorization reqs.
		/// <summary>
		/// Deletes all groups registered groups.
		/// </summary>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		[Delete("/api/Group")]
		Task DisbandAllGroupsAsync(CancellationToken token = default);
	}
}
