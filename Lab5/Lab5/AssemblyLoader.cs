using System;
using System.Linq;
using System.Reflection;

namespace FifthTask
{
    public class AssemblyLoader
    {
        public Assembly Assembly { get; set; }

        public AssemblyLoader(string assemblyPath)
        {
            try
            {
                Assembly = Assembly.LoadFrom(assemblyPath);
                Console.WriteLine("Assembly loaded successfully.");
            }
            catch (Exception e)
            {
                throw new Exception("Load error: " + e.Message);
            }
        }

        public void ShowPublicTypes()
        {
            var types = Assembly
                .GetTypes()
                .Where(t => t.IsPublic)
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name);
            foreach (var type in types)
            {
                Console.WriteLine(type.FullName);
            }
        }
    }
}
