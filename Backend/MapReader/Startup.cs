using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapReader.Interfaces;
using MapReader.Services;
using MapReader.Handlers;
using System.Text.Json.Serialization;
using MapReader.Executors; // PipelineExecutor namespace'i burada olmalý

namespace MapReader
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;

        public void ConfigureServices(IServiceCollection services)
        {
            // Handlers
            services.AddTransient<IFunctoidHandler, EqualHandler>();
            services.AddTransient<IFunctoidHandler, LogicalNotHandler>();
            services.AddTransient<IFunctoidHandler, SizeHandler>();
            services.AddTransient<IFunctoidHandler, StringLeftOrRight>();
            services.AddTransient<IFunctoidHandler, StringTrim>();
            services.AddTransient<IFunctoidHandler, ValueMappingHandler>();

            // Factory & Executor
            services.AddSingleton<FunctoidHandlerFactory>();
            services.AddTransient<MapExecutor>();

            // *** PipelineExecutor servisini ekledik ***
            services.AddTransient<PipelineExecutor>();

            //  Enum'larýn string olarak parse edilebilmesi için bu satýr eklendi
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // CORS
            services.AddCors(opts =>
                opts.AddDefaultPolicy(pb =>
                    pb.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                )
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
