using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using BaseObjectsMVVM;
using projectControl;

namespace subdivision.Models.experts
{
    public class ExpertsListVM:DictionaryViewModel<ExpertsVM,ExpertsM,ExpertsSql>
    {
        public ExpertsListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        } 
        public override void LoadItems()
        {
            try
            {
                var PlaceSql = new ExpertsSql();
                SQLiteDataAdapter adapter = PlaceSql.LoadItems();
                DataTable data = new DataTable();
                adapter.Fill(data);
                Items.Clear();
                foreach (DataRow row in data.Rows)
                {
                    Items.Add(new ExpertsVM(row));
                }
            }catch(Exception e)
            {
                MessageBox.Show("err 1"+e.Message);
            }
        }
        



    }
}