using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Glader.ASP.RPGCharacter
{
	//With generic controllers, cannot use the [controller] thingy. Must use strict name
	[Route("api/CharacterAppearance")]
	public sealed class CharacterAppearanceController<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> 
		: AuthorizationReadyController, ICharacterAppearanceService<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
		where TCustomizableSlotType : Enum
		where TProportionSlotType : Enum
	{
		private IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> AppearanceRepository { get; }

		private IRPGCharacterRepository CharacterRepository { get; }

		public CharacterAppearanceController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> appearanceRepository, 
			IRPGCharacterRepository characterRepository) 
			: base(claimsReader, logger)
		{
			AppearanceRepository = appearanceRepository ?? throw new ArgumentNullException(nameof(appearanceRepository));
			CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
		}

		/// <inheritdoc />
		[ProducesJson]
		[HttpGet("Characters/{id}")]
		public async Task<ResponseModel<RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, CharacterDataQueryResponseCode>> RetrieveCharacterAppearanceAsync([FromRoute(Name = "id")] int characterId, CancellationToken token = default)
		{
			try
			{
				var slots = await AppearanceRepository.RetrieveAllCustomizedSlotsAsync(characterId, token);
				var proportionSlots = await AppearanceRepository.RetrieveAllProportionSlotsAsync(characterId, token);

				//Even if the character doesn't exist, we can just give an EMPTY appearance
				//Using the empty instance
				if(slots.Length == 0 && proportionSlots.Length == 0)
					return Success<RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, CharacterDataQueryResponseCode>(RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>.Empty);

				var customizationData = new RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>();

				foreach (var slot in slots)
				{
					customizationData.SlotData.Add(slot.SlotType, slot.CustomizationId);
					customizationData.SlotColorData.Add(slot.SlotType, slot.SlotColor);
				}

				foreach(var slot in proportionSlots)
					customizationData.ProportionData.Add(slot.SlotType, slot.Proportion);

				return Success<RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, CharacterDataQueryResponseCode>(customizationData);
			}
			catch (Exception e)
			{
				if (Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to query character appearance for Character: {characterId}. Reason: {e}");

				return Failure<RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, CharacterDataQueryResponseCode>(CharacterDataQueryResponseCode.GeneralError);
			}
		}

		[AuthorizeJwt]
		[ProducesJson]
		[HttpPost("Characters/{id}")]
		public async Task<CharacterDataQueryResponseCode> CreateCharacterAppearanceAsync([FromRoute(Name = "id")] int characterId, [FromBody] RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> data, CancellationToken token = default)
		{
			if (!ModelState.IsValid)
				return CharacterDataQueryResponseCode.GeneralError;

			int accountId = ClaimsReader.GetAccountId<int>(User);

			try
			{
				if(!await CharacterRepository.ContainsAsync(characterId, token))
					return CharacterDataQueryResponseCode.CharacterDoesNotExist;

				//Only allow users who own the character to create its appearance
				if(!await CharacterRepository.AccountOwnsCharacterAsync(accountId, characterId, token))
					return CharacterDataQueryResponseCode.NotAuthorized;

				//We scope the appearance persistence in a transaction because we don't want a HALF customized character.
				await using IDbContextTransaction transaction = await AppearanceRepository.CreateTransactionAsync(token);

				if(data.ProportionData.Count > 0)
					await AppearanceRepository.CreateSlotsAsync(data.ProportionData.Select(p => new DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>(characterId, p.Key, p.Value)).ToArray(), token);

				if(data.SlotData.Count > 0)
					await AppearanceRepository.CreateSlotsAsync(ConvertToCustomizableSlotData(characterId, data), token);

				await transaction.CommitAsync(token);

				return CharacterDataQueryResponseCode.Success;
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to create character appearance for Character: {characterId} Account: {accountId}. Reason: {e}");

				return CharacterDataQueryResponseCode.GeneralError;
			}
		}

		private static DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>[] ConvertToCustomizableSlotData(int characterId, RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> data)
		{
			return data
				.SlotData
				.Select(p => new DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>(characterId, p.Key, p.Value, GetSlotColorData(data, p))).ToArray();
		}

		private static TColorStructureType GetSlotColorData(RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> data, KeyValuePair<TCustomizableSlotType, int> p)
		{
			//TODO: Should we really new it??
			if (!data.SlotColorData.ContainsKey(p.Key))
				return Activator.CreateInstance<TColorStructureType>();

			if (data.SlotColorData[p.Key] == null)
				return Activator.CreateInstance<TColorStructureType>();

			return data.SlotColorData[p.Key];
		}
	}
}
