using Domain.Enums;
namespace Domain.CoreServices
{
    public class OperationResult<T>
    {
        public T? Data { get; set; }

        public string? Message { get; set; }

        public Statuses StatusCode { get; set; }

        public string? ExceptionDetails { get; set; }

        public bool IsSuccess() => StatusCode == Statuses.Success;


        public OperationResult<T> FailedResult(string? message = "Operation is failed")
           => new OperationResult<T> { Data = default, Message = message, StatusCode = Statuses.Failed, };

        public OperationResult<T> NotExistResult(string? message = "Not Exist..")
        => new OperationResult<T> { Data = default, Message = message, StatusCode = Statuses.NotExist, };

        public OperationResult<T> ForbiddenResult(string? message = "Operation is forbidden..")
          => new OperationResult<T> { Data = default, Message = message, StatusCode = Statuses.Forbidden, };

        public OperationResult<T> UnauthorizedResult(string? message = "Operation is unauthorized..")
        => new OperationResult<T> { Data = default, Message = message, StatusCode = Statuses.Unauthorized, };

        public OperationResult<T> ExceptionResult(string? message = "Server Exception..")
       =>  new OperationResult<T> { Data = default, Message = message, StatusCode = Statuses.Exception };

        public OperationResult<T> SuccessResult(T data, string? message = "Operation is succeeded")
         => new OperationResult<T> { Data = data, Message = message, StatusCode = Statuses.Success, };
        //private Statuses _status;
        //public Statuses Status
        //{
        //    get => _status;
        //    set
        //    {
        //        _status = value;
        //        StatusCode =(int)_status;
        //    }
        //}
        //public void Success() => IsSuccess = Statuses.Success;
        //public void Failure() => Statuses
    }

}
