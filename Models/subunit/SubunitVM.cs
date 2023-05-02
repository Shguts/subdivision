using System;
using System.Data;
using BaseObjectsMVVM;
using subdivision.Models.experts;

namespace subdivision.Models.subunit
{
    public class SubunitVM:EntityViewModel<SubunitM,SubunitSql> 
    {
        public SubunitVM(DataRow row)
        {
            ParseArguments(row);
        }
        public SubunitVM()
        {
        }

        public override void ParseArguments(DataRow row)
        {
            Item.ItemId = Int32.Parse(row.ItemArray[0].ToString());
            Item.SubunitName = row.ItemArray[1].ToString();
        }

        public int? IdSubunit
        {
            get => Item.ItemId;
            set
            {
                Item.ItemId = value;
                OnPropertyChanged(() => IdSubunit);
            }
            
        }
        public string SubunitName
        {
            get => Item.SubunitName;
            set
            {
                SetPropertyValue(() => SubunitName, value, ref Item.SubunitName);
            }
            
        }
    }
}