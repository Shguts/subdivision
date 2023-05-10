using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;
using subdivision.Models.criteries;
using subdivision.Models.experts;
using subdivision.Models.Tasks;
using subdivision.Pages.BallsofTasks;

namespace subdivision.Models.balls_of_subunits
{
    public class SubunitsBallsListVM:DictionaryViewModel<SubunitsBallsVM,SubunitsBallsM,SubunitsBallsSql>
    {
        public SubunitsBallsListVM(WorkspaceViewModel parent): base(parent)
        {
            LoadItems();
        }
        public SubunitsBallsListVM(WorkspaceViewModel parent,ExpertsVM ExtraVM,CriteriesVM CriteriaVM,TasksVM tasksVm) : base(parent)
        {
            LoadItems(ExtraVM,CriteriaVM,tasksVm);
        }
        public void LoadItems(ExpertsVM ExtraVM,CriteriesVM CriteriaVM,TasksVM tasksVm)
        {
            try
            {
                var PlaceSql = new SubunitsBallsSql();
                SQLiteDataAdapter adapter =  PlaceSql.LoadItems(ExtraVM,CriteriaVM,tasksVm);;
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new SubunitsBallsVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }

        public override void LoadItems()
        {
            try
            {
                var PlaceSql = new SubunitsBallsSql();
                SQLiteDataAdapter adapter =  PlaceSql.LoadItems();;
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new SubunitsBallsVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }

        public void AddItem(SubunitsBallsVM item)
        {
            var PlaceSql = new SubunitsBallsSql();
            PlaceSql.Create(item.Item);
            UpdateCommand.Execute(null);
        }
        
        public override void OpenItem(SubunitsBallsVM item)
        {
            
        }
    }
}