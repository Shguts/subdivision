using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;

namespace subdivision.Models.criteries
{
    public class CriteriesListVM:DictionaryViewModel<CriteriesVM,CriteriesM,CriteriesSql>
    {
        public CriteriesListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        } 
        public override void LoadItems()
        {
            try
            {
                var PlaceSql = new CriteriesSql();
                SQLiteDataAdapter adapter = PlaceSql.LoadItems();
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new CriteriesVM(row));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("err 1"+e.Message);
            }
        }
    }
}