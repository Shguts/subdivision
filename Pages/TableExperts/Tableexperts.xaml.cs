using System.Windows.Controls;
using BaseObjectsMVVM;

namespace subdivision.Pages
{
    public partial class Tableexperts : Page
    {
        public Tableexperts(Frame mainFrame,WorkspaceViewModel parent)
        {
            DataContext = new TableexpertsVMwm(mainFrame,parent);
            InitializeComponent();
        }
    }
}