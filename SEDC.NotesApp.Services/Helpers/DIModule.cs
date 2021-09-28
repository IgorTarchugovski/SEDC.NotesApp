using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess.AdoNet;
using SEDC.NotesApp.DataAccess.Dapper;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Models.DbModels;
using SEDC.NotesApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Services.Helpers
{
    public static class DIModule
    {
        public static IServiceCollection RegisterModule(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesAppDbContext>(
                opt => opt.UseSqlServer(connectionString)
            );

            // register repositories
            services.AddTransient<IRepository<Note>, NoteRepository>();

            // Using entity framework
            /*services.AddTransient<IRepository<User>, UserRepository>();*/

            // Using AdoNet
            /*services.AddTransient<IRepository<User>>(
                x => new AdoNetUserRepository(connectionString)
            );*/

            // Using Dapper
            services.AddTransient<IRepository<User>>(
                x => new DapperUserRepository(connectionString)
            );

            return services;
        }
    }
}
