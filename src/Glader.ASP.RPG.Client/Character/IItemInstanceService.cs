using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Refit;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for REST service that provides
	/// item instance services
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface IItemInstanceService
	{
		//TODO: Require Role server authorization
		/// <summary>
		/// Attempts to create a new instance of an item with the specified information <see cref="request"/>.
		/// </summary>
		/// <param name="request">The item instance creation request.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>Result model.</returns>
		[RequiresAuthentication]
		[Post("/api/ItemInstance/Create")]
		Task<ResponseModel<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>> CreateItemAsync([JsonBody] RPGCreateItemInstanceRequest request, CancellationToken token = default);
	}
}
