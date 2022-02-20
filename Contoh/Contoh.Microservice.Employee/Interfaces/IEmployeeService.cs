

using Domain.Entities;

namespace Contoh.Microservice.Employee.Interfaces
{
    public interface IEmployeeService
    {
        ResponseBase<Domain.Entities.Employee> Add(Domain.Entities.Employee emp);

    }
}
