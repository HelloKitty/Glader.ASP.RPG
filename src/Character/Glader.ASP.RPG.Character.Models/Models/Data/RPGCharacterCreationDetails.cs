using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	[JsonObject]
	public sealed class RPGCharacterCreationDetails : IRPGCharacterCreationDetails
	{
		/// <inheritdoc />
		[JsonProperty]
		public DateTime CreationDate { get; private set; }

		public RPGCharacterCreationDetails(DateTime creationDate)
		{
			CreationDate = creationDate;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterCreationDetails()
		{
			
		}
	}
}
