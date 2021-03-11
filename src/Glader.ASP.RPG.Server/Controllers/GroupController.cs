using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Glader.ASP.RPG
{
	//TODO: If this controller 404's it's probably because it's generic. Cannot use [controller]
	[Route("api/[controller]")]
	public sealed class GroupController : AuthorizationReadyController, IGroupManagementService
	{
		private IRPGGroupRepository GroupRepository { get; }

		public GroupController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, IRPGGroupRepository groupRepository) 
			: base(claimsReader, logger)
		{
			GroupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
		}

		//Because group state could always be changing we don't want to cache right?? Maybe we can implement cache invalidation on change?? But multiple servers who would that work?
		/// <inheritdoc />
		[NoResponseCache]
		[HttpGet("{id}")]
		public async Task<ResponseModel<RPGGroupData, GroupDataQueryResponseCode>> RetrieveGroupAsync([FromRoute(Name = "id")] int groupId, CancellationToken token = default)
		{
			if (!await GroupRepository.ContainsAsync(groupId, token))
				return Failure<RPGGroupData, GroupDataQueryResponseCode>(GroupDataQueryResponseCode.GroupDoesNotExist);

			//One known case where this can fail is if another instance of this server app modified the DB inbetween the last check.
			try
			{
				var group = await GroupRepository.RetrieveAsync(groupId, token);

				if (groupId != group.Id)
					if (Logger.IsEnabled(LogLevel.Warning))
						Logger.LogWarning($"Encountered {nameof(RetrieveGroupAsync)} where requested group: {groupId} did not match queried group. Queried group was: {group.Id}");

				return Success<RPGGroupData, GroupDataQueryResponseCode>(new RPGGroupData(group.Id, group.Members.Select(gm => gm.CharacterId).ToArray()));
			}
			catch (Exception e)
			{
				if (Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to {nameof(RetrieveGroupAsync)}. Reason: {e}");

				return Failure<RPGGroupData, GroupDataQueryResponseCode>(GroupDataQueryResponseCode.GeneralError);
			}
		}

		//TODO: We need a server authorization check
		/// <inheritdoc />
		[HttpPost]
		public async Task<ResponseModel<RPGGroupData, GroupCreationResponseCode>> CreateGroupAsync([FromBody] RPGGroupCreationRequest request, CancellationToken token = default)
		{
			if (await GroupRepository.IsInGroupAsync(request.CharacterId, token))
				return Failure<RPGGroupData, GroupCreationResponseCode>(GroupCreationResponseCode.AlreadyGrouped);

			//TODO: Create name validation object
			if (String.IsNullOrWhiteSpace(request.Name))
				return Failure<RPGGroupData, GroupCreationResponseCode>(GroupCreationResponseCode.InvalidName);

			try
			{
				var groupData = new DBRPGGroup(request.Name, String.Empty);
				bool createResult = await GroupRepository.TryCreateAsync(groupData, token);

				//At this point group id is initialized, or should be!!

				//TODO: If this is coming from a SERVER role authorized group we can trust the request input charId BUT if we ever directly allow group creation
				//client facing then we need to have a way to validate the owned character and that they have an active session for the character.
				if (createResult)
					return Success<RPGGroupData, GroupCreationResponseCode>(new RPGGroupData(groupData.Id, new int[1] {request.CharacterId}));

				return Failure<RPGGroupData, GroupCreationResponseCode>(GroupCreationResponseCode.GeneralError);
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to {nameof(CreateGroupAsync)}. Reason: {e}");

				return Failure<RPGGroupData, GroupCreationResponseCode>(GroupCreationResponseCode.GeneralError);
			}
		}

		//TODO: We need a server authorization check
		/// <inheritdoc />
		[HttpPut("{gid}/Member/{cid}")]
		public async Task<GroupMemberManageResponseCode> AddMemberAsync([FromRoute(Name = "gid")] int groupId, [FromRoute(Name = "cid")] int characterId, CancellationToken token = default)
		{
			if (!await GroupRepository.ContainsAsync(groupId, token))
				return GroupMemberManageResponseCode.GroupDoesNotExist;

			if (await GroupRepository.IsInGroupAsync(characterId, token))
				return GroupMemberManageResponseCode.AlreadyGrouped;

			try
			{
				await GroupRepository.AddMemberAsync(groupId, characterId, token);
				return GroupMemberManageResponseCode.Success;
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to {nameof(AddMemberAsync)}. Reason: {e}");

				return GroupMemberManageResponseCode.GeneralError;
			}
		}

		//TODO: We need a server authorization check
		/// <inheritdoc />
		[HttpDelete("Member/{cid}")]
		public async Task<GroupMemberManageResponseCode> RemoveMemberAsync(int characterId, CancellationToken token = default)
		{
			//If the users isn't in a group then we have nothing we can do.
			if (!await GroupRepository.IsInGroupAsync(characterId, token))
				return GroupMemberManageResponseCode.GroupDoesNotExist;

			await GroupRepository.RemoveMemberAsync(characterId, token);
			return GroupMemberManageResponseCode.Success;
		}

		//TODO: We need a server authorization check
		/// <inheritdoc />
		[HttpDelete]
		public async Task DisbandAllGroupsAsync(CancellationToken token = default)
		{
			await GroupRepository.DeleteAllGroupsAsync();
		}
	}
}
