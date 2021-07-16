using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	public enum ItemInstanceCreationResult
	{
		Success = GladerEssentialsModelConstants.RESPONSE_CODE_SUCCESS_VALUE,
		GeneralServerError = 2,
		RequestInvalid = 3,
		TemplateMissing = 4
	}

	/// <summary>
	/// Request model for creating an item instance.
	/// </summary>
	[JsonObject]
	public sealed record RPGCreateItemInstanceRequest([property: JsonProperty] int TemplateId);

	/// <summary>
	/// Response model for creating an item instance.
	/// Containing the instance id of the newly created item instance.
	/// </summary>
	[JsonObject]
	public sealed record RPGCreateItemInstanceResponse([property: JsonProperty] int InstanceId);
}
