using hello_blazor.Components;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Runtime;

namespace hello_blazor
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			/*
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();
			*/


			builder.Services.AddAuthentication("cookie")
				.AddCookie("cookie")
				.AddOpenIdConnect("meldrx", options =>
				{
					options.ClientId = "9b0756aff32f4b63b9190a0e6b3115cc";
					options.ClientSecret = "Si5JZz9YF4LmUCTenmqkTfduRmAVUPx";
					options.ResponseType = OpenIdConnectResponseType.Code;
					options.UsePkce = true;
					options.Scope.Clear();
					options.Scope.Add("offline_access");
					options.Scope.Add("profile ");
					options.Scope.Add("openid");
					options.Scope.Add("user/*.*");
					options.Authority = "https://app.meldrx.com/api/fhir/43e28cd9-d87a-4c43-afc2-bcf6ddfd8e88";

					options.SaveTokens = true;
					options.SignInScheme = "cookie";

					options.Events.OnRedirectToIdentityProvider = context =>
					{
						return Task.CompletedTask;
					};
				});

			var app = builder.Build();

			app.MapGet("/hello", () => Results.Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties(), ["meldrx"]));

			/*
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();
*/

			app.Run();
		}
	}
}
