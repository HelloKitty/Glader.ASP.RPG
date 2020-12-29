using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Contract for Types that can be linked to a character entry by the id.
	/// </summary>
	public interface ICharacterEntryLinkable
	{
		/// <summary>
		/// The character id.
		/// </summary>
		int Id { get; }
	}
}
