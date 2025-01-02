using System.Net;

namespace AIMSV2.Utility;

/// <summary>
///     Custom api result object to provide generic way of communication
/// </summary>
public class Result
{
    /// <summary>
    ///     Gets and Sets <see cref="StatusType"/>.
    /// </summary>
    public StatusType Status { get; set; }
    /// <summary>
    ///     Gets and Sets <see cref="HttpStatusCode"/>.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }
    /// <summary>
    ///     Gets and Sets Message.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    ///     Gets and Sets Message Code.
    /// </summary>
    public string? MessageCode { get; set; }
    /// <summary>
    ///     Gets and Sets <see cref="Exception"/>.
    /// </summary>
    public Exception? ResultException { get; set; }
    /// <summary>
    ///     Gets and Sets Result Object.
    /// </summary>
    public object? ResultObject { get; set; }
    /// <summary>
    ///     Gets and Sets Result Object 2.
    /// </summary>
    public object? ResultObject2 { get; set; }
    /// <summary>
    ///     Indicates whethere there is error or not. 
    /// </summary>
    public bool HasError
    {
        get
        {
            return (Status == StatusType.Error);
        }
    }
    /// <summary>
    ///     Indicates whethere there is WebApi error or not. 
    /// </summary>
    public object WebApiError
    {
        get
        {
            return new { Message, MessageCode };
        }
    }
    /// <summary>
    ///     Gets Api Result of specified <see cref="Result"/> object.
    /// </summary>
    public object ApiResult
    {
        get
        {
            if (HttpStatusCode == HttpStatusCode.OK)
            {
                return new { StatusCode = (int)HttpStatusCode, Data = ResultObject };
            }
            else
            {
                return new { StatusCode = (int)HttpStatusCode, Message, MessageCode, Data = ResultObject };
            }
        }
    }
    /// <summary>
    ///     Gets Error Message of specified <see cref="Result"/> object.
    /// </summary>
    public string? ErrorMessage
    {
        get
        {
            return Message
                +
                ((null != ResultException && !string.IsNullOrEmpty(ResultException.StackTrace)) ? "; Error: " + ResultException.Message + "; StackTrace: " + ResultException.StackTrace : "");
        }
    }
    /// <summary>
    ///     Gets Status Code of specified <see cref="Result"/> object.
    /// </summary>
    public int StatusCode
    {
        get
        {
            return (int)HttpStatusCode;
        }
    }

    /// <summary>
    ///     Initialize the Result with default parameters
    /// </summary>
    public Result()
    {
        this.Status = StatusType.Success;
        this.HttpStatusCode = HttpStatusCode.OK;
    }
    /// <summary>
    ///     Initialize the Result with specified parameters
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exp"></param>
    public Result(string message, Exception? exp)
    {
        this.Status = StatusType.Error;
        this.HttpStatusCode = HttpStatusCode.BadRequest;
        this.Message = message;
        if (null != exp)
        {
            this.ResultException = exp;
        }
    }
    /// <summary>
    ///     Initialize the Result with specified parameters
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageCode"></param>
    /// <param name="httpStatusCode"></param>
    /// <param name="exp"></param>
    public Result(string message, string? messageCode = null, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, Exception? exp = null)
    {
        this.Status = StatusType.Error;
        this.Message = message;
        this.MessageCode = messageCode;
        this.HttpStatusCode = httpStatusCode;
        if (null != exp)
        {
            this.ResultException = exp;
        }
    }
}

/// <summary>
///     Enum to specify status types
/// </summary>
public enum StatusType
{
    Success,
    Error
}
