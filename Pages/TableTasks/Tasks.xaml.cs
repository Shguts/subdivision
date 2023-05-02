using System.Windows.Controls;
using BaseObjectsMVVM;

namespace subdivision.Pages.TableTasks
{
    public partial class Tasks : Page
    {
        public Tasks(Frame mainFrame,WorkspaceViewModel parent)
        {
            DataContext = new TabletasksVMwm(mainFrame, parent);
            InitializeComponent();
        }
    }
}