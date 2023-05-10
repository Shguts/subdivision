using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_tasks;
using subdivision.Models.criteries;
using subdivision.Models.experts;
using subdivision.Models.Tasks;

namespace subdivision.Models.balls_of_subunits
{
    public class SubunitsBallsSql:ModelSql<SubunitsBallsM>
    {
        public override int? Create(SubunitsBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "insert into balls_of_subunits(ExpertID, CriterieID, TaskID , SubunitID, mark,q) select "
                    +item.ExpertID+",'"+item.CriterieID+"','"+item.TaskID+"','"+item.SubunitID+"','"+item.mark.ToString(CultureInfo.InvariantCulture)+"','"+item.q.ToString(CultureInfo.InvariantCulture)+"'; select max(ExpertID) from balls_of_subunits",
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

        public override void Update(SubunitsBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    " update balls_of_subunits set " +
                    "ExpertID = '" + item.ExpertID +
                    "CriterieID = '" + item.CriterieID +
                    "TaskID = '" + item.TaskID +
                    "SubunitID = '" + item.SubunitID +
                    "mark = '" + item.mark +
                    "q = '" + item.q +
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

        public override void Delete(SubunitsBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    "delete from balls_of_subunits " +
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
                    "SELECT ExpertID, CriterieID, TaskID, SubunitID, mark, q FROM balls_of_subunits order by ExpertID ",MainStaticObject.SqlManager.Connection);
                MainStaticObject.SqlManager.Connection.Close();
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show("err 4" + e.Message);
            }

            return null;
        }
        public SQLiteDataAdapter LoadItems(ExpertsVM ExtraVM,CriteriesVM CrteriaVM,TasksVM tasksVm)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "SELECT ExpertID, CriterieID,TaskID, SubunitID, mark, q FROM balls_of_subunits where ExpertID = "+ExtraVM.IdExpert+" and " +
                    "CriterieID = "+CrteriaVM.IdCriterie+" and TaskID = "+tasksVm.IdTask+" order by ExpertID ",MainStaticObject.SqlManager.Connection);
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