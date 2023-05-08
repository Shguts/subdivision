using System;
using System.Data;
using BaseObjectsMVVM;
using subdivision.Models.balls_of_criterion;

namespace subdivision.Models.balls_of_subunits
{
    public class SubunitsBallsVM:EntityViewModel<SubunitsBallsM, SubunitsBallsSql>
    {
        public SubunitsBallsVM()
        {


        }

        public SubunitsBallsVM(DataRow row)
        {
            ParseArguments(row);

        }

        public override void ParseArguments(DataRow row)
        {
            Item.ExpertID= Int32.Parse(row.ItemArray[0].ToString());
            Item.CriterieID= Int32.Parse(row.ItemArray[1].ToString());
            Item.TaskID= Int32.Parse(row.ItemArray[2].ToString());
            Item.SubunitID= Int32.Parse(row.ItemArray[3].ToString());
            Item.mark = Double.Parse(row.ItemArray[4].ToString());
        }
        public int ExpertID
        {
            get => Item.ExpertID;
            set
            {
                Item.ExpertID = value;
                OnPropertyChanged(() => ExpertID);
            }
            
        }
        public int CriterieID
        {
            get => Item.CriterieID;
            set
            {
                Item.CriterieID = value;
                OnPropertyChanged(() => CriterieID);
            }
            
        }
        public int TaskID
        {
            get => Item.TaskID;
            set
            {
                Item.TaskID = value;
                OnPropertyChanged(() => TaskID);
            }
            
        }
        public int SubunitID
        {
            get => Item.SubunitID;
            set
            {
                Item.SubunitID = value;
                OnPropertyChanged(() => SubunitID);
            }
            
        }
        public double mark
        {
            get => Item.mark;
            set
            {
                Item.mark = value;
                OnPropertyChanged(() => mark);
            }
            
        }
    }
}