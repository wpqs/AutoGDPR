﻿using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using MxReturnCode;

namespace Gdpr.UI.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MxUserMsg.Init(Assembly.GetExecutingAssembly(), MxMsgs.SupportedCultures);
            MxReturnCode<bool> rc = new MxReturnCode<bool>("Main()", false, "admin@imageqc.com");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
