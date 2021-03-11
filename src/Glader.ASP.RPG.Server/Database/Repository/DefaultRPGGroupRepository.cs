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
		public DefaultRPGGroupRepository(RPGCharacterDatabaseContext context) 
			: base(context.Groups, context)
		{

		}

		/// <inheritdoc />
		public async Task<bool> IsInGroupAsync(int characterId, CancellationToken token = default)
		{
			return await Context.Set<DBRPGGroupMember>()
				.AnyAsync(gm => gm.CharacterId == characterId, token);
		}

		/// <inheritdoc />
		public async Task AddMemberAsync(int groupId, int characterId, CancellationToken token = default)
		{
			DBRPGGroup group = await RetrieveAsync(groupId, token);

			//TODO: Does this work??
			group.Members
				.Add(new DBRPGGroupMember(groupId, characterId));

			await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task RemoveMemberAsync(int characterId, CancellationToken token = default)
		{
			var member = new DBRPGGroupMember(1, characterId);

			Context.Set<DBRPGGroupMember>().Attach(member);
			Context.Set<DBRPGGroupMember>().Remove(member);

			await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task DeleteAllGroupsAsync()
		{
			ModelSet.RemoveRange(ModelSet);
			await Context.SaveChangesAsync(true);
		}
	}
}
