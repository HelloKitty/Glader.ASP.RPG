using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Simplified type interface for <see cref="IGenericRepositoryCrudable{TKey,TModel}"/>
	/// for model Type: <see cref="DBRPGGroup"/>.
	/// </summary>
	public interface IRPGGroupRepository : IGenericRepositoryCrudable<int, DBRPGGroup>, IEntireTableQueryable<DBRPGGroup>
	{
		/// <summary>
		/// Indicates if the character with the specified id is already in a group.
		/// </summary>
		/// <param name="characterId">The character id to check.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>True if the character is in a group.</returns>
		Task<bool> IsInGroupAsync(int characterId, CancellationToken token = default);

		/// <summary>
		/// Retrieves the <see cref="DBRPGGroupMember"/> that matches the <see cref="characterId"/>.
		/// </summary>
		/// <param name="characterId">The character id to retrieve the group member.</param>
		/// <param name="token"></param>
		/// <returns>The group memberbership.</returns>
		Task<DBRPGGroupMember> RetrieveGroupMemberAsync(int characterId, CancellationToken token = default);

		/// <summary>
		/// Adds a member to a group with the id <see cref="groupId"/> if it exists.
		/// </summary>
		/// <param name="groupId">The group id to add to.</param>
		/// <param name="characterId">The character to add.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		Task AddMemberAsync(int groupId, int characterId, CancellationToken token = default);

		/// <summary>
		/// Remove a member to a group with the character id <see cref="characterId"/>.
		/// </summary>
		/// <param name="characterId">The character id.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>The group the member was apart of.</returns>
		Task<DBRPGGroup> RemoveMemberAsync(int characterId, CancellationToken token = default);

		//Don't make this cancellable, that makes no sense
		/// <summary>
		/// Deletes all <see cref="DBRPGGroup"/> from the database.
		/// </summary>
		/// <returns></returns>
		Task DeleteAllGroupsAsync();
	}
}
