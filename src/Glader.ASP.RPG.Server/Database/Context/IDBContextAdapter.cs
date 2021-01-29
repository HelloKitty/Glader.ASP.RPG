using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Database context adapter type.
	/// </summary>
	/// <typeparam name="TDBContextType">The requested DB Context type.</typeparam>
	public interface IDBContextAdapter<out TDBContextType>
		where TDBContextType : RPGCharacterDatabaseContext
	{
		/// <summary>
		/// DB Context Type.
		/// </summary>
		TDBContextType Context { get; }
	}
}
