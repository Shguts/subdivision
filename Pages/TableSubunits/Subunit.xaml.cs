using System.Windows.Controls;
using BaseObjectsMVVM;

namespace subdivision.Pages.TableSubunits
{
    public partial class Subunit : Page
    {
        public Subunit(Frame mainFrame,WorkspaceViewModel parent)
        {
            DataContext = new TablesubunitsVMwm(mainFrame, parent);
            InitializeComponent();
        }
    }
}