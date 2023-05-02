using BaseObjectsMVVM;

namespace subdivision.Models.balls_of_criterion
{
    public class CriterionBallsListVM:DictionaryViewModel<CriterionBallsVM,CriterionBallsM,CriterionBallsSql>
    {
        public CriterionBallsListVM(WorkspaceViewModel parent) : base(parent)
        {
            LoadItems();
        } 
    }
}