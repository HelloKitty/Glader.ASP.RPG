using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// <see cref="IGenericRepositoryCrudable{TKey,TModel}"/> for <see cref="DBRPGCharacter"/>.
	/// </summary>
	public interface IRPGCharacterRepository : IGenericRepositoryCrudable<int, DBRPGCharacter>
	{

	}
}
