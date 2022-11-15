using System.ComponentModel.DataAnnotations;

namespace LoanCalcAPI.Models
{
    /// <summary>
    /// Loan type database entity
    /// </summary>
    public class Loan
    {        
        [Key]
        public int IdLoan { get; set; }
      
        public string? LoanName { get; set; }
       
        public double Interest { get; set; }
    }
}
