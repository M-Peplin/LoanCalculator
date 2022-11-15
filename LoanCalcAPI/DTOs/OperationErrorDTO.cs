namespace LoanCalcAPI.DTOs
{
    /// <summary>
    /// Helper that indicates operation error
    /// </summary>
    public class OperationErrorDTO : OperationResultDTO
    {
        public int Code { get; set; }
    }
}
