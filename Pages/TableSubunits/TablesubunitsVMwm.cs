using System.Windows.Controls;
using BaseObjectsMVVM;
using projectControl;
using subdivision.Models.experts;
using subdivision.Models.subunit;
using subdivision.Pages.TableCriterion;

namespace subdivision.Pages.TableSubunits
{
    public class TablesubunitsVMwm:WorkspaceViewModel
    {
        public TablesubunitsVMwm(Frame mainFrame,WorkspaceViewModel parent):base(mainFrame,parent)
        {
            
        }
        private SubunitListVM _subunitListVM;
        public SubunitListVM SubunitListVM
        {
            get => _subunitListVM ?? (_subunitListVM = new SubunitListVM(this));
            set
            {
                _subunitListVM = value;
                OnPropertyChanged(() => SubunitListVM);
            }
        }
        public override void SaveViewModel()
        {
            SubunitListVM.SaveItems();
        }

        public override void UpdateViewModel()
        {
            SubunitListVM.UpdateCommand.Execute(null);
        }

    }
}