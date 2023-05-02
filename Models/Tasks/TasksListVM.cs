using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.subunit;

namespace subdivision.Models.Tasks
{
    public class TasksListVM:DictionaryViewModel<TasksVM,TasksM,TaskSql>
    {
        public TasksListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        }

        public override void LoadItems()
        {
            try
            {
                var PlaceSql = new TaskSql();
                SQLiteDataAdapter adapter = PlaceSql.LoadItems();
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new TasksVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }
    }
}