using Contoh.Microservice.Employee.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Contoh.Microservice.Employee.Services
{
    public class EmployeeService : Interfaces.IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseBase<Domain.Entities.Employee> Add(Domain.Entities.Employee emp)
        {
            var response = new ResponseBase<Domain.Entities.Employee>() {  Success= SuccessType.Success, Message= "Success" };

            try
            {
                emp.Created_at = DateTime.Now;
                emp.Created_by = 1;
                _unitOfWork.Employees.Add(emp);
                _unitOfWork.Complete();
                response.Data = emp;
            }
            catch (Exception ex)
            {
                response.Success = SuccessType.Failed;
                response.Message = ex.Message + (ex.InnerException==null?"":ex.InnerException.Message);
                //throw;
            }
            return response;

        }

    }
}
