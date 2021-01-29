using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for a data model that represents an RPG character entry.
	/// </summary>
	public interface IRPGCharacterEntry : ICharacterEntryLinkable
	{
		/// <summary>
		/// Represents the RPG Character's name.
		/// </summary>
		string Name { get; }
	}
}
