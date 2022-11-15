namespace LoanCalcAPI.DTOs
{
    /// <summary>
    /// Generic helper that indicates operation success
    /// </summary>
    public class OperationSuccessDTO<T> : OperationResultDTO where T : class
    {
        public T Result { get; set; }
    }
}
