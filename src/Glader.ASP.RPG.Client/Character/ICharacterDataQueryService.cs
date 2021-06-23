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
	public interface ICharacterDataQueryService
	{
		/// <summary>
		/// REST endpoint that gets the character id that the user is authorized for.
		/// </summary>
		/// <param name="token">Cancel token.</param>
		/// <returns>The id of the character in the Auth token claim.</returns>
		[RequiresAuthentication]
		[Get("/api/CharacterData")]
		Task<int> RetrieveAuthorizedCharacterAsync(CancellationToken token = default);

		//TODO: We should constrain this to a SERVER or ADMIN role
		[Get("/api/CharacterData/Characters/{id}/account")]
		Task<ResponseModel<RPGCharacterAccountData, CharacterDataQueryResponseCode>> RetrieveAccountAsync([AliasAs("id")] int characterId, CancellationToken token = default);

		/// <summary>
		/// Retrieves a basic integer array character list of all character associated with
		/// the authorized account.
		/// </summary>
		/// <param name="token">Cancel token.</param>
		/// <returns>Sorted array of character ids matching the authorized account.</returns>
		[RequiresAuthentication]
		[Get("/api/CharacterData/CharactersBasic/")]
		Task<int[]> RetrieveCharacterBasicListAsync(CancellationToken token = default);
	}

	/// <summary>
	/// Contract for REST service that provides
	/// character data query services.
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface ICharacterDataQueryService<TRaceType, TClassType> : ICharacterDataQueryService
		where TRaceType : Enum
		where TClassType : Enum
	{
		/// <summary>
		/// REST endpoint that gets all the <see cref="RPGCharacterData{TRaceType,TClassType}"/> for the account associated with
		/// the Auth token supplied.
		/// </summary>
		/// <param name="token">Cancel token.</param>
		/// <returns>An array of all RPG Character Data.</returns>
		[RequiresAuthentication]
		[Get("/api/CharacterData/Characters")]
		Task<RPGCharacterData<TRaceType, TClassType>[]> RetrieveCharactersDataAsync(CancellationToken token = default);

		/// <summary>
		/// REST endpoint that gets all the <see cref="RPGCharacterData{TRaceType,TClassType}"/> for the account associated with
		/// the Auth token supplied.
		/// </summary>
		/// <param name="characterId">The id of the character to query for.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>An array of all RPG Character Data.</returns>
		[Get("/api/CharacterData/Characters/{id}")]
		Task<ResponseModel<RPGCharacterData<TRaceType, TClassType>, CharacterDataQueryResponseCode>> RetrieveCharacterDataAsync([AliasAs("id")] int characterId, CancellationToken token = default);
	}
}
