using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glader.ASP.RPG;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPGCharacter.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestDataController : ControllerBase
	{
		[HttpGet]
		public async Task<string> TestData([FromServices] DbContext context)
		{
			var classes = await context
				.Set<DBRPGItemClass<TestItemClass>>()
				.Include(m => m.SubClasses)
				.ToArrayAsync();

			return classes.Length.ToString();
		}
	}
}
