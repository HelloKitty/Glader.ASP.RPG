using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	public enum CharacterItemInventoryQueryResult
	{
		Success = GladerEssentialsModelConstants.RESPONSE_CODE_SUCCESS_VALUE,
		GeneralServerError = 2,
		Empty = 3,
		CharacterDoesntExist = 4
	}

	[JsonObject]
	public record RPGCharacterInventoryItemData([property: JsonProperty] int InstanceId, 
		[property: JsonProperty] int TemplateId);

	[JsonObject]
	public sealed record RPGCharacterItemInventoryResponse([property: JsonProperty] RPGCharacterInventoryItemData[] Items);
}
