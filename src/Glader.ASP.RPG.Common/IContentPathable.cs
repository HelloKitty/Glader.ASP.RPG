using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for types that have a path to loadable content.
	/// </summary>
	public interface IContentPathable
	{
		/// <summary>
		/// Relative string content load path for the content.
		/// </summary>
		string ContentPath { get; }
	}
}
