using System;

namespace Glader.ASP.RPG
{
	//TODO: Move to Glader Essentials
	/// <summary>
	/// Contract for types that are self-describing/descriptable.
	/// </summary>
	public interface IModelDescriptable
	{
		/// <summary>
		/// The visual human-readable name for the model.
		/// </summary>
		string VisualName { get; }

		/// <summary>
		/// The description of the model.
		/// </summary>
		string Description { get; }
	}
}
