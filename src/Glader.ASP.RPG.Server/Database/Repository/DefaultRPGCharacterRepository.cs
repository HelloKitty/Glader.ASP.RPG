﻿using System;
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
	public sealed class DefaultRPGCharacterRepository<TRaceType, TClassType> : GeneralGenericCrudRepositoryProvider<int, DBRPGCharacter<TRaceType, TClassType>>, IRPGCharacterRepository<TRaceType, TClassType>
		where TRaceType : Enum 
		where TClassType : Enum
	{
		public new RPGCharacterDatabaseContext<TRaceType, TClassType> Context { get; }

		public DefaultRPGCharacterRepository(IDBContextAdapter<RPGCharacterDatabaseContext<TRaceType, TClassType>> contextAdapter) 
			: base(contextAdapter.Context.Set<DBRPGCharacter<TRaceType, TClassType>>(), contextAdapter.Context)
		{
			if (contextAdapter == null) throw new ArgumentNullException(nameof(contextAdapter));
			Context = contextAdapter.Context;
		}

		/// <inheritdoc />
		public async Task<DBRPGCharacter<TRaceType, TClassType>[]> RetrieveOwnedCharactersAsync(int ownershipId, CancellationToken token = default)
		{
			return await Context
				.CharacterOwnership
				.Where(o => o.OwnershipId == ownershipId)
				.Select(ownership => ownership.Character)
				.ToArrayAsync(token);
		}

		public async Task<DBRPGCharacter<TRaceType, TClassType>> CreateCharacterAsync(int ownershipId, string name, TRaceType race, TClassType classType, CancellationToken token = default)
		{
			await using IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync(token);

			try
			{
				EntityEntry<DBRPGCharacter<TRaceType, TClassType>> entry = await Context
					.Characters
					.AddAsync(new DBRPGCharacter<TRaceType, TClassType>(name, race, classType), token);

				await Context.SaveChangesAsync(token);

				//Now we link the character via the Ownership table
				await Context
					.CharacterOwnership
					.AddAsync(new DBRPGCharacterOwnership<TRaceType, TClassType>(ownershipId, entry.Entity.Id), token);

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
				.CharacterOwnership
				.AnyAsync(o => o.CharacterId == characterId && o.OwnershipId == ownershipId, token);
		}
	}
}
