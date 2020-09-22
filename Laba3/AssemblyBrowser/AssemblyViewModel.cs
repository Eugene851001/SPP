using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using AssemblyInfoGetter;
using System.Windows;
using Microsoft.Win32;

namespace AssemblyBrowser
{
    class AssemblyViewModel: INotifyPropertyChanged
    {
  
        private AssemblyGetter assemblyInfoGetter;
        private List<System.Reflection.MethodInfo> extensionMethods;

        private RealCommand openCommand;
        public RealCommand OpenCommand
        {
            get
            {
                if(openCommand == null)
                {
                    openCommand = new RealCommand(obj =>
                    {
                        OpenFileDialog openFile = new OpenFileDialog();
                        string fileName = "";
                        openFile.Filter = "Library files(*.dll)|*.dll";
                        if(openFile.ShowDialog() == true)
                        {
                            fileName = openFile.FileName;
                        }
                        try
                        {
                            assemblyInfoGetter.LoadAssembly(fileName);
                            var asmInfo = assemblyInfoGetter.GetAssemblyInfo();
                            namespacesInfo.Add(asmInfo);
                            extensionMethods.AddRange(assemblyInfoGetter.GetExtensionMethods());
                            foreach (var namespaceInfo in namespacesInfo)
                            {
                                foreach (var methodInfo in extensionMethods)
                                {
                                    assemblyInfoGetter.AddExtensionMethod(namespaceInfo, methodInfo);
                                }
                            }
                         //   onPropertyChanged("NamespacesInfo");
                        }
                        catch
                        {
                            MessageBox.Show($"Failed to load or parse assembly");
                        }
                    });
                }
                return openCommand;
            }
        }

        private ObservableCollection<NamespaceInfo> namespacesInfo = new ObservableCollection<NamespaceInfo>();
        public ObservableCollection<NamespaceInfo> NamespacesInfo 
        { 
            get
            {
                return namespacesInfo;
            }
            set
            {
                namespacesInfo = value;
                onPropertyChanged("NamespacesInfo");
            }
        }

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                onPropertyChanged("Age");
            }
        }

        public AssemblyViewModel()
        {
            age = 12;
            extensionMethods = new List<System.Reflection.MethodInfo>();
            assemblyInfoGetter = new AssemblyGetter();
            assemblyInfoGetter.LoadAssembly("DTO.dll");
            namespacesInfo.Add(assemblyInfoGetter.GetAssemblyInfo());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
