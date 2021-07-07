using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Default database-backed implementation of <see cref="IRPGCharacterAppearanceRepository{TCustomizableSlotType,TColorStructureType,TProportionSlotType,TProportionStructureType}"/>
	/// </summary>
	/// <typeparam name="TCustomizableSlotType"></typeparam>
	/// <typeparam name="TColorStructureType"></typeparam>
	/// <typeparam name="TProportionSlotType"></typeparam>
	/// <typeparam name="TProportionStructureType"></typeparam>
	public sealed class DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> : IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> 
		where TCustomizableSlotType : Enum 
		where TProportionSlotType : Enum
	{
		private DbContext Context { get; }

		public DefaultRPGCharacterAppearanceRepository(IRPGDBContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			Context = context.Context ?? throw new ArgumentNullException(nameof(context.Context));
		}

		/// <inheritdoc />
		public async Task<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>[]> RetrieveAllCustomizedSlotsAsync(int characterId, CancellationToken token = default)
		{
			return await Context
				.Set<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>>()
				.Where(s => s.CharacterId == characterId)
				.ToArrayAsync(token);
		}

		/// <inheritdoc />
		public async Task<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>[]> RetrieveAllProportionSlotsAsync(int characterId, CancellationToken token = default)
		{
			return await Context
				.Set<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>>()
				.Where(s => s.CharacterId == characterId)
				.ToArrayAsync(token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotAsync(DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType> slot, CancellationToken token = default)
		{
			await Context
				.Set<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>>()
				.AddAsync(slot, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotAsync(DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType> slot, CancellationToken token = default)
		{
			await Context
				.Set<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>>()
				.AddAsync(slot, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotsAsync(DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>[] slots, CancellationToken token = default)
		{
			await Context
				.Set<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>>()
				.AddRangeAsync(slots, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotsAsync(DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>[] slots, CancellationToken token = default)
		{
			await Context
				.Set<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>>()
				.AddRangeAsync(slots, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken token = default)
		{
			return await Context
				.Database
				.BeginTransactionAsync(token);
		}
	}
}
