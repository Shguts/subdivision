using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
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
using subdivision.Pages.TableCriterion;
using subdivision.Pages.TableSubunits;
using subdivision.Pages.TableTasks;

namespace subdivision.Pages
{
    public class TableexpertsVMwm:WorkspaceViewModel
    {
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
        public TableexpertsVMwm(Frame mainFrame,WorkspaceViewModel parent):base(mainFrame,parent)
        {
            
        }
        public override void SaveViewModel()
        {
            ExpertListVM.SaveItems();
        }

        public override void UpdateViewModel()
        {
            ExpertListVM.UpdateCommand.Execute(null);
        }
        private RelayCommand _opencriterionListCommnand;

        public RelayCommand OpencriterionListCommnand => _opencriterionListCommnand ?? (_opencriterionListCommnand =
                new RelayCommand(obj => Navigate(new Criterion(MainFrame, this)))
            );
        private RelayCommand _opensubunitListCommnand;

        public RelayCommand OpensubunitListCommnand => _opensubunitListCommnand ?? (_opensubunitListCommnand =
                new RelayCommand(obj => Navigate(new Subunit(MainFrame, this)))
            );
        private RelayCommand _opentaskListCommnand;

        public RelayCommand OpentaskListCommnand => _opentaskListCommnand ?? (_opentaskListCommnand =
                new RelayCommand(obj => Navigate(new Tasks(MainFrame, this)))
            );

        /*private RelayCommand _test;

        public RelayCommand test => _test ?? (_test =
                new RelayCommand(obj => Navigate(new BallsofCriterion.BallsofCriterion(MainFrame,this)))
            );*/
    }

}