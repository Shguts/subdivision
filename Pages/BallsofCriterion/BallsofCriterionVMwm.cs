using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Media;
using BaseObjectsMVVM;
using projectControl;
using subdivision.Models.balls_of_criterion;
using subdivision.Models.criteries;
using subdivision.Models.experts;
using subdivision.Pages.TableSubunits;

namespace subdivision.Pages.BallsofCriterion
{
    public class BallsofCriterionVMwm:WorkspaceViewModel
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
        
        private CriterionBallsListVM _ballsOfCriterionList;
        public CriterionBallsListVM BallsOfCriterionList
        {
            get => _ballsOfCriterionList ?? (_ballsOfCriterionList = new CriterionBallsListVM(this,ExtraVM));
            set
            {
                _ballsOfCriterionList = value;
                OnPropertyChanged(() =>  BallsOfCriterionList);
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
        
        public BallsofCriterionVMwm(Frame mainframe,WorkspaceViewModel parent,ExpertsVM expertsVm):base (mainframe,parent)
        {
            ExtraVM = expertsVm;
        }
        public int WrapPanelWidth
        {
            get => (CriteriesListVM.Items.Count+1)*80;
        }
            
        private ObservableCollection<InfoExelArray> _infoArray;

        public ObservableCollection<InfoExelArray> InfoArray
        {
            get => _infoArray ?? (_infoArray = PushInfoArray());
        }

        private ObservableCollection<InfoExelArray> PushInfoArray()
        {
            var res = new ObservableCollection<InfoExelArray>();
            res.Add(new InfoExelArray() { IsReadOnly = true, Descr = "", Background = Brushes.White});
            foreach (var i in CriteriesListVM.Items)
            {
                
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = i.CriteriesName});
            }
            
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = CriteriesListVM.Items[i].CriteriesName});
                for (int j = 0; j < CriteriesListVM.Items.Count; j++)
                {
                    res.Add(new InfoExelArray() { IsReadOnly = j<=i, Descr = j!=i?"0":"1",Background = j!=i?Brushes.Red:Brushes.LawnGreen});
                }
            }
            res.Add(new InfoExelArray() { IsReadOnly = true, Descr = "Сумма",Background = Brushes.Aqua});
            for (int j = 0; j < CriteriesListVM.Items.Count; j++)
            {
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = "",Background = Brushes.Aqua});
            }
            return res;
        }
        
        private RelayCommand _createItemsCommand;

        public RelayCommand CreateItemsCommand => _createItemsCommand ?? (_createItemsCommand =
                new RelayCommand(obj => CreateItemsSum())
            );

        public void CreateItemsSum()
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                if (ExtraVM.IdExpert != null)
                {
                    var a = Convert.ToDecimal(InfoArray[_getIndexForSum(i)].Descr, provider);
                    BallsOfCriterionList.AddItem(new CriterionBallsVM()
                    {
                        CriterieID = i+1, ExpertID = (int)ExtraVM.IdExpert,
                        mark = a,q = Convert.ToDouble(res[i]/res.Sum(),provider)
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
        
        public List<double> res = new List<double>();
        private void _calculate()
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                
                for (int j = i+1; j < CriteriesListVM.Items.Count; j++)
                {
                    InfoArray[GetIndexByIndexes(j, i)].Descr = (1/Convert.ToDouble(InfoArray[GetIndexByIndexes(i,j)].Descr,provider)).ToString(CultureInfo.InvariantCulture);
                    InfoArray[GetIndexByIndexes(j, i)].Background = Brushes.White;
                }
            }

            
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                double res1 = 0;
                for (int j = 0; j < CriteriesListVM.Items.Count; j++)
                {
                    var out1 = Convert.ToDouble(InfoArray[GetIndexByIndexes(j, i)].Descr,provider);
                    res1 += out1;
                }
                
                InfoArray[_getIndexForSum(i)].Descr = res1.ToString(CultureInfo.InvariantCulture);
                
            }
            
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                double res1 = 1;
                for (int j = 0; j < CriteriesListVM.Items.Count; j++)
                {
                    double.TryParse(InfoArray[GetIndexByIndexes(i, j)].Descr,NumberStyles.Any,CultureInfo.InvariantCulture, out double out1);
                    res1 *= out1;
                }
                var outres = (double)1 / CriteriesListVM.Items.Count;
                res.Add(Math.Pow(res1, outres));
                

            }




        }
        public override void SaveViewModel()
        {
            BallsOfCriterionList.SaveItems();
        }

        public override void UpdateViewModel()
        {
            BallsOfCriterionList.UpdateCommand.Execute(null);
        }
        private int _getIndexForSum(int i)
        {

            return InfoArray.Count - CriteriesListVM.Items.Count + i;
            

        }

        public int GetIndexByIndexes(int j, int i) => (CriteriesListVM.Items.Count + 1) * (j + 1) + i + 1;

    }

    public class InfoExelArray : INotifyPropertyChanged
    {
        private bool _isReadOnly;
        private string _descr;
        private Brush _backGround;

        public Brush Background
        {
            get => _backGround;
            set
            {
                _backGround = value;  
                OnPropertyChanged(() => Background);

            }
            
        }
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