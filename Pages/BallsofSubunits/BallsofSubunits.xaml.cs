using System.Windows.Controls;
using BaseObjectsMVVM;
using subdivision.Pages.BallsofTasks;

namespace subdivision.Pages.BallsofSubunits
{
    public partial class BallsofSubunits : Page
    {
        public BallsofSubunits(Frame mainFrame,WorkspaceViewModel parent,int expertsVm,int criteriesVm,int tasksVm)
        {
            DataContext = new BallsofSubunitsVMwm(mainFrame, parent, expertsVm, criteriesVm,tasksVm);
            InitializeComponent();
        }
    }
}