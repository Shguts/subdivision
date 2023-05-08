using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Navigation;
using BaseObjectsMVVM;
using subdivision.Models.experts;
using subdivision.Models.Tasks;
using subdivision.Pages.BallsofTasks;

namespace subdivision.Models.balls_of_criterion
{
    public class CriterionBallsListVM:DictionaryViewModel<CriterionBallsVM,CriterionBallsM,CriterionBallsSql>
    {
        public CriterionBallsListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        }
        public CriterionBallsListVM(WorkspaceViewModel parent,ExpertsVM ExtraVM) : base(parent)
        {
            LoadItems(ExtraVM);
        }
        public void LoadItems(ExpertsVM ExtraVM)
        {
            try
            {
                var PlaceSql = new CriterionBallsSql();
                SQLiteDataAdapter adapter =  PlaceSql.LoadItems(ExtraVM);
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new CriterionBallsVM(row));
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
                var PlaceSql = new CriterionBallsSql();
                SQLiteDataAdapter adapter =  PlaceSql.LoadItems();;
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new CriterionBallsVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }

        public void AddItem(CriterionBallsVM item)
        {
            var PlaceSql = new CriterionBallsSql();
            PlaceSql.Create(item.Item);
            UpdateCommand.Execute(null);
        }
        
        public override void OpenItem(CriterionBallsVM item)
        {
            Parent.MainFrame.Navigate(new BallsofTasks(Parent.MainFrame,Parent,item.ExpertID,item.CriterieID));
        }
    }
}