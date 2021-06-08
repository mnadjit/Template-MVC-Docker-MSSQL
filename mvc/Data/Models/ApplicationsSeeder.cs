using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using mvc.Models.Entities;

namespace mvc.Data.Models
{
    public class ApplicationsSeeder
    {
        private readonly ApplicationsContext _ctx;
        private readonly IWebHostEnvironment _webEnv;

        public ApplicationsSeeder(ApplicationsContext ctx, IWebHostEnvironment webEnv)
        {
            _ctx = ctx;
            _webEnv = webEnv;
        }

        public void Seed(){
            _ctx.Database.EnsureCreated();

            if (!_ctx.Applcations.Any()) {
                var filePath = Path.Combine(_webEnv.ContentRootPath, "Data/Samples/data.json");
                var json = File.ReadAllText(filePath);
                var nhApplications = JsonSerializer.Deserialize<IEnumerable<NhApplcation>>(json);

                _ctx.Applcations.AddRange(nhApplications);

                _ctx.SaveChanges();
            }
        }
    }
}