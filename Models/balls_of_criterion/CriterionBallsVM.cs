using System;
using System.Data;
using BaseObjectsMVVM;
using subdivision.Models.criteries;

namespace subdivision.Models.balls_of_criterion
{
    public class CriterionBallsVM:EntityViewModel<CriterionBallsM, CriterionBallsSql>
    {
        public CriterionBallsVM()
        {


        }

        public CriterionBallsVM(DataRow row)
        {
            ParseArguments(row);

        }

        public override void ParseArguments(DataRow row)
        {
            Item.ExpertID= Int32.Parse(row.ItemArray[0].ToString());
            Item.CriterieID= Int32.Parse(row.ItemArray[1].ToString());
            Item.mark = Decimal.Parse(row.ItemArray[2].ToString());
            Item.q = Double.Parse(row.ItemArray[3].ToString());
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
        public double q
        {
            get => Item.q;
            set
            {
                Item.q = value;
                OnPropertyChanged(() => q);
            }
            
        }
        public decimal mark
        {
            get => Convert.ToDecimal(Item.mark);
            set
            {
                Item.mark = value;
                OnPropertyChanged(() => mark);
            }
            
        }
        
        
    }
}