﻿using System;
using System.IO;
using System.Linq;

namespace IISExpressBootstrapper
{
    internal class WebApplication
    {
        public readonly string Name;
        public readonly int PortNumber;
        public readonly string FullPath;

        public WebApplication(string name, int portNumber)
        {
            Name = name;
            PortNumber = portNumber;
            FullPath = GetFullPath();
        }

        private string GetFullPath()
        {
            var solutionFolder = GetSolutionFolderPath();
            var projectPath = FindSubFolderPath(solutionFolder, Name);

            return projectPath;
        }
        
        private static string GetSolutionFolderPath()
        {
            var directory = new DirectoryInfo(Environment.CurrentDirectory);

            while (directory != null && directory.GetFiles("*.sln").Length == 0)
            {
                directory = directory.Parent;
            }

            if (directory == null)
                throw new DirectoryNotFoundException(); 
            
            return directory.FullName;

        }

        private static string FindSubFolderPath(string rootFolderPath, string folderName)
        {
            var directory = new DirectoryInfo(rootFolderPath);

            directory = (directory.GetDirectories("*", SearchOption.AllDirectories)
                .Where(folder => String.Equals(folder.Name, folderName, StringComparison.CurrentCultureIgnoreCase)))
                .FirstOrDefault();

            if (directory == null)
            {
                throw new DirectoryNotFoundException();
            }

            return directory.FullName;
        }
    }
}
