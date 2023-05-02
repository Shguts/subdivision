using System.Windows.Controls;
using BaseObjectsMVVM;
using subdivision.Models.subunit;
using subdivision.Models.Tasks;

namespace subdivision.Pages.TableTasks
{
    public class TabletasksVMwm:WorkspaceViewModel
    {
        public TabletasksVMwm(Frame mainFrame,WorkspaceViewModel parent):base(mainFrame,parent)
        {
            
        }
        private TasksListVM _tasksListVM;
        public TasksListVM TasksListVM
        {
            get => _tasksListVM?? (_tasksListVM = new TasksListVM(this));
            set
            {
                _tasksListVM = value;
                OnPropertyChanged(() => TasksListVM);
            }
        }
        public override void SaveViewModel()
        {
            TasksListVM.SaveItems();
        }

        public override void UpdateViewModel()
        {
            TasksListVM.UpdateCommand.Execute(null);
        }
    }
}