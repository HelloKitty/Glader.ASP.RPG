using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Refit;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Contract for REST service that provides
	/// character appearance services.
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface ICharacterAppearanceService<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> 
		where TCustomizableSlotType : Enum 
		where TProportionSlotType : Enum
	{
		/// <summary>
		/// REST endpoint that gets all the <see cref="RPGCharacterCustomizationData{TCustomizableSlotType,TColorStructureType,TProportionSlotType,TProportionStructureType}"/> for the account associated with
		/// the Auth token supplied.
		/// </summary>
		/// <param name="characterId">The id of the character to query for.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>All customization data.</returns>
		[Get("/api/CharacterAppearance/Characters/{id}")]
		Task<ResponseModel<RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, CharacterDataQueryResponseCode>> RetrieveCharacterAppearanceAsync([AliasAs("id")] int characterId, CancellationToken token = default);

		/// <summary>
		/// REST endpoint that gets all the <see cref="RPGCharacterCustomizationData{TCustomizableSlotType,TColorStructureType,TProportionSlotType,TProportionStructureType}"/> for the account associated with
		/// the Auth token supplied.
		/// </summary>
		/// <param name="characterId">The id of the character to query for.</param>
		/// <param name="data">The customization data to save as the initial.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>Creates the initial customization data (can only be used on the initial creation).</returns>
		[RequiresAuthentication]
		[Post("/api/CharacterAppearance/Characters/{id}")]
		Task<CharacterDataQueryResponseCode> CreateCharacterAppearanceAsync([AliasAs("id")] int characterId, [JsonBody] RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> data, CancellationToken token = default);
	}
}
