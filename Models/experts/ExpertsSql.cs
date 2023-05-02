using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;

namespace subdivision.Models.experts
{
    public class ExpertsSql:ModelSql<ExpertsM>
    {
        public override int? Create(ExpertsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "insert into experts(id_expert, first_name, second_name, last_name,sum_of_ball,password) select "
                    +item.ItemId+",'"+item.FirstName+"','"+item.SecondName+"','"+item.LastName+"','"+item.SumOfBall+"','"+item.password+"'; select max(id_expert) from experts",
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

        public override void Update(ExpertsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    " update experts set " +
                    "first_name = '" + item.FirstName +
                    "',last_name = '" + item.LastName +
                    "',second_name = '" + item.SecondName +
                    "',sum_of_ball = '" + item.SumOfBall + 
                    "',password = '" + item.password +
                    "' where id_expert = " + item.ItemId +";",
                    MainStaticObject.SqlManager.Connection);
                res.ExecuteNonQuery();
                MainStaticObject.SqlManager.Connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("err 3" + e.Message);
            }
        }

        public override void Delete(ExpertsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    "delete from experts " +
                    " where id_expert = "+item.ItemId +";",
                    MainStaticObject.SqlManager.Connection);
                res.ExecuteNonQuery();
                MainStaticObject.SqlManager.Connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Нельзя удалить эксперта так как принмимает участие");
            }
        }
        public override SQLiteDataAdapter LoadItems()
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "SELECT id_expert, first_name, last_name, second_name, sum_of_ball,password FROM experts order by id_expert",MainStaticObject.SqlManager.Connection);
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