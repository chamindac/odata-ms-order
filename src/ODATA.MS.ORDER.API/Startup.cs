using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using ODATA.MS.ORDER.API.Data;
using ODATA.MS.ORDER.API.Models;

namespace ODATA.MS.ORDER.API
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
            services //.AddEntityFrameworkNpgsql()
                    .AddDbContext<OderDbContext>(opt =>
                        opt.UseNpgsql(Configuration.GetConnectionString("OrderDBConnection"))
                        );

            services.AddControllers()
                // add OData capability to controllers
                .AddOData(options => options.Select().Filter().Count().OrderBy().Expand()
                            .SetMaxTop(100) // enable usage of $top
                            .AddRouteComponents("odata", GetEdmModel()) // enable OData routing
                            )
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ODATA.MS.ORDER.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODATA.MS.ORDER.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // Support OData EDM
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Order>("Orders"); // must match BooksController
            
            //modelBuilder.EntityType<Book>()
            //            .Action("AddRating")
            //            .Parameter<int>("Rating");
            //modelBuilder.EntityType<Book>().Collection
            //    .Function("BestSelling")
            //    .Returns<Book>();
            //modelBuilder
            //    .Function("GetTotalBookSalesValue")
            //    .Returns<double>()
            //    .Parameter<int>("BookCategory");
            return modelBuilder.GetEdmModel();
        }
    }
}
