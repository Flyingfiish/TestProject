using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class DllClass
    {
        public string Name { get; set; }
        public ObservableCollection<string> PublicClasses { get; set; }
        public ObservableCollection<string> ProtectedClasses { get; set; }
    }
}
