using General.Core.Entities;
using General.Core.Entities.Abstract;
using General.Core.Entities.Concrete;

namespace General.Entities.Complex.Requests
{
    public class RequestService<TDto> : IRequestService
          where TDto : IBaseEntity, new()
    {
        public RequestService(TDto data)
        {
            Data = data;
        }

        public Request Request { get; set; }
        public TDto Data { get; set; }
        public Enums.RequestTypes RequestType { get; set; }

    }
}
