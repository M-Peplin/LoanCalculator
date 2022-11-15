using LoanCalcAPI.Models;

namespace LoanCalcAPI.Helpers
{
    /// <summary>
    /// Helper providing instantiation of proper loan type.
    /// </summary>
    public static class LoanTypes
    {
        public enum LoanType
        {
            HousingLoan = 1,            
        }
        /// <summary>
        /// Based on loan Id from entity, different derived loans will be created.
        /// To be improved: Types should not depend on Id, but on additional database
        /// column containing enum, e.g. CODE.
        /// </summary>
        public static LoanBase GetLoanType(LoanType loanType)
        {
            switch (loanType)
            {
                case LoanType.HousingLoan: return new HousingLoan();
                default: return new HousingLoan();                  
            }
        }       


    }
}
