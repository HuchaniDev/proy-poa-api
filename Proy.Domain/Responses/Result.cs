using System.Net;

namespace Proy.Domain.Responses;

public class Result<T>
{
    public T Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public HttpStatusCode StatusCode { get; private set; }
    public string Message { get; private set; }
    public List<string>Errors { get; private set; }
    
    private Result(T data, bool isSuccess, HttpStatusCode code,string message, List<string> errors)
    {
        Data = data;
        IsSuccess = isSuccess;
        StatusCode = code;
        Message = message;
        Errors = errors;
    }

    public static Result<T> Success(
        T data,
        HttpStatusCode code
    ) => new Result<T>(
        data,
        true,
        code,
        HttpStatusMessages.GetMessage((int)code),
        new List<string>()
    );
    public static Result<T> Failure(
        List<string> errors,
        HttpStatusCode code
    ) => new Result<T>(
        default(T),
        false,
        code,
        HttpStatusMessages.GetMessage((int)code),
        errors
    );
    
}