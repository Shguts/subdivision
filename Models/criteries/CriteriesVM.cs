using System;
using System.Data;
using BaseObjectsMVVM;
using subdivision.Models.experts;

namespace subdivision.Models.criteries
{
    public class CriteriesVM : EntityViewModel<CriteriesM, CriteriesSql>
    {
        public CriteriesVM()
        {


        }

        public CriteriesVM(DataRow row)
        {
            ParseArguments(row);

        }

        public override void ParseArguments(DataRow row)
        {
            Item.ItemId = Int32.Parse(row.ItemArray[0].ToString());
            Item.CriteriesName = row.ItemArray[1].ToString();
        }

        public int? IdCriterie
        {
            get => Item.ItemId;
            set
            {
                Item.ItemId = value;
                OnPropertyChanged(() => IdCriterie);
            }

        }   

        public string CriteriesName
        {
            get => Item.CriteriesName;
            set { SetPropertyValue(() => CriteriesName, value, ref Item.CriteriesName); }
        }
    }
}