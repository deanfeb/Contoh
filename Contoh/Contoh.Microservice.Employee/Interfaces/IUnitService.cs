
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Domain.Entities;

namespace Contoh.Microservice.Employee.Interfaces
{
    public interface IUnitService
    {
        ResponseBase<Domain.Entities.Unit> Add(Domain.Entities.Unit obj);
        ResponseBase<Domain.Entities.Unit> Get(int id);

        ResponseBase<Domain.Entities.Unit> Update(Domain.Entities.Unit obj);

        ResponseBase<Domain.Entities.Unit> Delete(int id);

        LoadResult GetList(DataSourceLoadOptions loadOptions);
        LoadResult GetList2(DataSourceLoadOptions loadOptions);

    }
}
