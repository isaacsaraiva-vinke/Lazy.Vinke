// LazyAssemblyResolver.cs
//
// This file is integrated part of "Lazy Vinke" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 01

using System;
using System.IO;
using System.Reflection;

namespace Lazy.Vinke
{
    public static class LazyAssemblyResolver
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Resolve an assembly location
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The resolve event arguments</param>
        /// <returns>The located assembly</returns>
        public static Assembly Resolve(Object sender, ResolveEventArgs args)
        {
            String assemblyFileName = args.Name.Substring(0, args.Name.IndexOf(','));

            if (assemblyFileName.EndsWith(".dll") == true)
                assemblyFileName = assemblyFileName.Remove(assemblyFileName.LastIndexOf(".dll"), 4);

            String assemblyFolderPath = Path.Combine(Environment.CurrentDirectory, "Bin", assemblyFileName.ToLower());

            if (Directory.Exists(assemblyFolderPath) == false)
                assemblyFolderPath = Path.Combine(Environment.CurrentDirectory, "Bin");

            String[] fileCollection = Directory.GetFiles(assemblyFolderPath, assemblyFileName + ".dll", SearchOption.AllDirectories);

            if (args.Name.Contains("Version") == true && args.Name.Contains("Culture") == true && args.Name.Contains("PublicKeyToken") == true)
            {
                foreach (String file in fileCollection)
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(file);

                        if (assembly.GetName().FullName == args.Name)
                            return assembly;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            if (fileCollection.Length > 0)
            {
                try { return Assembly.LoadFrom(fileCollection[0]); }
                catch { return null; }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
