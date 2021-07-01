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

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Default EF Core database-backed implementation of <see cref="IRPGCharacterRepository{TRaceType,TClassType}"/>
	/// </summary>
	public sealed class DefaultRPGCharacterRepository<TRaceType, TClassType> : GeneralGenericCrudRepositoryProvider<int, DBRPGCharacter>, IRPGCharacterRepository<TRaceType, TClassType>
		where TRaceType : Enum 
		where TClassType : Enum
	{
		public DefaultRPGCharacterRepository(IRPGDBContext contextAdapter) 
			: base(contextAdapter.Context.Set<DBRPGCharacter>(), contextAdapter.Context)
		{

		}

		/// <inheritdoc />
		public async Task<FullCharacterData<TRaceType, TClassType>[]> RetrieveOwnedCharactersAsync(int ownershipId, CancellationToken token = default)
		{
			//INCLUDE IS REQUIRED TO GET PROGRESS
			var fullCharacterDatas = await Context
				.Set<DBRPGCharacterOwnership>()
				.Include(o => o.Character).ThenInclude(o => o.Progress)
				.Where(o => o.OwnershipId == ownershipId)
				.Select(ownership => ownership.Character)
				.Join(Context.Set<DBRPGCharacterDefinition<TRaceType, TClassType>>()
						.Include(d => d.Race)
						.Include(d => d.Class),
					character => character.Id,
					def => def.Id,
					(character, def) => new { character, def })
				.ToArrayAsync(token);

			return fullCharacterDatas
				.Select(data => new FullCharacterData<TRaceType, TClassType>(data.character, data.def))
				.ToArray();
		}

		public async Task<DBRPGCharacter> CreateCharacterAsync(int ownershipId, string name, TRaceType race, TClassType classType, CancellationToken token = default)
		{
			await using IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync(token);

			try
			{
				EntityEntry<DBRPGCharacter> entry = await Context
					.Set<DBRPGCharacter>()
					.AddAsync(new DBRPGCharacter(name), token);

				await Context.SaveChangesAsync(token);

				//Now we link the character via the Ownership table
				await Context
					.Set<DBRPGCharacterOwnership>()
					.AddAsync(new DBRPGCharacterOwnership(ownershipId, entry.Entity.Id), token);

				//Now we add the race/class definition table entry (originally was apart of DBRPGCharacter but to keep things simple
				//typed we try to avoid generic type parameter carrying.
				await Context
					.Set<DBRPGCharacterDefinition<TRaceType, TClassType>>()
					.AddRangeAsync(new DBRPGCharacterDefinition<TRaceType, TClassType>(entry.Entity.Id, race, classType));

				await Context.SaveChangesAsync(token);
				await transaction.CommitAsync(token);

				return entry.Entity;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<bool> AccountOwnsCharacterAsync(int ownershipId, int characterId, CancellationToken token = default)
		{
			return await Context
				.Set<DBRPGCharacterOwnership>()
				.AnyAsync(o => o.CharacterId == characterId && o.OwnershipId == ownershipId, token);
		}

		/// <inheritdoc />
		public async Task<FullCharacterData<TRaceType, TClassType>> RetrieveFullCharacterDataAsync(int id, CancellationToken token = default)
		{
			var data = await Context
				.Set<DBRPGCharacter>()
				.Include(o => o.Progress)
				.Where(c => c.Id == id)
				.Join(Context.Set<DBRPGCharacterDefinition<TRaceType, TClassType>>()
						.Include(d => d.Race)
						.Include(d => d.Class),
					character => character.Id,
					def => def.Id,
					(character, def) => new { character, def })
				.FirstAsync(token);

			return new FullCharacterData<TRaceType, TClassType>(data.character, data.def);
		}

		/// <inheritdoc />
		public async Task<int> RetrieveAssociatedAccountIdAsync(int characterId, CancellationToken token = default)
		{
			var result = await Context.Set<DBRPGCharacterOwnership>().FirstOrDefaultAsync(co => co.CharacterId == characterId, token);
			return result.OwnershipId;
		}

		public override async Task<DBRPGCharacter> RetrieveAsync(int key, CancellationToken token = new CancellationToken(), bool includeNavigationProperties = false)
		{
			//INCLUDE IS REQUIRED TO GET PROGRESS
			return await Context
				.Set<DBRPGCharacter>()
				.Include(m => m.Progress)
				.FirstAsync(m => m.Id == key, token);
		}
	}
}
