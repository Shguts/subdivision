using System.Windows.Controls;
using BaseObjectsMVVM;

namespace subdivision.Pages.TableCriterion
{
    public partial class Criterion : Page
    {
        public Criterion(Frame mainFrame,WorkspaceViewModel parent)
        {
            DataContext = new TablecriterionVMwm(mainFrame,parent);
            InitializeComponent();
        }
    }
}