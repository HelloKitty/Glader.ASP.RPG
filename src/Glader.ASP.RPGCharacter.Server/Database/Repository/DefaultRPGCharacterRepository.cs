using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Default EF Core database-backed implementation of <see cref="IRPGCharacterRepository"/>
	/// </summary>
	public sealed class DefaultRPGCharacterRepository<TCustomizableSlotType> : GeneralGenericCrudRepositoryProvider<int, DBRPGCharacter>, IRPGCharacterRepository 
		where TCustomizableSlotType : Enum
	{
		public new RPGCharacterDatabaseContext<TCustomizableSlotType> Context { get; }

		public DefaultRPGCharacterRepository(RPGCharacterDatabaseContext<TCustomizableSlotType> context) 
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

		public async Task<DBRPGCharacter> CreateCharacterAsync(int ownershipId, string name, CancellationToken token = default)
		{
			await using IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync(token);

			try
			{
				EntityEntry<DBRPGCharacter> entry = await Context
					.Characters
					.AddAsync(new DBRPGCharacter(name), token);

				await Context.SaveChangesAsync(token);

				//Now we link the character via the Ownership table
				await Context
					.CharacterOwnership
					.AddAsync(new DBRPGCharacterOwnership(ownershipId, entry.Entity.Id), token);

				await Context.SaveChangesAsync(token);
				await transaction.CommitAsync(token);

				return entry.Entity;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
