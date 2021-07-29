using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class AssemblyFinder
    {
        private static string[] GetAllDllFiles(string directory)
        {
            try
            {
                string[] allFoundFiles = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);
                return allFoundFiles;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static ObservableCollection<DllAssembly> GetDllMethods(string directory)
        {
            var dllPaths = GetAllDllFiles(directory);

            if(dllPaths == null)
            {
                return null;
            }

            ObservableCollection<DllAssembly> dllAssemblies = new ObservableCollection<DllAssembly>();

            foreach (var dllPath in dllPaths)
            {
                Assembly asm = Assembly.LoadFrom(dllPath);

                var dllAssembly = new DllAssembly() { Name = asm.FullName, DllClasses = new ObservableCollection<DllClass>() };

                IEnumerable<Type> types = asm.GetTypes().Where(t => t.IsClass);
                foreach (Type t in types)
                {
                    var methodsInfos = t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

                    var dllClass = new DllClass() 
                    { 
                        Name = t.Name, 
                        ProtectedClasses = new ObservableCollection<string>(),
                        PublicClasses = new ObservableCollection<string>()
                    };

                    foreach (var method in methodsInfos.Where(m => m.IsPublic))
                        dllClass.PublicClasses.Add(method.Name);

                    foreach (var method in methodsInfos.Where(m => m.IsFamily))
                        dllClass.ProtectedClasses.Add(method.Name);

                    dllAssembly.DllClasses.Add(dllClass);
                }
                dllAssemblies.Add(dllAssembly);
            }
            return dllAssemblies;
        }
    }
}
