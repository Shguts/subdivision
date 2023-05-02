using System.Windows.Controls;
using BaseObjectsMVVM;
using projectControl;
using subdivision.Models.criteries;

namespace subdivision.Pages.TableCriterion
{
    public class TablecriterionVMwm:WorkspaceViewModel
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
        public override void SaveViewModel()
        {
            CriteriesListVM.SaveItems();
        }

        public override void UpdateViewModel()
        {
            CriteriesListVM.UpdateCommand.Execute(null);
        }
        public TablecriterionVMwm(Frame mainFrame, WorkspaceViewModel parent) : base(mainFrame, parent)
        {
            
        }
    }
}