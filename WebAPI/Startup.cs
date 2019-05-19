using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Platform.Data;
using Platform.Models;

namespace WebAPI
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddOData();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			//Data Source=DESKTOP-TEEK14F;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
			var connection = @"Server=DESKTOP-TEEK14F;Database=RecruitmentOffice;Trusted_Connection=True;ConnectRetryCount=0";
			services.AddDbContext<DataContext>
				(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("WebAPI")));
			// BloggingContext requires
			// using EFGetStarted.AspNetCore.NewDb.Models;
			// UseSqlServer requires
			// using Microsoft.EntityFrameworkCore;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();


			

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseMvc(b =>
			{
				b.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
				b.MapODataServiceRoute("odata", "odata", GetEdmModel());
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}

		private static IEdmModel GetEdmModel()
		{
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<MedicalComissionResults>("MedicalComissionResults");
			builder.EntitySet<Recruit>("Recruits");
			builder.EntitySet<Schedule>("Schedules");
			builder.EntitySet<RecruitmentOffice>("RecruitmentOffices");
			builder.EntitySet<TroopType>("TroopTypes");
			builder.EntitySet<TroopKind>("TroopKinds");
			
			return builder.GetEdmModel();
		}
	}
}
