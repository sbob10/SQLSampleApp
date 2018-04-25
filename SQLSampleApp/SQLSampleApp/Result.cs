using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSampleApp
{
    public class Result
    {
        public Status Status { get; set; }
        public Message Message { get; set; }
        public string InnerMessage { get; set; }
        public Exception Exception { get; set; }

        public Result()
        {

        }

        public Result(Status status)
        {
            this.Status = status;
            this.Message = Message.NULL;
            this.InnerMessage = "";
            this.Exception = null;
        }

        public Result(Result result)
        {
            this.Status = result.Status;
            this.Message = result.Message;
            this.InnerMessage = result.InnerMessage;
            this.Exception = result.Exception;
        }

        public Result(Message message, Status status = Status.NULL, Exception exception = null)
        {
            this.Status = status;
            this.Message = message;
            this.InnerMessage = "";
            this.Exception = exception;
        }

        public Result(Message message, string innerMessage = "", Status status = Status.NULL, Exception exception = null)
        {
            this.Status = status;
            this.Message = message;
            this.InnerMessage = innerMessage;
            this.Exception = exception;
        }
    }


    public class Result<T> : Result
    {
        //private int result;
        //private string p;

        public T Data { get; set; }

        public Result()
        {

        }

        public Result(T data)
        {
            this.Data = data;
        }

        public Result(Result result)
        {
            this.Status = result.Status;
            this.Message = result.Message;
            this.InnerMessage = result.InnerMessage;
            this.Exception = result.Exception;

        }

        public Result(Message message, string innerMessage = "", Status status = Status.NULL, Exception exception = null)
        {
            this.Status = status;
            this.Message = message;
            this.InnerMessage = innerMessage;
            this.Exception = exception;
        }

        public Result(T data, Result result)
        {
            this.Status = result.Status;
            this.Message = result.Message;
            this.InnerMessage = result.InnerMessage;
            this.Exception = result.Exception;
            this.Data = data;

        }

        public Result(T data, Message message, string innerMessage = "", Status status = Status.NULL, Exception exception = null)
        {
            this.Status = status;
            this.Message = message;
            this.InnerMessage = innerMessage;
            this.Exception = exception;
            this.Data = data;

        }

        public Result(T data, Message message, Status status)
        {
            this.Data = data;
            this.Message = message;
            this.Status = status;
        }


    }


    public enum Status
    {
        Success,
        Warning,
        Error,
        Information,
        NULL
    }

    public enum Message
    {
        NULL,
        NAME_EMPTY_NULL,

        DB_INIT,
        DB_ACCESS,
        
        DB_ENTRY_ADD,
        DB_ENTRIES_LOAD
    }
}
