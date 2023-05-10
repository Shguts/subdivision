using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;
using BaseObjectsMVVM;
using subdivision.Models.experts;
using subdivision.Models.Tasks;

namespace subdivision.Models.balls_of_criterion
{
    public class CriterionBallsSql:ModelSql<CriterionBallsM>
    {
         public override int? Create(CriterionBallsM item)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "insert into balls_of_criterion(ExpertID, CriterieID,mark,q) select "
                    +item.ExpertID+",'"+item.CriterieID+"','"+item.mark.ToString(CultureInfo.InvariantCulture)+"','"+item.q.ToString(CultureInfo.InvariantCulture)+"'; select max(ExpertID) from balls_of_criterion",
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
         public override void Update(CriterionBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    " update balls_of_criterion set " +
                    "ExpertID = '" + item.ExpertID +
                    "CriterieID = '" + item.CriterieID +
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

        public override void Delete(CriterionBallsM item)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteCommand(
                    "delete from balls_of_criteries " +
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
        public SQLiteDataAdapter LoadItems(ExpertsVM ExtraVM)
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "SELECT ExpertID, CriterieID, mark, q FROM balls_of_criterion where ExpertID = "+ExtraVM.IdExpert+" order by ExpertID ",MainStaticObject.SqlManager.Connection);
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
        public override SQLiteDataAdapter LoadItems()
        {
            try
            {
                MainStaticObject.SqlManager.Connection.Open();
                var res = new SQLiteDataAdapter(
                    "SELECT ExpertID, CriterieID, mark, q FROM balls_of_criterion order by ExpertID ",MainStaticObject.SqlManager.Connection);
                MainStaticObject.SqlManager.Connection.Close();
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show("err 4" + e.Message);
            }

            return null;
        }       
    }
}