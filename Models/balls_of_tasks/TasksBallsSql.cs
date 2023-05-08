using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;

namespace subdivision.Models.balls_of_tasks
{
    public class TasksBallsSql:ModelSql<TasksBallsM>
    {
        public override int? Create(TasksBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "insert into balls_of_tasks(ExpertID, CriterieID, TaskID ,mark) select "
                    +item.ExpertID+",'"+item.CriterieID+"','"+item.TaskID+"','"+item.mark+"'; select max(ExpertID) from balls_of_tasks",
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

        public override void Update(TasksBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    " update balls_of_tasks set " +
                    "ExpertID = '" + item.ExpertID +
                    "CriterieID = '" + item.CriterieID +
                    "TaskID = '" + item.TaskID +
                    "mark = '" + item.mark +
                    "' where ExpertID = " + item.ExpertID +";",
                    MainStaticObject.SqlManager.Connection);
                res.ExecuteNonQuery();
                MainStaticObject.SqlManager.Connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("err 3" + e.Message);
            }
        }

        public override void Delete(TasksBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    "delete from balls_of_tasks " +
                    " where ExpertID = "+item.ExpertID +";",
                    MainStaticObject.SqlManager.Connection);
                res.ExecuteNonQuery();
                MainStaticObject.SqlManager.Connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Нельзя удалить подразделение так как принмимает участие");
            }
        }
        public override SQLiteDataAdapter LoadItems()
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "SELECT ExpertID, CriterieID,TaskID,mark FROM balls_of_tasks order by ExpertID ",MainStaticObject.SqlManager.Connection);
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