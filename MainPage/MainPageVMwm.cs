using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using BaseObjectsMVVM;
using BaseObjectsMVVM.WpfControls;
using Google.Protobuf.WellKnownTypes;
using projectControl;
using subdivision.Models.experts;
using subdivision.Pages;
using subdivision.Pages.BallsofCriterion;

namespace subdivision.MainPage
{
    public class MainPageVMwm : WorkspaceViewModel
    {
        private Frame MainFrame;
        public MainPageVMwm(Frame mainFrame) : base(mainFrame, null)
        {
            MainStaticObject.SqlManager = new SqlManager();
            MainStaticObject.SqlManager.ConnectionString =
                "Data Source=divisions.sqlite; Version=3; FailIfMissing=False";
            MainStaticObject.SqlManager.Connect();
            MainFrame = mainFrame;
        }
        
        private ExpertsListVM _experstlist;
        public ExpertsListVM ExpertListVM
        {
            get => _experstlist ?? (_experstlist = new ExpertsListVM(this));
            set
            {
                _experstlist = value;
                OnPropertyChanged(() => ExpertListVM);
            }
        }
        public string psswd {private get; set; }
        
        private RelayCommand nextpage;
        public RelayCommand NextPage => nextpage ?? (nextpage = new RelayCommand(obj => newPage()));
        public virtual void newPage()
        {
            if (psswd == "admin123456") Navigate(new Pages.Tableexperts(MainFrame, this));
            else
            {
                if (ExpertListVM.Items.FirstOrDefault(x => x.Password == psswd)!=null)
                {
                    Navigate(new BallsofCriterion(MainFrame, this,ExpertListVM.Items.FirstOrDefault(x => x.Password == psswd)));
                }
            };

        }
        public override void UpdateViewModel()
        {
        }
       // public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) => psswd = ((PasswordBox)sender).SecurePassword;

        
    }
}