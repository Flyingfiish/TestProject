using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestProject.Common;
using TestProject.Models;

namespace TestProject.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {

            DllAssemblies = new ObservableCollection<DllAssembly>();
        }

        public ObservableCollection<DllAssembly> DllAssemblies { get; set; }

        private string directory;

        public string Directory
        {
            get => directory;
            set
            {
                directory = value;
                OnPropertyChanged("Directory");
            }
        }

        private RelayCommand getDllFilesCommand;
        public RelayCommand GetDllFilesCommand
        {
            get
            {
                return getDllFilesCommand ??
                    (getDllFilesCommand = new RelayCommand(obj =>
                    {
                        if (Directory != null)
                        {
                            DllAssemblies.Clear();
                            var result = AssemblyFinder.GetDllMethods(Directory);
                            if (result != null)
                                foreach (var assembly in result)
                                    DllAssemblies.Add(assembly);
                        }
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
