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

namespace Glader.ASP.RPGCharacter
{
	[Route("api/[controller]")]
	public sealed class CharacterDataController : AuthorizationReadyController, 
		ICharacterDataQueryService, ICharacterCreationService
	{
		private IRPGCharacterRepository CharacterRepository { get; }

		public CharacterDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			IRPGCharacterRepository characterRepository) 
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

		private RPGCharacterData ConvertDbToTransit(DBRPGCharacter character)
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
				DBRPGCharacter character = await CharacterRepository.RetrieveAsync(characterId, token);
				return new ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>(ConvertDbToTransit(character));
			}
			else
				return new ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>(CharacterDataQueryResponseCode.CharacterDoesNotExist);
		}

		/// <inheritdoc />
		[ProducesJson]
		[AuthorizeJwt]
		[HttpPost("Characters")]
		public async Task<ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>> CreateCharacterAsync([FromBody] RPGCharacterCreationRequest request, CancellationToken token = default)
		{
			//TODO: Fix GetAccountId API
			int accountId = ClaimsReader.GetAccountId<int>(User);

			if (!ModelState.IsValid)
				return new ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>(CharacterCreationResponseCode.GeneralError);

			//TODO: Add validation pipeline.
			if (String.IsNullOrWhiteSpace(request.Name))
				return new ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>(CharacterCreationResponseCode.InvalidName);

			//TODO: Better handling and response code implementation.
			try
			{
				DBRPGCharacter character = await CharacterRepository.CreateCharacterAsync(accountId, request.Name, token);
				return new ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>(new RPGCharacterCreationResult(character.Id));
			}
			catch (Exception e)
			{
				return new ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>(CharacterCreationResponseCode.GeneralError);
			}
		}
	}
}
