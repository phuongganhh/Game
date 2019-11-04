using System;
using System.Net;
using System.Threading.Tasks;

namespace Common
{
    public interface ICommand : IDisposable
    {
        Task<Result> Execute(ObjectContext context);
    }

    public interface ICommand<T> : IDisposable
    {
        Task<Result<T>> Execute(ObjectContext context);
    }

    public abstract class CommandBase : ICommand
    {
        protected virtual Task ValidateCore(ObjectContext context)
        {
            return Task.CompletedTask;
        }
        protected virtual Task OnExecutingCore(ObjectContext context)
        {
            return Task.CompletedTask;
        }
        protected virtual Task OnExecutedCore(ObjectContext context, Result result)
        {
            return Task.CompletedTask;
        }
        protected abstract Task<Result> ExecuteCore(ObjectContext context);
        public async Task<Result> Execute(ObjectContext context)
        {
            try
            {
                await ValidateCore(context);
                await OnExecutingCore(context);
                var result = await ExecuteCore(context);
                await OnExecutedCore(context, result);
                return result;
            }
            catch (BusinessException ex)
            {
                LoggerManager.Logger.Info(ex.Message);
                return new Result
                {
                    code = (int)ex.exit_code,
                    message = ex.Message
                };
            }
            catch(Exception ex)
            {
                LoggerManager.Logger.Error(ex, 500.ToString());
                return new Result
                {
                    code = (int)HttpStatusCode.InternalServerError,
                    message = "Đã xảy ra lỗi không xác định!"
                };
            }
        }

        public virtual void Dispose()
        {
        }

        protected Task<Result> Success(string message = "Success")
        {
            var r = new Result
            {
                message = message
            };
            return Task.Run(() =>
            {
                return r;
            });
        }
    }

    public abstract class CommandBase<T> : ICommand<T>
    {

        /// <summary>
        /// Validate before execute a command. Base validation does nothing
        /// </summary>
        protected virtual Task ValidateCore(ObjectContext context)
        {
            return Task.CompletedTask;
        }
        protected virtual Task OnExecutingCore(ObjectContext context)
        {
            return Task.CompletedTask;
        }
        protected virtual Task OnExecutedCore(ObjectContext context, Result<T> result)
        {
            return Task.CompletedTask;
        }
        protected abstract Task<Result<T>> ExecuteCore(ObjectContext context);
        public async Task<Result<T>> Execute(ObjectContext context)
        {
            try
            {
                await ValidateCore(context);
                await OnExecutingCore(context);
                var result = await ExecuteCore(context);
                await OnExecutedCore(context, result);
                return result;
            }
            catch (BusinessException ex)
            {
                LoggerManager.Logger.Info(ex.Message);
                return new Result<T>
                {
                    code = (int)ex.exit_code,
                    message = ex.Message
                };
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Error(ex,500.ToString());
                return new Result<T>
                {
                    code = (int)HttpStatusCode.InternalServerError,
                    message = "Đã xảy ra lỗi không xác định!"
                };
            }
        }
        public virtual void Dispose()
        {
        }
        
        protected Task<Result<T>> Success(T data,IPaging paging = null, string message = "Success")
        {
            var r =  new Result<T>
            {
                data = data,
                message = message,
                paging = paging
            };
            return Task.Run(() =>
            {
                return r;
            });
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
            this.code = 200;
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
        long? count { get; set; }
        long? current_page { get; set; }
        long? page_size { get; set; }
    }

    public class Paging : IPaging
    {
        public long? count { get;set; }
        public long? current_page { get;set; }
        public long? page_size { get;set; }
    }

}
