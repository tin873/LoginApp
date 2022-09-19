using Helper;
using System.Net;

namespace Core.Model
{
    public abstract class BaseResult
    {
        public virtual string Status { get; set; } = "Ok";
        public virtual HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
        public virtual string Message { get; set; } = "Success";
        public virtual string SystemName { get => $"{AppConstants.AppName} API"; }
    }
}
