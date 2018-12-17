using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Globomatics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Globomatics
{
   public class Startup
   {
      private readonly IConfiguration configuration;

      public Startup (IConfiguration configuration)
      {
         this.configuration = configuration;
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices (IServiceCollection services)
      {
         services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);;

         //services.AddSingleton<IConferenceService, ConferenceMemoryService> ();
         //services.AddSingleton<IProposalService, ProposalMemoryService> ();

         // Using the API Services
         //services.AddSingleton<IConferenceService, ConferenceApiService> ();
         services.AddHttpClient<IConferenceService, ConferenceApiService> ();
         services.AddSingleton<IProposalService, ProposalApiService> ();

         // Calling the Web API
         services.AddHttpClient ("GlobomanticsApi", c =>
            c.BaseAddress = new Uri ("http://localhost:5000"));

         services.Configure<GlobomanticsOptions> (configuration.GetSection ("Globomantics"));
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure (IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment ())
         {
            app.UseDeveloperExceptionPage ();
         }
         else
         {
            app.UseExceptionHandler ("/Error");
         }

         app.UseStaticFiles (); // Serves images and ccs files         

         app.UseMvc (routes =>
         {
            routes.MapRoute (
               name: "default",
               template: "{controller=Conference}/{action=Index}/{id?}");

            //routes.MapRoute ("default", "{controller=Conference}/{action=Index}/{id?}");
         });
      }
   }
}
