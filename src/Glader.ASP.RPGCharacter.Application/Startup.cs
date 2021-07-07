using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GGDBF;
using Glader.Essentials;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var mvcBuilder = services.AddControllers();

			services.AddTransient<EntityFrameworkGGDBFDataSource>();

			//TODO: Maybe put this in the RegisterGGDBF
			//GGDBF requires a datasource registered
			services.RegisterGladerRPGGGDBF<EntityFrameworkGGDBFDataSource, AutoMapperGGDBFDataConverter>(CreateRPGOptionsBuilder, mvcBuilder);

			services.RegisterGladerRPGSystem(builder =>
			{
				builder.UseMySql("server=127.0.0.1;port=3306;Database=glader.test;Uid=root;Pwd=test;", optionsBuilder =>
				{
					optionsBuilder.MigrationsAssembly(GetType().Assembly.FullName);
				});
			}, CreateRPGOptionsBuilder, mvcBuilder);

			services.RegisterGladerASP();

			//TODO: Support real certs.
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					//TODO: This is demo test signing code from: https://github.com/openiddict/openiddict-core/blob/7e1c9dd1307f8127288682459b0b1d82ea804e4f/src/OpenIddict.Server/OpenIddictServerBuilder.cs#L647
					using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
					store.Open(OpenFlags.ReadWrite);

					var subject = new X500DistinguishedName("CN=OpenIddict Server Signing Certificate");
					var certificate = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, subject.Name, validOnly: false)
						.OfType<X509Certificate2>()
						.SingleOrDefault();

					options.TokenValidationParameters
						.IssuerSigningKey = new X509SecurityKey(certificate);

					//TODO: This audience stuff is ALL WRONG.
					options.Audience = "auth-server";
					options.TokenValidationParameters.ValidIssuer = "https://localhost:5003/";
				});
		}

		private static RPGOptionsBuilder CreateRPGOptionsBuilder(RPGOptionsBuilder builder)
		{
			builder = builder with {RegisterAsNonGenericDBContext = true};

			return builder
				.WithCustomizationType<TestCustomizationSlotType, TestColorType>()
				.WithProportionType<TestProportionSlotType, TestVectorType<float>>()
				.WithRaceType<TestRaceType>()
				.WithClassType<TestClassType>()
				.WithSkillType<TestSkillType>()
				.WithStatType<TestStatType>()
				.WithItemClassType<TestItemClass>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
