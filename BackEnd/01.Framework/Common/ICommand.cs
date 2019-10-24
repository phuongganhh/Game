using System;
using System.Net;

namespace Common
{
    public interface ICommand : IDisposable
    {
        Result Execute(ObjectContext context);
    }

    public interface ICommand<T> : IDisposable
    {
        Result<T> Execute(ObjectContext context);
    }

    public abstract class CommandBase : ICommand
    {
        protected virtual void ValidateCore(ObjectContext context)
        {
        }
        protected virtual void OnExecutingCore(ObjectContext context)
        {
        }
        protected virtual void OnExecutedCore(ObjectContext context, Result result)
        {
        }
        protected abstract Result ExecuteCore(ObjectContext context);
        public Result Execute(ObjectContext context)
        {
            try
            {
                ValidateCore(context);
                OnExecutingCore(context);
                var result = ExecuteCore(context);
                OnExecutedCore(context, result);
                return result;
            }
            catch (BusinessException ex)
            {
                return new Result
                {
                    code = (int)ex.exit_code,
                    message = ex.Message
                };
            }
            catch(Exception ex)
            {
                return new Result
                {
                    code = (int)HttpStatusCode.InternalServerError,
                    message = ex.Message
                };
            }
        }

        public virtual void Dispose()
        {
        }

        protected Result Success(string message = "Success")
        {
            return new Result
            {
                code = 0,
                message = message
            };
        }
    }

    public abstract class CommandBase<T> : ICommand<T>
    {

        /// <summary>
        /// Validate before execute a command. Base validation does nothing
        /// </summary>
        protected virtual void ValidateCore(ObjectContext context)
        {
        }
        protected virtual void OnExecutingCore(ObjectContext context)
        {
        }
        protected virtual void OnExecutedCore(ObjectContext context, Result<T> result)
        {
        }
        protected abstract Result<T> ExecuteCore(ObjectContext context);
        public Result<T> Execute(ObjectContext context)
        {
            try
            {
                ValidateCore(context);
                OnExecutingCore(context);
                var result = ExecuteCore(context);
                OnExecutedCore(context, result);
                return result;
            }
            catch (BusinessException ex)
            {
                return new Result<T>
                {
                    code = (int)ex.exit_code,
                    message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    code = (int)HttpStatusCode.InternalServerError,
                    message = ex.Message
                };
            }
        }
        public virtual void Dispose()
        {
        }
        
        protected Result<T> Success(T data, string message = "Success")
        {
            return new Result<T>
            {
                data = data,
                code = 0,
                message = message
            };
        }
    }

    public interface IResult
    {
        string message { get; set; }
        int? code { get; set; }
    }

    public interface IResult<T> : IResult
    {
        T data { get; set; }
    }

    public class Result : IResult
    {
        public string message { get; set; }
        public int? code { get; set; }
        public Result()
        {
            this.code = 0;
            this.message = "Success";
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T data { get; set; }
        public IPaging paging { get; set; }
    }

    public interface IPaging
    {
        long? total { get; set; }
        long? current_page { get; set; }
        long? page_size { get; set; }
    }

    public class Paging : IPaging
    {
        public long? total { get;set; }
        public long? current_page { get;set; }
        public long? page_size { get;set; }
    }

}
