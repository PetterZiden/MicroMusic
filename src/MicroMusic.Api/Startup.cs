using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;

using MediatR;

using MicroMusic.Domain.Abstractions;
using MicroMusic.Spotify;
using MicroMusic.Spotify.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System;

namespace MicroMusic.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(GetJsonOptions());

            services.AddControllers();

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(GetSwaggerGenOptions());

            services.AddAutoMapper(typeof(Startup));

            services.AddSpotifyAuth(options =>
            {
                options.ClientId = Configuration.GetSection("SpotifyCredentials").GetSection("ClientId").Value;
                options.ClientSecret = Configuration.GetSection("SpotifyCredentials").GetSection("ClientSecret").Value;
            });

            return SetupContainer(services);
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroMusic V1");
            });

        }

        private static Action<JsonOptions> GetJsonOptions()
        {
            return options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.WriteIndented = true;
            };
        }

        private Action<SwaggerGenOptions> GetSwaggerGenOptions()
        {
            return options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroMusic", Version = "v1" });
            };
        }

        private IServiceProvider SetupContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(Configuration).As<IConfiguration>();

            if (Configuration.GetSection("SpotifyServiceSettings").GetValue<bool>("UseMockData"))
            {
                builder.RegisterType<Spotify.Mock.SpotifyService>().As<ISpotifyService>();
            }
            else
            {
                builder.RegisterType<SpotifyService>().As<ISpotifyService>();
            }

            builder.Populate(services);

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

    }
}
