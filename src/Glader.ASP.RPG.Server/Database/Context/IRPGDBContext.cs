using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Database context adapter type.
	/// </summary>
	public interface IRPGDBContext
	{
		/// <summary>
		/// DB Context.
		/// </summary>
		DbContext Context { get; }
	}
}
