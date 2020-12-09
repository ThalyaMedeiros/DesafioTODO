using Api.Services;
using Api.ViewModel;
using AutoMapper;
using Domain.Context.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;

namespace Api
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
            services.AddControllers();

            services.AddMvc();

            services.AddResponseCompression();

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddScoped<DataContext, DataContext>();

            services.AddScoped<IItensService, ItensService>();
            services.AddScoped<IItensRepository, ItensRepository>();
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IFuncoesRepository, FuncoesRepository>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<ICriptografia, Criptografia>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio TODO", Version = "v1" });

                SecuritySchemeType apiKey = SecuritySchemeType.ApiKey;
                ParameterLocation header = ParameterLocation.Header;
                x.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = header,
                        Description = "Copie 'Bearer ' + token'",
                        Name = "Authorization",
                        Type = apiKey
                    });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Itens, ItensViewModel>();
                cfg.CreateMap<Usuarios, UsuariosViewModel>();
                cfg.CreateMap<UsuariosViewModel, Usuarios>();
                cfg.CreateMap<ItensViewModel, Itens>().ForMember(p => p.Id, x => x.Ignore());
                cfg.CreateMap<Itens, RespostaItensViewModel>().ForMember(av => av.Status,
                      m => m.MapFrom(a => a.Status.ToString()));
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;


                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

                };


            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio TODO - V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
