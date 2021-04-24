using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	[JsonObject]
	public sealed class RPGCharacterAccountData
	{
		[JsonProperty]
		public int AccountId { get; private set; }

		public RPGCharacterAccountData(int accountId)
		{
			if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId));
			AccountId = accountId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterAccountData()
		{

		}
	}
}
