using System.Windows.Controls;
using BaseObjectsMVVM;
using subdivision.Models.criteries;
using subdivision.Models.experts;

namespace subdivision.Pages.BallsofTasks
{
    public partial class BallsofTasks : Page
    {
        public BallsofTasks(Frame mainFrame,WorkspaceViewModel parent,int expertsVm,int criteriesVm)
        {
            DataContext = new BallsofTasksVMwm(mainFrame, parent, expertsVm, criteriesVm);
            InitializeComponent();
        }
    }
}