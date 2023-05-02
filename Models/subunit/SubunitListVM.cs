using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.criteries;
using subdivision.Models.experts;

namespace subdivision.Models.subunit
{
    public class SubunitListVM:DictionaryViewModel<SubunitVM,SubunitM,SubunitSql>
    {
        public SubunitListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        }

        public override void LoadItems()
        {
            try
            {
                var PlaceSql = new SubunitSql();
                SQLiteDataAdapter adapter = PlaceSql.LoadItems();
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new SubunitVM(row));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 1" + e.Message);
            }
        }
    }
}