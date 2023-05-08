using System.Windows.Controls;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;

namespace subdivision.Pages.Statistics
{
    public partial class Stats : Page
    {
        public Stats(Frame mainFrame,WorkspaceViewModel parent)
        {
            DataContext = new StatsVMwm(mainFrame,parent);
            InitializeComponent();
        }
    }
}