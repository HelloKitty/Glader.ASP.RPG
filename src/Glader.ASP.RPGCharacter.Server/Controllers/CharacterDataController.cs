using System;
using System.Collections.Generic;
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
	public sealed class CharacterDataController : AuthorizationReadyController, ICharacterDataQueryService
	{
		private RPGCharacterDatabaseContext Context { get; }

		public CharacterDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			RPGCharacterDatabaseContext context) 
			: base(claimsReader, logger)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		[ProducesJson]
		public async Task<RPGCharacterData[]> RetrieveCharactersDataAsync(CancellationToken token = default)
		{
			throw new NotImplementedException();
		}

		[ProducesJson]
		[HttpGet("Characters/{id}")]
		public async Task<ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>> 
			RetrieveCharacterDataAsync([FromRoute(Name = "id")] int characterId, CancellationToken token = default)
		{
			if (await Context.Characters.AnyAsync(c => c.Id == characterId, token))
			{
				DBRPGCharacter character = await Context.Characters.FindAsync(characterId);
				return new ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>(new RPGCharacterData(new RPGCharacterEntry(character.Id, character.Name), new RPGCharacterCreationDetails(character.CreationDate), new RPGCharacterProgress(character.Progress.Experience, character.Progress.Level, character.Progress.PlayTime)));
			}
			else
				return new ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>(CharacterDataQueryResponseCode.CharacterDoesNotExist);
		}
	}
}
