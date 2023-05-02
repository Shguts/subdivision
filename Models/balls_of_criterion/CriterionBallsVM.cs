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
            throw new System.NotImplementedException();
        }
    }
}