using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	//We don't send string name here because group names aren't required information
	//and can go through the NameQuery system.
	/// <summary>
	/// Represents the basic group data.
	/// </summary>
	[JsonObject]
	public sealed class RPGGroupData
	{
		/// <summary>
		/// The group id.
		/// </summary>
		[JsonProperty]
		public int Id { get; private set; }

		/// <summary>
		/// The character ids apart of the group.
		/// </summary>
		[JsonProperty]
		public int[] MemberIds { get; private set; } = Array.Empty<int>();

		public RPGGroupData(int id, int[] memberIds)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

			Id = id;
			MemberIds = memberIds ?? throw new ArgumentNullException(nameof(memberIds));
		}

		/// <summary>
		/// Creates an empty group with the specified group id <see cref="id"/>.
		/// </summary>
		/// <param name="id">The group id.</param>
		public RPGGroupData(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
			Id = id;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGGroupData()
		{

		}
	}
}
