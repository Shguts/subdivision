using System.Windows.Controls;

namespace subdivision.MainPage
{
    public partial class MainPage : Page
    {
        public MainPage(Frame mainFrame)
        {
            DataContext = new MainPageVMwm(mainFrame);
            InitializeComponent();
        }
    }
}