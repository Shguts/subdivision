using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;
using subdivision.Models.balls_of_subunits;
using subdivision.Models.balls_of_tasks;
using subdivision.Models.criteries;
using subdivision.Models.experts;
using subdivision.Models.subunit;
using subdivision.Models.Tasks;

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
        private TasksBallsListVM _ballsOfTaskList;
        public TasksBallsListVM BallsOfTaskList
        {
            get => _ballsOfTaskList ?? (_ballsOfTaskList = new TasksBallsListVM(this));
            set
            {
                _ballsOfTaskList = value;
                OnPropertyChanged(() =>  BallsOfTaskList);
            }
        }
        private SubunitsBallsListVM _ballsOfSubunitsList;
        public SubunitsBallsListVM  BallsOfSubunitsList
        {
            get => _ballsOfSubunitsList ?? (_ballsOfSubunitsList = new SubunitsBallsListVM(this));
            set
            {
                _ballsOfSubunitsList = value;
                OnPropertyChanged(() => BallsOfSubunitsList);
            }
        }
        
        private ExpertsListVM _expertslist;

        public ExpertsListVM ExperetsListVM
        {
            get => _expertslist ?? (_expertslist = new ExpertsListVM(this));
            set
            {
                _expertslist = value;
                OnPropertyChanged(() => ExperetsListVM);
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
        private TasksListVM _taskslist;

        public TasksListVM TasksListVM
        {
            get => _taskslist ?? (_taskslist = new TasksListVM(this));
            set
            {
                _taskslist = value;
                OnPropertyChanged(() => TasksListVM);
            }
        }
        private SubunitListVM _subunitslist;

        public SubunitListVM SubunitsListVM
        {
            get => _subunitslist ?? (_subunitslist = new SubunitListVM(this));
            set
            {
                _subunitslist = value;
                OnPropertyChanged(() => SubunitsListVM);
            }
        }
        public int WrapPanelWidth
        {
            get => (CriteriesListVM.Items.Count+1)*20;
        }
        private ObservableCollection<InfoExelArray> _infoArrayCriterie;

        public ObservableCollection<InfoExelArray> InfoArrayCriterie
        {
            get => _infoArrayCriterie ?? (_infoArrayCriterie = new ObservableCollection<InfoExelArray>());
        }
        private ObservableCollection<InfoExelArray> _infoArrayTasks;

        public ObservableCollection<InfoExelArray> InfoArrayTasks
        {
            get => _infoArrayTasks ?? (_infoArrayTasks = new ObservableCollection<InfoExelArray>());
        }
        private ObservableCollection<InfoExelArray> _infoArraySubunits;

        public ObservableCollection<InfoExelArray> InfoArraySubunits
        {
            get => _infoArraySubunits ?? (_infoArraySubunits = new ObservableCollection<InfoExelArray>());
        }

        public StatsVMwm(Frame mainframe, WorkspaceViewModel parent) : base(mainframe, parent)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";
            for (int i = 1; i <= CriteriesListVM.Items.Count; i++)
            {
                var addq = Convert.ToDouble(BallsOfCriterionList.Items.Where(x=>x.CriterieID==i).Select(y=>y.q).FirstOrDefault(),provider);
                InfoArrayCriterie.Add(new InfoExelArray(){ Descr = addq.ToString(CultureInfo.InvariantCulture)});
            }
            for (int i = 1; i <= TasksListVM.Items.Count; i++)
            {
                var addq = Convert.ToDouble(BallsOfTaskList.Items.Where(x=>x.TaskID==i).Select(y=>y.q).FirstOrDefault(),provider);
                InfoArrayTasks.Add(new InfoExelArray(){ Descr = addq.ToString(CultureInfo.InvariantCulture)});
            }
            for (int i = 1; i <= SubunitsListVM.Items.Count; i++)
            {
                var addq = Convert.ToDouble(BallsOfSubunitsList.Items.Where(x=>x.SubunitID==i).Select(y=>y.q).FirstOrDefault(),provider);
                InfoArraySubunits.Add(new InfoExelArray(){ Descr = addq.ToString(CultureInfo.InvariantCulture)});
            }
        }
    }
    public class InfoExelArray : INotifyPropertyChanged
    {
        private string _descr;

        public string Descr
        {
            get => _descr;
            set
            {
                _descr = value;
                OnPropertyChanged(() => Descr);
            }
        }
        /// <summary>
        /// Механизм "пинания" View при изенении ViewModel
        /// </summary>
        public virtual void OnPropertyChanged<TPropertyType>(Expression<Func<TPropertyType>> propertyExpr)
        {
            string propertyName = GetPropertySymbol(propertyExpr);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static string GetPropertySymbol<TResult>(Expression<Func<TResult>> expr)
        {
            if (expr.Body is UnaryExpression)
                return ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            return ((MemberExpression)expr.Body).Member.Name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}