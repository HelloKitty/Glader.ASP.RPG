using System;
using System.Threading.Tasks;
using GGDBF;
using Refit;

namespace Glader.ASP.RPG.GGDBFStreamClientTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			RefitHttpGGDBFDataSource dataSource = new RefitHttpGGDBFDataSource($@"https://localhost:5001/");
			await dataSource.RetrieveTableAsync<TestClassType, DBRPGClass<TestClassType>>();
		}
	}
}
