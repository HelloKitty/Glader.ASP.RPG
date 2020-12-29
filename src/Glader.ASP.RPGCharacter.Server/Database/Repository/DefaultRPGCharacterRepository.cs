using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Default EF Core database-backed implementation of <see cref="IRPGCharacterRepository"/>
	/// </summary>
	public sealed class DefaultRPGCharacterRepository : GeneralGenericCrudRepositoryProvider<int, DBRPGCharacter>, IRPGCharacterRepository
	{
		public DefaultRPGCharacterRepository(RPGCharacterDatabaseContext context) 
			: base(context.Characters, context)
		{

		}
	}
}
