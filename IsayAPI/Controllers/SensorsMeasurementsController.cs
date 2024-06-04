using IsayAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsMeasurementsController : ControllerBase
    {
        private readonly User8Context _smContext;
        public SensorsMeasurementsController(User8Context context)
        {
            _smContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SensorsMeasurement>>> GetMesType()
        {
            return await _smContext.SensorsMeasurements.Select(SensorsMeasurement => SensorsMeasurement).ToListAsync();
        }
    }
}