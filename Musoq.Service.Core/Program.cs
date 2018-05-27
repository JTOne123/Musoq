﻿using System;
using System.Reflection;

namespace Musoq.Service.Core
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
#if DEBUG
            //var server = new ContextService();
            //server.Start(args);
#else
            var servicesToRun = new ServiceBase[]
            {
                new ContextService()
            };
            ServiceBase.Run(servicesToRun);
#endif
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (assembly.FullName == args.Name)
                    return assembly;

            return null;
        }
    }
}