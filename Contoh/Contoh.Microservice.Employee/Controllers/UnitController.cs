using Contoh.Microservice.Employee.Interfaces;
using Contoh.Microservice.Employee.Models;
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



        [HttpGet]
        [Route("{id}")]
        public ResponseBase<Domain.Entities.Unit> Get(int id)
        {
            return _unitService.Get(id);
        }

        [HttpPost]
        [Route("add")]
        public ResponseBase<Domain.Entities.Unit> Add(Domain.Entities.Unit unit)
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
