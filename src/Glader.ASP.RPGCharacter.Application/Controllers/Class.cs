using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Glader.ASP.RPGCharacter
{
	[Route("test")]
	public class TestController : Controller
	{
		private RPGCharacterDatabaseContext Context { get; }

		public TestController(RPGCharacterDatabaseContext context)
		{
			Context = context;
		}

		[HttpPost]
		public async Task Test()
		{
			await Context.Characters.AddAsync(new DBRPGCharacter("Glader"));
			await Context.SaveChangesAsync(true);
		}
	}
}
