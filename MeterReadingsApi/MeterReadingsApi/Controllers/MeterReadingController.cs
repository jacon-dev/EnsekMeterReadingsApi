using System.Collections.Generic;
using System.Linq;
using MeterReadingsApi.Core;
using MeterReadingsApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace MeterReadingsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingController : ControllerBase
    {
        IDataAccess dataAccess;

        public MeterReadingController(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public IEnumerable<MeterReading> Get()
        {
            return dataAccess.GetMeterReadings().ToList();
        }
    }
}
