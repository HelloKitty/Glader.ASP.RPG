using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	public interface IRPGCharacterCreationDetails
	{
		/// <summary>
		/// Represents the timestamp of the creation.
		/// </summary>
		DateTime CreationDate { get; }
	}
}
