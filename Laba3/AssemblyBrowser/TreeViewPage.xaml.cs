using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssemblyBrowser
{
    /// <summary>
    /// Логика взаимодействия для TreeViewPage.xaml
    /// </summary>
    public partial class TreeViewPage : Page
    {
        public TreeViewPage()
        {
            InitializeComponent();
            var assemblyVM = new AssemblyViewModel();
            DataContext = assemblyVM;
            assemblyTree.ItemsSource = assemblyVM.AssembliesInfo;
        //    this.assemblyTree.ItemsSource = assemblyVM.NamespacesInfo;
            // DataContext = new AssemblyViewModel();
            /*    AssemblyViewModel assemblyVM = new AssemblyViewModel();
                this.assemblyTree.Items.Add(assemblyVM.Age);
                foreach(var namespaceInfo in assemblyVM.NamespaceInfos)
                {
                    var namespaceNode = new TreeViewItem() { Header = namespaceInfo.Name};
                    foreach(var dataType in namespaceInfo.DataTypes)
                    {
                        var dataTypeNode = new TreeViewItem() { Header = dataType.Name };
                        foreach(var field in dataType.Fields)
                        {
                            var fieldNode = new TreeViewItem() { Header = $"Field: {field.Name}" };
                            dataTypeNode.Items.Add(fieldNode);
                        }
                        foreach(var method in dataType.Methods)
                        {
                            var methodNode = new TreeViewItem() { Header = $"Method: {method.Name}" };
                            dataTypeNode.Items.Add(methodNode);
                        }
                        namespaceNode.Items.Add(dataTypeNode);
                    }
                    assemblyTree.Items.Add(namespaceNode);
                }*/
        }
    }
}
