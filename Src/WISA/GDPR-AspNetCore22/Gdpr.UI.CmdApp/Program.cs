﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Gdpr.Domain;
using Microsoft.Extensions.Configuration;
using MxReturnCode;

namespace Gdpr.UI.CmdApp
{
    class Program
    {
        public static readonly String WebAppVersion = typeof(Program)?.GetTypeInfo()?.Assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        public static readonly string WebAppName = typeof(Program)?.GetTypeInfo()?.Assembly?.GetName().Name ?? "[not set]";

        static async Task<int> Main(string[] args)
        {
            MxReturnCode<int> rc = new MxReturnCode<int>($"{Program.WebAppName} v{Program.WebAppVersion}", 1);

            rc.Init(Assembly.GetExecutingAssembly(), "admin@imageqc.com", null, null, null, MxMsgs.SupportedCultures);

            Console.WriteLine(rc.GetInvokeDetails());

            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json")
                .Build();
            var conn = config?["ConnectionStrings:DefaultConnection"];  //03-12-18
            if (conn == null)
                rc.SetError(2010101, MxError.Source.AppSetting, "config not built or ConnectionStrings:DefaultConnection not found");
            else
            {
                using (IAdminRepo repo = new AdminRepo(conn))
                {
                    rc += await repo.GetUrdCountAsync();
                }
                if (rc.IsSuccess(true))
                {
                    Console.WriteLine($"Roles found = {rc.GetResult()}");
                    rc.SetResult(0);
                }
            }
            Console.WriteLine(rc.IsError(true) ? rc.GetErrorUserMsg() : $"Hello World!");
            Console.WriteLine(rc.IsError(true) ? rc.GetErrorTechMsg(): "no error");

            return rc.GetResult();
        }
    }
}
