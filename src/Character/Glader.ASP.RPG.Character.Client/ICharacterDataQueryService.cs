using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Refit;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for REST service that provides
	/// character data query services.
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface ICharacterDataQueryService
	{
		/// <summary>
		/// REST endpoint that gets all the <see cref="RPGCharacterData"/> for the account associated with
		/// the Auth token supplied.
		/// </summary>
		/// <param name="token">Cancel token.</param>
		/// <returns>An array of all RPG Character Data.</returns>
		[RequiresAuthentication]
		[Get("/api/CharacterData/Characters")]
		Task<RPGCharacterData[]> RetrieveCharactersDataAsync(CancellationToken token = default);

		/// <summary>
		/// REST endpoint that gets all the <see cref="RPGCharacterData"/> for the account associated with
		/// the Auth token supplied.
		/// </summary>
		/// <param name="characterId">The id of the character to query for.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>An array of all RPG Character Data.</returns>
		[Get("/api/CharacterData/Characters/{id}")]
		Task<ResponseModel<RPGCharacterData, CharacterDataQueryResponseCode>> RetrieveCharacterDataAsync([AliasAs("id")] int characterId, CancellationToken token = default);
	}
}
