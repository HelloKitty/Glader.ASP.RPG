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
		ICharacterDataQueryService, ICharacterCreationService<TRaceType, TClassType>
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
		public async Task<RPGCharacterData[]> RetrieveCharactersDataAsync(CancellationToken token = default)
		{
			//TODO: Fix GetAccountId API
			int accountId = ClaimsReader.GetAccountId<int>(User);
			return (await CharacterRepository
					.RetrieveOwnedCharactersAsync(accountId, token))
				.Select(ConvertDbToTransit)
				.ToArray();
		}

		private RPGCharacterData ConvertDbToTransit(DBRPGCharacter<TRaceType, TClassType> character)
		{
			if (character == null) throw new ArgumentNullException(nameof(character));

			return new RPGCharacterData(new RPGCharacterEntry(character.Id, character.Name), new RPGCharacterCreationDetails(character.CreationDate), new RPGCharacterProgress(character.Progress.Experience, character.Progress.Level, character.Progress.PlayTime));
		}

		/// <inheritdoc />
		[ProducesJson]
		[HttpGet("Characters/{id}")]
		public async Task<ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>> 
			RetrieveCharacterDataAsync([FromRoute(Name = "id")] int characterId, CancellationToken token = default)
		{
			//TODO: Properly handle failure and return correct response codes.
			if (await CharacterRepository.ContainsAsync(characterId, token))
			{
				DBRPGCharacter<TRaceType, TClassType> character = await CharacterRepository.RetrieveAsync(characterId, token);
				return new ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>(ConvertDbToTransit(character));
			}
			else
				return new ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>(CharacterDataQueryResponseCode.CharacterDoesNotExist);
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
				DBRPGCharacter<TRaceType, TClassType> character = await CharacterRepository.CreateCharacterAsync(accountId, request.Name, request.Race, request.ClassType, token);
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
