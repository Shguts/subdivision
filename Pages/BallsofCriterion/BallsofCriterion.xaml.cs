using System.Windows.Controls;
using BaseObjectsMVVM;
using subdivision.Models.experts;

namespace subdivision.Pages.BallsofCriterion
{
    public partial class BallsofCriterion : Page
    {
        public BallsofCriterion(Frame mainframe,WorkspaceViewModel parent,ExpertsVM expertsVm)
        {
            DataContext = new BallsofCriterionVMwm(mainframe,parent,expertsVm);
            InitializeComponent();
        }
    }
}