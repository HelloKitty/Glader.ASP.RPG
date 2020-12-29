using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Glader.ASP.RPGCharacter
{
	[Route("api/[controller]")]
	public sealed class CharacterDataController : AuthorizationReadyController
	{
		public CharacterDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}
	}
}
