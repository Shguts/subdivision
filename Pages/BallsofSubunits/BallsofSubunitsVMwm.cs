using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using BaseObjectsMVVM;
using projectControl;
using subdivision.Models.balls_of_criterion;
using subdivision.Models.balls_of_subunits;
using subdivision.Models.criteries;
using subdivision.Models.experts;
using subdivision.Models.subunit;
using subdivision.Models.Tasks;

namespace subdivision.Pages.BallsofSubunits
{
    public class BallsofSubunitsVMwm:WorkspaceViewModel
    {
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
        private ExpertsListVM _expertslist;
        public ExpertsListVM ExpertsListVM
        {
            get => _expertslist ?? (_expertslist = new ExpertsListVM(this));
            set
            {
                _expertslist = value;
                OnPropertyChanged(() => ExpertsListVM);
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
        public BallsofSubunitsVMwm(Frame mainFrame,WorkspaceViewModel parent,int expertsVm,int criteriesVm,int tasksVm):base(mainFrame,parent)
        {
            ExtraVM = ExpertsListVM.Items.FirstOrDefault(x => x.IdExpert==expertsVm);
            CriteriaVM = CriteriesListVM.Items.FirstOrDefault(x => x.IdCriterie==criteriesVm);
            TaskeVM = TasksListVM.Items.FirstOrDefault(x => x.IdTask == tasksVm);
        }
        /// <summary>
        /// ///////////////////
        /// </summary>
        private SubunitListVM _subuintslist;
        public SubunitListVM SubunitsListVM
        {
            get => _subuintslist ?? (_subuintslist = new SubunitListVM (this));
            set
            {
                _subuintslist = value;
                OnPropertyChanged(() => SubunitsListVM);
            }
        }
        
        private SubunitsBallsListVM _ballsOfSubunitsList;
        public SubunitsBallsListVM BallsOfSubunitList
        {
            get => _ballsOfSubunitsList ?? (_ballsOfSubunitsList = new SubunitsBallsListVM(this,ExtraVM,CriteriaVM,TaskeVM));
            set
            {
                _ballsOfSubunitsList = value;
                OnPropertyChanged(() =>  BallsOfSubunitList);
            }
        }
        private TasksVM _taskeVM;

        public TasksVM TaskeVM
        {
            get { return _taskeVM; }
            set
            {
                _taskeVM = value;
                OnPropertyChanged(() => TaskeVM);
            }
        }
        private CriteriesVM _criteriaVM;

        public CriteriesVM CriteriaVM
        {
            get { return _criteriaVM; }
            set
            {
                _criteriaVM = value;
                OnPropertyChanged(() => CriteriaVM);
            }
        }
        private ExpertsVM _extraVM;

        public ExpertsVM ExtraVM
        {
            get { return _extraVM; }
            set
            {
                _extraVM = value;
                OnPropertyChanged(() => ExtraVM);
            }
        }
        
        public int WrapPanelWidth
        {
            get => (SubunitsListVM.Items.Count+1)*40;
        }
            
        private ObservableCollection<InfoExelArray> _infoArray;

        public ObservableCollection<InfoExelArray> InfoArray
        {
            get => _infoArray ?? (_infoArray = PushInfoArray());
        }

        private ObservableCollection<InfoExelArray> PushInfoArray()
        {
            var res = new ObservableCollection<InfoExelArray>();
            res.Add(new InfoExelArray() { IsReadOnly = true, Descr = ""});
            foreach (var i in SubunitsListVM.Items)
            {
                
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = i.SubunitName});
            }
            
            for (int i = 0; i < SubunitsListVM.Items.Count; i++)
            {
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = SubunitsListVM.Items[i].SubunitName});
                for (int j = 0; j < SubunitsListVM.Items.Count; j++)
                {
                    res.Add(new InfoExelArray() { IsReadOnly = j<=i, Descr = j!=i?"2":"1"});
                }
            }
            res.Add(new InfoExelArray() { IsReadOnly = true, Descr = "Сумма"});
            for (int j = 0; j < SubunitsListVM.Items.Count; j++)
            {
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = ""});
            }
            return res;
        }
        
        private RelayCommand _createItemsCommand;

        public RelayCommand CreateItemsCommand => _createItemsCommand ?? (_createItemsCommand =
                new RelayCommand(obj => CreateItemsSum())
            );

        public void CreateItemsSum()
        {
            for (int i = 0; i < SubunitsListVM.Items.Count; i++)
            {
                if (ExtraVM.IdExpert != null)
                {
                    double.TryParse(InfoArray[_getIndexForSum(i)].Descr, out double a);
                    if (CriteriaVM.IdCriterie != null)
                        if (TaskeVM.IdTask != null)
                            BallsOfSubunitList.AddItem(new SubunitsBallsVM()
                            {
                                CriterieID = (int)CriteriaVM.IdCriterie, ExpertID = (int)ExtraVM.IdExpert,
                                TaskID = (int)TaskeVM.IdTask,SubunitID = i+1,
                                mark = a
                            });
                }
                
                else throw new Exception("Ошибка");
                //_getIndexForSum(i);
            }
        }
        
        private RelayCommand _calculateInfoCommand;

        public RelayCommand CalculateInfoCommand => _calculateInfoCommand ?? (_calculateInfoCommand =
                new RelayCommand(obj => _calculate())
            );
        

        private void _calculate()
        {
            
            for (int i = 0; i < SubunitsListVM.Items.Count; i++)
            {
                
                for (int j = i+1; j < SubunitsListVM.Items.Count; j++)
                {
                    InfoArray[GetIndexByIndexes(j, i)].Descr = (1/Convert.ToDouble(InfoArray[GetIndexByIndexes(i,j)].Descr)).ToString(CultureInfo.InvariantCulture);
                }
            }

            
            for (int i = 0; i < SubunitsListVM.Items.Count; i++)
            {
                double res1 = 0;
                for (int j = 0; j < SubunitsListVM.Items.Count; j++)
                {
                    double.TryParse(InfoArray[GetIndexByIndexes(j, i)].Descr,NumberStyles.Any,CultureInfo.InvariantCulture, out double out1);
                    res1 += out1;
                }
                
                InfoArray[_getIndexForSum(i)].Descr = res1.ToString();
                
            }

            List<double> res = new List<double>();
            for (int i = 0; i < SubunitsListVM.Items.Count; i++)
            {
                double res1 = 0;
                for (int j = 0; j < SubunitsListVM.Items.Count; j++)
                {
                    double.TryParse(InfoArray[GetIndexByIndexes(i, j)].Descr,NumberStyles.Any,CultureInfo.InvariantCulture, out double out1);
                    res1 *= out1;
                }
                res.Add(Math.Pow(res1, 1 / SubunitsListVM.Items.Count));

            }
            
        }

        private int _getIndexForSum(int i)
        {

            return InfoArray.Count - SubunitsListVM.Items.Count + i;
            

        }

        public int GetIndexByIndexes(int j, int i) => (SubunitsListVM.Items.Count + 1) * (j + 1) + i + 1;

    }

    public class InfoExelArray : INotifyPropertyChanged
    {
        private bool _isReadOnly;
        private string _descr;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;  
                OnPropertyChanged(() => IsReadOnly);

            }
        }

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