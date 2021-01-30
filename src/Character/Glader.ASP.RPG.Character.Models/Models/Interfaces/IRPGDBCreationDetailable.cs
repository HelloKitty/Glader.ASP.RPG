using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	//TODO: Move to Glader Essentials
	/// <summary>
	/// Contract for RPG DB models that are creation detailable.
	/// </summary>
	public interface IRPGDBCreationDetailable
	{
		/// <summary>
		/// Represents the timestamp of the creation.
		/// </summary>
		DateTime CreationDate { get; }
	}
}
