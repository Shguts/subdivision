using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.experts;

namespace subdivision.Models.criteries
{
    public class CriteriesSql:ModelSql<CriteriesM>
    {
        public override int? Create(CriteriesM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "insert into criteries(criterie_id,criterion_name) select "
                    +item.ItemId+",'"+item.CriteriesName+"'; select max(criterie_id) from criteries",
                    MainStaticObject.SqlManager.Connection);
                MainStaticObject.SqlManager.Connection.Close();
                DataTable data = new DataTable();
                res.Fill(data);
                
                if (data.Rows.Count > 0)
                {
                    return Int32.Parse(data.Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err 2" + e.Message);
            }

            return null;
        }

        public override void Update(CriteriesM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    " update criteries set " +
                    "criterion_name = '" + item.CriteriesName +
                    "' where criterie_id = " + item.ItemId +";",
                    MainStaticObject.SqlManager.Connection);
                res.ExecuteNonQuery();
                MainStaticObject.SqlManager.Connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("err 3" + e.Message);
            }
        }

        public override void Delete(CriteriesM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    "delete from criteries " +
                    " where criterie_id = "+item.ItemId +";",
                    MainStaticObject.SqlManager.Connection);
                res.ExecuteNonQuery();
                MainStaticObject.SqlManager.Connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Нельзя удалить критерий так как принимимает участие");
            }
        }
        public override SQLiteDataAdapter LoadItems()
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "SELECT criterie_id, criterion_name FROM criteries order by criterie_id",MainStaticObject.SqlManager.Connection);
                MainStaticObject.SqlManager.Connection.Close();
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show("err 4" + e.Message);
            }

            return null;
            //return null;
        }   
    }
}