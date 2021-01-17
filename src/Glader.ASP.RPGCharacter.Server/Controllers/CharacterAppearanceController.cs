using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
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

		public CharacterAppearanceController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> appearanceRepository) 
			: base(claimsReader, logger)
		{
			AppearanceRepository = appearanceRepository ?? throw new ArgumentNullException(nameof(appearanceRepository));
		}

		/// <inheritdoc />
		[ProducesJson]
		[HttpGet("Characters/{id}")]
		public async Task<ResponseModel<RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, CharacterDataQueryResponseCode>> RetrieveCharacterAppearanceAsync(int characterId, CancellationToken token = default)
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
	}
}
