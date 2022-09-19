using System.Net;

namespace Core.Model
{
    public class ApiResult : BaseResult
    {
        /// <summary>
        /// Trả về success mà không cần data
        /// </summary>
        /// <param name="code">Http status code</param>
        /// <param name="message">Message thông báo</param>
        /// <returns></returns>
        public static ApiResult Ok(HttpStatusCode code = HttpStatusCode.OK, string message = "Success")
        {
            return new ApiResult
            {
                Code = code,
                Message = message
            };
        }

        /// <summary>
        /// Trả về error mà không cần data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResult Error(
            string message
            , HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            return new ApiResult
            {
                Message = message,
                Status = "NOTOK",
                Code = code,
            };
        }
    }

    public class ApiResult<TData> : BaseResult
    {
        /// <summary>
        /// Trả về sccuess kèm data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResult<TData> Ok(TData data, HttpStatusCode code = HttpStatusCode.OK, string message = "Success")
        {
            return new ApiResult<TData>
            {
                Data = data,
                Code = code,
                Message = message
            };
        }

        /// <summary>
        /// Trả về lồi kèm data
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResult<TData> Error(
            string message
            , TData data = default
            , HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            return new ApiResult<TData>
            {
                Message = message,
                Status = "NOTOK",
                Data = data,
                Code = code,
            };
        }
        public TData Data { get; set; } = default;
    }
}
