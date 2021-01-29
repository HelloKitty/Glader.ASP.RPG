using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Database context adapter type.
	/// </summary>
	/// <typeparam name="TDBContextType">The requested DB Context type.</typeparam>
	public interface IDBContextAdapter<out TDBContextType>
		where TDBContextType : DbContext
	{
		/// <summary>
		/// DB Context Type.
		/// </summary>
		TDBContextType Context { get; }
	}
}
