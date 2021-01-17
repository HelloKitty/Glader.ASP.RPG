﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Data repository interface for <see cref="DBRPGCharacterCustomizableSlot{TCustomizableSlotType,TColorStructureType}"/> and <see cref="DBRPGCharacterProportionSlot{TProportionSlotType,TProportionStructureType}"/>
	/// </summary>
	/// <typeparam name="TCustomizableSlotType"></typeparam>
	/// <typeparam name="TColorStructureType"></typeparam>
	/// <typeparam name="TProportionSlotType"></typeparam>
	/// <typeparam name="TProportionStructureType"></typeparam>
	public interface IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
		where TProportionSlotType : Enum 
		where TCustomizableSlotType : Enum
	{
		/// <summary>
		/// Retrieves an array of all customized slots for the specified character.
		/// </summary>
		/// <param name="characterId"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>[]> RetrieveAllCustomizedSlotsAsync(int characterId, CancellationToken token = default);

		/// <summary>
		/// Retrieves an array of all proportioned slots for the specified character.
		/// </summary>
		/// <param name="characterId"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>[]> RetrieveAllProportionSlotsAsync(int characterId, CancellationToken token = default);

		/// <summary>
		/// Creates a slot for customized character data.
		/// </summary>
		/// <param name="slot"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<bool> CreateSlotAsync(DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType> slot, CancellationToken token = default);

		/// <summary>
		/// Creates a slot for proportioned character data.
		/// </summary>
		/// <param name="slot"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<bool> CreateSlotAsync(DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType> slot, CancellationToken token = default);
	}
}
