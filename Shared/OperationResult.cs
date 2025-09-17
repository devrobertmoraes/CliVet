namespace CliVet.Shared
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public IReadOnlyCollection<string> Errors { get; private set; }
        public ErrorType? ErrorType { get; private set; } // anulável, em caso de sucesso não há erro.

        private OperationResult(bool isSuccess, T data, IReadOnlyCollection<string> errors, ErrorType? errorType)
        {
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
            ErrorType = errorType;
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>(true, data, new List<string>(), null);
        }

        public static OperationResult<T> Fail(string error)
        {
            return new OperationResult<T>(false, default(T), new List<string> { error }, Shared.ErrorType.Validation);
        }

        public static OperationResult<T> Fail(IEnumerable<string> errors)
        {
            return new OperationResult<T>(false, default(T), errors.ToList(), Shared.ErrorType.Validation);
        }

        public static OperationResult<T> NotFound(string message = "Recurso não encontrado.")
        {
            return new OperationResult<T>(false, default(T), new List<string> { message }, Shared.ErrorType.NotFound);
        }
    }
}
