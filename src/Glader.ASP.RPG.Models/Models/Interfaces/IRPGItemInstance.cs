using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for an Item Instance entry.
	/// </summary>
	public interface IRPGItemInstance
	{
		/// <summary>
		/// The unique identifier for the item instance.
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// The id for the Item's template.
		/// </summary>
		public int TemplateId { get; }
	}

	[JsonObject]
	public record RPGItemInstance(int Id, int TemplateId) : IRPGItemInstance;
}
