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
	/// character data services.
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface ICharacterCreationService<TRaceType, TClassType>
		where TRaceType : Enum
		where TClassType : Enum
	{
		/// <summary>
		/// REST method for attempting to create a new character
		/// with the data contained within <see cref="RPGCharacterCreationRequest{TRaceType,TClassType}"/>.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		[RequiresAuthentication]
		[Post("/api/CharacterData/Characters")]
		Task<ResponseModel<RPGCharacterCreationResult, CharacterCreationResponseCode>> CreateCharacterAsync([JsonBody] RPGCharacterCreationRequest<TRaceType, TClassType> request, CancellationToken token = default);
	}
}
