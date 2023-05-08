using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;
using subdivision.Models.criteries;

namespace subdivision.Pages.Statistics
{
    public class StatsVMwm : WorkspaceViewModel
    {
        private CriterionBallsListVM _ballsOfCriterionList;
        public CriterionBallsListVM BallsOfCriterionList
        {
            get => _ballsOfCriterionList ?? (_ballsOfCriterionList = new CriterionBallsListVM(this));
            set
            {
                _ballsOfCriterionList = value;
                OnPropertyChanged(() =>  BallsOfCriterionList);
            }
        }
        private CriteriesListVM _criterieslist;

        public CriteriesListVM CriteriesListVM
        {
            get => _criterieslist ?? (_criterieslist = new CriteriesListVM(this));
            set
            {
                _criterieslist = value;
                OnPropertyChanged(() => CriteriesListVM);
            }
        }

        public List<double> GetQGlobbal
        {
            get;
            set;

        }

        public int WrapPanelWidth
        {
            get => (CriteriesListVM.Items.Count+1)*40;
        }
        public StatsVMwm(Frame mainframe, WorkspaceViewModel parent) : base(mainframe, parent)
        {
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                GetQGlobbal.Add(BallsOfCriterionList.Items[i].q);
                //)((
            }
        }
    }
}