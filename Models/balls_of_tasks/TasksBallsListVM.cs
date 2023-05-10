using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;
using subdivision.Models.balls_of_subunits;
using subdivision.Models.criteries;
using subdivision.Models.experts;
using subdivision.Pages.BallsofSubunits;
using subdivision.Pages.BallsofTasks;

namespace subdivision.Models.balls_of_tasks
{
    public class TasksBallsListVM:DictionaryViewModel<TasksBallsVM,TasksBallsM,TasksBallsSql>
    {
        
        public TasksBallsListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        }
        public TasksBallsListVM(WorkspaceViewModel parent,ExpertsVM extraVm,CriteriesVM criteriesVm) : base(parent)
        {
            LoadItems(extraVm,criteriesVm);
        }
        public override void LoadItems()
        {
            try
            {
                var PlaceSql = new TasksBallsSql();
                SQLiteDataAdapter adapter = PlaceSql.LoadItems();
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new TasksBallsVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }
        public void LoadItems(ExpertsVM ExtraVM,CriteriesVM CriteriaVM)
        {
            try
            {
                var PlaceSql = new TasksBallsSql();
                SQLiteDataAdapter adapter =  PlaceSql.LoadItems(ExtraVM,CriteriaVM);;
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new TasksBallsVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }

        public void AddItem(TasksBallsVM item)
        {
            var PlaceSql = new TasksBallsSql();
            PlaceSql.Create(item.Item);
            UpdateCommand.Execute(null);
        }
        
        public override void OpenItem(TasksBallsVM item)
        {
            Parent.MainFrame.Navigate(new BallsofSubunits(Parent.MainFrame,Parent,item.ExpertID,item.CriterieID,item.TaskID));
        }
    }
}