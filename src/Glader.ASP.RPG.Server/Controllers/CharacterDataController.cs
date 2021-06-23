using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Glader.ASP.RPG
{
	//TODO: If this controller 404's it's probably because it's generic. Cannot use [controller]
	[Route("api/CharacterData")]
	public sealed class CharacterDataController<TRaceType, TClassType> : AuthorizationReadyController, 
		ICharacterDataQueryService<TRaceType, TClassType>, ICharacterCreationService<TRaceType, TClassType>
		where TRaceType : Enum
		where TClassType : Enum
	{
		private IRPGCharacterRepository<TRaceType, TClassType> CharacterRepository { get; }

		public CharacterDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			IRPGCharacterRepository<TRaceType, TClassType> characterRepository) 
			: base(claimsReader, logger)
		{
			CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
		}

		/// <inheritdoc />
		[ProducesJson]
		[AuthorizeJwt]
		[HttpGet("Characters")]
		public async Task<RPGCharacterData<TRaceType, TClassType>[]> RetrieveCharactersDataAsync(CancellationToken token = default)
		{
			//TODO: Fix GetAccountId API
			int accountId = ClaimsReader.GetAccountId<int>(User);
			return (await CharacterRepository
					.RetrieveOwnedCharactersAsync(accountId, token))
				.Select(ConvertDbToTransit)
				.ToArray();
		}

		private RPGCharacterData<TRaceType, TClassType> ConvertDbToTransit(FullCharacterData<TRaceType, TClassType> characterData)
		{
			if (characterData == null) throw new ArgumentNullException(nameof(characterData));

			return new RPGCharacterData<TRaceType, TClassType>(new RPGCharacterEntry(characterData.Character.Id, characterData.Character.Name), new RPGCharacterCreationDetails(characterData.Character.CreationDate), new RPGCharacterProgress(characterData.Character.Progress.Experience, characterData.Character.Progress.Level, characterData.Character.Progress.PlayTime), characterData.Definition.Race.Id, characterData.Definition.Class.Id);
		}

		/// <inheritdoc />
		[ProducesJson]
		[HttpGet("Characters/{id}")]
		public async Task<ResponseModel<RPGCharacterData<TRaceType, TClassType>, CharacterDataQueryResponseCode>> 
			RetrieveCharacterDataAsync([FromRoute(Name = "id")] int characterId, CancellationToken token = default)
		{
			//TODO: Properly handle failure and return correct response codes.
			if (await CharacterRepository.ContainsAsync(characterId, token))
			{
				FullCharacterData<TRaceType, TClassType> characterData = await CharacterRepository.RetrieveFullCharacterDataAsync(characterId, token);
				return new ResponseModel<RPGCharacterData<TRaceType, TClassType>, CharacterDataQueryResponseCode>(ConvertDbToTransit(characterData));
			}
			else
				return new ResponseModel<RPGCharacterData<TRaceType, TClassType>, CharacterDataQueryResponseCode>(CharacterDataQueryResponseCode.CharacterDoesNotExist);
		}

		/// <inheritdoc />
		[ProducesJson]
		[AuthorizeJwt]
		[HttpGet]
		public async Task<int> RetrieveAuthorizedCharacterAsync(CancellationToken token = default)
		{
			//Idea here is that a principal used for authorization may contain the claim for
			//the subaccount id. The subaccount id is to be treated as the character id
			//in our backend.
			return ClaimsReader.GetCharacterId(User);
		}

		//TODO: We should constrain this to a SERVER or ADMIN role
		[HttpGet("Characters/{id}/account")]
		public async Task<ResponseModel<RPGCharacterAccountData, CharacterDataQueryResponseCode>> RetrieveAccountAsync([FromRoute(Name = "id")] int characterId, CancellationToken token = default)
		{
			if (!await CharacterRepository.ContainsAsync(characterId, token))
				return Failure<RPGCharacterAccountData, CharacterDataQueryResponseCode>(CharacterDataQueryResponseCode.CharacterDoesNotExist);

			int accountId = await CharacterRepository.RetrieveAssociatedAccountIdAsync(characterId, token);
			return Success<RPGCharacterAccountData, CharacterDataQueryResponseCode>(new RPGCharacterAccountData(accountId));
		}

		/// <inheritdoc />
		[ProducesJson]
		[AuthorizeJwt]
		[HttpGet("CharactersBasic")]
		public async Task<int[]> RetrieveCharacterBasicListAsync(CancellationToken token = default)
		{
			//TODO: Properly handle failure and return correct response codes.
			int accountId = ClaimsReader.GetAccountId<int>(User);

			return (await CharacterRepository
					.RetrieveOwnedCharactersAsync(accountId, token))
				.Select(d => d.Character.Id)
				.OrderBy(i => i)
				.ToArray();
		}

		/// <inheritdoc />
		[ProducesJson]
		[AuthorizeJwt]
		[HttpPost("Characters")]
		public async Task<ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>> 
			CreateCharacterAsync([FromBody] RPGCharacterCreationRequest<TRaceType, TClassType> request, CancellationToken token = default)
		{
			//TODO: Fix GetAccountId API
			int accountId = ClaimsReader.GetAccountId<int>(User);

			if (!ModelState.IsValid)
				return Failure<RPGCharacterCreationResult, CharacterCreationResponseCode>(CharacterCreationResponseCode.GeneralError);

			//TODO: Add validation pipeline.
			if (String.IsNullOrWhiteSpace(request.Name))
				return Failure<RPGCharacterCreationResult, CharacterCreationResponseCode>(CharacterCreationResponseCode.InvalidName);

			try
			{
				DBRPGCharacter character = await CharacterRepository.CreateCharacterAsync(accountId, request.Name, request.Race, request.ClassType, token);
				return Success<RPGCharacterCreationResult, CharacterCreationResponseCode>(new RPGCharacterCreationResult(character.Id));
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to create Character: {request} Reason: {e}");

				return Failure<RPGCharacterCreationResult, CharacterCreationResponseCode>(CharacterCreationResponseCode.GeneralError);
			}
		}
	}
}
