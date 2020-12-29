using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Default EF Core database-backed implementation of <see cref="IRPGCharacterRepository"/>
	/// </summary>
	public sealed class DefaultRPGCharacterRepository : GeneralGenericCrudRepositoryProvider<int, DBRPGCharacter>, IRPGCharacterRepository
	{
		public new RPGCharacterDatabaseContext Context { get; }

		public DefaultRPGCharacterRepository(RPGCharacterDatabaseContext context) 
			: base(context.Characters, context)
		{
			Context = context;
		}

		/// <inheritdoc />
		public async Task<DBRPGCharacter[]> RetrieveOwnedCharactersAsync(int ownershipId, CancellationToken token = default)
		{
			return await Context
				.CharacterOwnership
				.Where(o => o.OwnershipId == ownershipId)
				.Select(ownership => ownership.Character)
				.ToArrayAsync(token);
		}
	}
}
