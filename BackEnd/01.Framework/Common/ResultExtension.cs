using System;
using System.Threading.Tasks;

namespace Common.Framework
{
    public static class ResultExtensions
    {
        public static Task ThrowIfFail(this Task obj, string keyword = "Message")
        {
            if (obj.Exception != null)
                throw new Exception(string.Format("{0} : {1}", keyword, obj.Exception.Message));
            return obj;
        }
        public static Task<T> ThrowIfFail<T>(this Task<T> obj, string keyword = "Message")
        {
            if(obj.Exception != null)
            {
                throw new Exception(string.Format("{0} : {1}", keyword, obj.Exception.Message));
            }
            return obj;
        }
    }
}
