using Contoh.Microservice.Employee.Models;

namespace Contoh.Microservice.Employee.Interfaces
{
    public interface IUnitService
    {
        ResponseBase<Domain.Entities.Unit> Add(Domain.Entities.Unit obj);
        ResponseBase<Domain.Entities.Unit> Get(int id);

        ResponseBase<Domain.Entities.Unit> Update(Domain.Entities.Unit obj);

        ResponseBase<Domain.Entities.Unit> Delete(int id);

    }
}
