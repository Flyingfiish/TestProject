using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class DllAssembly
    {
        public string Name { get; set; }
        public ObservableCollection<DllClass> DllClasses { get; set; }
    }
}
