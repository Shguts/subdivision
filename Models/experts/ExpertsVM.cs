using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using BaseObjectsMVVM;

namespace subdivision.Models.experts
{
    public class ExpertsVM:EntityViewModel<ExpertsM,ExpertsSql> 
    {
        public ExpertsVM(DataRow row)
        {
            ParseArguments(row);
        }
        public ExpertsVM()
        {
        }

        public override void ParseArguments(DataRow row)
        {
            Item.ItemId = Int32.Parse(row.ItemArray[0].ToString());
            Item.FirstName = row.ItemArray[1].ToString();
            Item.SecondName = row.ItemArray[2].ToString();
            Item.LastName= row.ItemArray[3].ToString();
            Item.SumOfBall= Int32.Parse(row.ItemArray[4].ToString());
            Item.password= row.ItemArray[5].ToString();
            
        }

        public int? IdExpert
        {
            get => Item.ItemId;
            set
            {
                Item.ItemId = value;
                OnPropertyChanged(() => IdExpert);
            }
            
        }
        public string FirstName
        {
            get => Item.FirstName;
            set
            {
                SetPropertyValue(() => FirstName, value, ref Item.FirstName);
            }
            
        }
        public string SecondName
        {
            get => Item.SecondName;
            set
            {
                SetPropertyValue(() => SecondName, value, ref Item.SecondName);
            }
            
        }
        public string LastName
        {
            get => Item.LastName;
            set
            {
                SetPropertyValue(() => LastName, value, ref Item.LastName);
            }
        }

        public int SumOfBall
        {
            get => Item.SumOfBall;
            set
            {
                SetPropertyValue(() => SumOfBall, value, ref Item.SumOfBall);
            }
        }
        public string Password
        {
            get => Item.password;
            set
            {
                SetPropertyValue(() => Password, value, ref Item.password);
            }
        }
    }
    
}