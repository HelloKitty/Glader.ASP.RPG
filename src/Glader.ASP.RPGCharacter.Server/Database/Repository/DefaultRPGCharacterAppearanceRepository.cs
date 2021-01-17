﻿using System;
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
		private RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> Context { get; }

		public DefaultRPGCharacterAppearanceRepository(RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> context)
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
	}
}