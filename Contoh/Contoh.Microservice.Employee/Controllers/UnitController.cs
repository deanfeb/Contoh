using Contoh.Microservice.Employee.Interfaces;

using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contoh.Microservice.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }


        [HttpPost]
        [Route("GetList")]
        public LoadResult GetList(DataSourceLoadOptions loadOptions)
        {
            return _unitService.GetList(loadOptions);
        }

        [HttpPost]
        [Route("GetList2")]
        public LoadResult GetList2(DataSourceLoadOptions loadOptions)
        {
            return _unitService.GetList2(loadOptions);
        }


        [HttpGet]
        [Route("{id}")]
        public ResponseBase<Domain.Entities.Unit> Get(int id)
        {
            return _unitService.Get(id);
        }

        [HttpPost]
        [Route("add")]
        public ResponseBase<Domain.Entities.Unit> Add([FromBody] Domain.Entities.Unit unit)
        {
            return _unitService.Add(unit);
        }

        [HttpPost]
        [Route("del/{id}")]
        public ResponseBase<Domain.Entities.Unit> GetWeatherTypes(int id)
        {
            return _unitService.Delete(id);
        }

        [HttpPost]
        [Route("update")]
        public ResponseBase<Domain.Entities.Unit> Update([FromBody] Domain.Entities.Unit unit)
        {
            return _unitService.Update(unit);
        }



    }
}
