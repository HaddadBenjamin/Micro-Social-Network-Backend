using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals
{
    public interface IHalService<Dto>
    {
        HALResponse AddLinks(Dto dto);
    }
}