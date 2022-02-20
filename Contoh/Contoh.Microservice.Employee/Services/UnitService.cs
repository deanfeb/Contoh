using Contoh.Microservice.Employee.Interfaces;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Domain.Entities;
using Domain.Interfaces;

namespace Contoh.Microservice.Employee.Services
{
    public class UnitService : Interfaces.IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseBase<Domain.Entities.Unit> Add(Domain.Entities.Unit emp)
        {
            var response = new ResponseBase<Domain.Entities.Unit>() { Success = SuccessType.Success, Message = "Success" };

            try
            {
                emp.Created_at = DateTime.Now;
                emp.Created_by = 1;
                _unitOfWork.Units.Add(emp);
                _unitOfWork.Complete();
                response.Data = emp;
            }
            catch (Exception ex)
            {
                response.Success = SuccessType.Failed;
                response.Message = ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message);
                //throw;
            }
            return response;

        }

        public ResponseBase<Unit> Delete(int id)
        {
            var response = new ResponseBase<Domain.Entities.Unit>() { Success = SuccessType.Success, Message = "Success" };

            try
            {
                var unit = _unitOfWork.Units.GetById(id);
                unit.IsActive = false;
                _unitOfWork.Complete();
                response.Data = unit;
            }
            catch (Exception ex)
            {
                response.Success = SuccessType.Failed;
                response.Message = ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message);
                //throw;
            }
            return response;
        }

        public ResponseBase<Unit> Get(int id)
        {
            var response = new ResponseBase<Domain.Entities.Unit>() { Success = SuccessType.Success, Message = "Success" };

            try
            {
                var unit = _unitOfWork.Units.GetById(id);
                response.Data = unit;
            }
            catch (Exception ex)
            {
                response.Success = SuccessType.Failed;
                response.Message = ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message);
                //throw;
            }
            return response;
        }

        public LoadResult GetList(DataSourceLoadOptions loadOptions)
        {
            var data = _unitOfWork.Units.GetAllQueryable();

            return DataSourceLoader.Load(data, loadOptions);
        }

        public LoadResult GetList2(DataSourceLoadOptions loadOptions)
        {
            var data = _unitOfWork.Units.GetQueryable();

            return DataSourceLoader.Load(data, loadOptions);
        }

        public ResponseBase<Unit> Update(Unit obj)
        {
            var response = new ResponseBase<Domain.Entities.Unit>() { Success = SuccessType.Success, Message = "Success" };

            try
            {
                var unit = _unitOfWork.Units.GetById(obj.Id);
                unit.Code = obj.Code;
                unit.Name = obj.Name;
                unit.IsActive = obj.IsActive;

                _unitOfWork.Complete();
                response.Data = unit;
            }
            catch (Exception ex)
            {
                response.Success = SuccessType.Failed;
                response.Message = ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message);
                //throw;
            }
            return response;
        }
    }
}
