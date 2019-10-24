using System;

namespace Common.Framework
{
    public static class ResultExtensions
    {
        public static IResult ThrowIfFail(this IResult obj, string keyword = "Message")
        {
            if (obj.code != 200)
                throw new Exception(string.Format("{0} : {1}", keyword, obj.message));
            return obj;
        }
        public static IResult<T> ThrowIfFail<T>(this IResult<T> obj, string keyword = "Message")
        {
            return ((IResult)obj).ThrowIfFail(keyword) as IResult<T>;
        }
    }
}
