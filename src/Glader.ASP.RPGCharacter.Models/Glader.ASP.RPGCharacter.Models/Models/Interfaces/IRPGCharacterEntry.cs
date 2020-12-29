using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Contract for a data model that represents an RPG character entry.
	/// </summary>
	public interface IRPGCharacterEntry
	{
		/// <summary>
		/// Represents the RPG Character's id.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// Represents the RPG Character's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Represents the timestamp of the creation.
		/// </summary>
		DateTime CreationDate { get; }
	}
}
