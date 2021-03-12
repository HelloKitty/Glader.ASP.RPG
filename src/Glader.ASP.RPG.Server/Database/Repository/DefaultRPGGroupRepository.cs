using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Default DB/EF Core backed implementation of <see cref="IRPGGroupRepository"/>.
	/// </summary>
	public sealed class DefaultRPGGroupRepository : GeneralGenericCrudRepositoryProvider<int, DBRPGGroup>, IRPGGroupRepository
	{
		public DefaultRPGGroupRepository(IDBContextAdapter<RPGCharacterDatabaseContext> contextAdapter) 
			: base(contextAdapter.Context.Groups, contextAdapter.Context)
		{

		}

		/// <inheritdoc />
		public async Task<bool> IsInGroupAsync(int characterId, CancellationToken token = default)
		{
			return await Context.Set<DBRPGGroupMember>()
				.AnyAsync(gm => gm.CharacterId == characterId, token);
		}

		public override async Task<DBRPGGroup> RetrieveAsync(int key, CancellationToken token = default, bool includeNavigationProperties = false)
		{
			if (includeNavigationProperties)
				return await ModelSet
					.Include(g => g.Members)
					.FirstAsync(g => g.Id == key, token);
			else
				return await ModelSet.FindAsync(new object[] { key }, token);
		}

		/// <inheritdoc />
		public async Task AddMemberAsync(int groupId, int characterId, CancellationToken token = default)
		{
			DBRPGGroup group = await RetrieveAsync(groupId, token, true);

			//TODO: Does this work??
			group.Members
				.Add(new DBRPGGroupMember(groupId, characterId));

			await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<DBRPGGroup> RemoveMemberAsync(int characterId, CancellationToken token = default)
		{
			//If there is only one member then we should disband the group
			//But we NEED TO KNOW that the group was disbanded for future proof realtime social notifications
			//involving group listing and possibily other stuff.
			var groupMember = await Context.Set<DBRPGGroupMember>()
				.Include(gm => gm.Group)
				.ThenInclude(g => g.Members)
				.FirstAsync(gm => gm.CharacterId == characterId, token);

			groupMember.Group
				.Members
				.Remove(groupMember);

			await Context.SaveChangesAsync(true, token);

			return groupMember.Group;
		}

		/// <inheritdoc />
		public async Task DeleteAllGroupsAsync()
		{
			ModelSet.RemoveRange(ModelSet);
			await Context.SaveChangesAsync(true);
		}
	}
}
