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
			try
			{
				var dataSource = new RefitHttpGGDBFDataSource<RPGStaticDataContext<TestSkillType, TestRaceType, TestClassType, TestProportionSlotType, TestCustomizationSlotType, TestStatType>>($@"https://localhost:5001/", new RefitHttpGGDBFDataSourceOptions(true));
				var tables = await dataSource.RetrieveTableAsync<DBRPGCharacterStatDefaultKey<TestStatType, TestRaceType, TestClassType>, DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClassType>, RPGStaticDataContext_DBRPGCharacterStatDefault<TestSkillType, TestRaceType, TestClassType, TestProportionSlotType, TestCustomizationSlotType, TestStatType>>();
				Console.WriteLine($"Meep");

				await RPGStaticDataContext<TestSkillType, TestRaceType, TestClassType, TestProportionSlotType, TestCustomizationSlotType, TestStatType>.Initialize(dataSource);

				Console.WriteLine(RPGStaticDataContext<TestSkillType, TestRaceType, TestClassType, TestProportionSlotType, TestCustomizationSlotType, TestStatType>.Instance.Class.Count);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.ReadKey();
		}
	}
}
