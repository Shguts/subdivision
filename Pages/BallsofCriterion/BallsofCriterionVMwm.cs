using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Controls;
using BaseObjectsMVVM;
using projectControl;
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
        public BallsofCriterionVMwm(Frame mainframe,WorkspaceViewModel parent,ExpertsVM expertsVm):base (mainframe,parent)
        {
            

        }
        public int WrapPanelWidth
        {
            get => (CriteriesListVM.Items.Count+1)*40;
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
            foreach (var i in CriteriesListVM.Items)
            {
                
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = i.CriteriesName});
            }
            
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = CriteriesListVM.Items[i].CriteriesName});
                for (int j = 0; j < CriteriesListVM.Items.Count; j++)
                {
                    res.Add(new InfoExelArray() { IsReadOnly = j<=i?true:false, Descr = j!=i?"2":"1"});
                }
            }
            res.Add(new InfoExelArray() { IsReadOnly = true, Descr = "Сумма"});
            for (int j = 0; j < CriteriesListVM.Items.Count; j++)
            {
                res.Add(new InfoExelArray() { IsReadOnly = true, Descr = ""});
            }
            return res;
        }
        private RelayCommand _calculateInfoCommand;

        public RelayCommand CalculateInfoCommand => _calculateInfoCommand ?? (_calculateInfoCommand =
                new RelayCommand(obj => _calculate())
            );

        private void _calculate()
        {
            
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                
                for (int j = i+1; j < CriteriesListVM.Items.Count; j++)
                {
                    InfoArray[GetIndexByIndexes(j, i)].Descr = (1/Convert.ToDouble(InfoArray[GetIndexByIndexes(i,j)].Descr)).ToString(CultureInfo.InvariantCulture);
                }
            }

            
            for (int i = 0; i < CriteriesListVM.Items.Count; i++)
            {
                double res1 = 0;
                for (int j = 0; j < CriteriesListVM.Items.Count; j++)
                {
                    double.TryParse(InfoArray[GetIndexByIndexes(j, i)].Descr,NumberStyles.Any,CultureInfo.InvariantCulture, out double out1);
                    res1 += out1;
                }
                InfoArray[_getIndexForSum(i)].Descr = res1.ToString();
            }

        }

        private int _getIndexForSum(int i)
        {

            return InfoArray.Count - CriteriesListVM.Items.Count + i;


        }

        public int GetIndexByIndexes(int j, int i) => (CriteriesListVM.Items.Count + 1) * (j + 1) + i + 1;

    }   

    public class InfoSumArray
    {
        
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