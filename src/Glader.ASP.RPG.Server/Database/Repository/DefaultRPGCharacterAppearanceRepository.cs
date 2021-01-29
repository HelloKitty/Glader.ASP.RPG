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
	/// <typeparam name="TRaceType"></typeparam>
	/// <typeparam name="TClassType"></typeparam>
	public sealed class DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType> : IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> 
		where TCustomizableSlotType : Enum 
		where TProportionSlotType : Enum
		where TRaceType : Enum
		where TClassType : Enum
	{
		private RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType> Context { get; }

		public DefaultRPGCharacterAppearanceRepository(RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType> context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <inheritdoc />
		public async Task<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>[]> RetrieveAllCustomizedSlotsAsync(int characterId, CancellationToken token = default)
		{
			return await Context
				.CustomizableSlots.Where(s => s.CharacterId == characterId)
				.ToArrayAsync(token);
		}

		/// <inheritdoc />
		public async Task<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>[]> RetrieveAllProportionSlotsAsync(int characterId, CancellationToken token = default)
		{
			return await Context
				.ProportionSlots.Where(s => s.CharacterId == characterId)
				.ToArrayAsync(token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotAsync(DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType> slot, CancellationToken token = default)
		{
			await Context
				.CustomizableSlots
				.AddAsync(slot, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotAsync(DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType> slot, CancellationToken token = default)
		{
			await Context
				.ProportionSlots
				.AddAsync(slot, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotsAsync(DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>[] slots, CancellationToken token = default)
		{
			await Context
				.CustomizableSlots
				.AddRangeAsync(slots, token);

			//If we changed a row, then it was added.
			return 0 < await Context.SaveChangesAsync(true, token);
		}

		/// <inheritdoc />
		public async Task<bool> CreateSlotsAsync(DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>[] slots, CancellationToken token = default)
		{
			await Context
				.ProportionSlots
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
