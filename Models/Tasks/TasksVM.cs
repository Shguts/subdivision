using System;
using System.Data;
using BaseObjectsMVVM;

namespace subdivision.Models.Tasks
{
    public class TasksVM:EntityViewModel<TasksM,TaskSql>
    {
        public TasksVM(DataRow row)
        {
            ParseArguments(row);
        }
        public TasksVM()
        {
        }

        public override void ParseArguments(DataRow row)
        {
            Item.ItemId = Int32.Parse(row.ItemArray[0].ToString());
            Item.task_name = row.ItemArray[1].ToString();
        }

        public int? IdTask
        {
            get => Item.ItemId;
            set
            {
                Item.ItemId = value;
                OnPropertyChanged(() => IdTask);
            }
            
        }
        public string TaskName
        {
            get => Item.task_name;
            set
            {
                SetPropertyValue(() => TaskName, value, ref Item.task_name);
            }
            
        }
    }
}