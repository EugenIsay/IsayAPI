using IsayAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        private readonly SensorsContext _sensorsContext;
        public ApiController(SensorsContext context) 
        {
            _sensorsContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Sensor>>> GetSensors()
        {
            return await _sensorsContext.Sensors.Select(sensor => sensor).ToListAsync();
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Sensor>> GetSensorsItem(int id)
        {
            return await _sensorsContext.Sensors.FindAsync(id) ?? throw new InvalidOperationException();
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<List<Sensor>>> DeleteSensor(int id)
        {
            Sensor? sensor = await _sensorsContext.Sensors.FindAsync(id);
            if (sensor != null) { _sensorsContext.Remove(sensor); }
            await _sensorsContext.SaveChangesAsync();
            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<List<Sensor>>> AddSensor(Sensor sensor)
        {
            _sensorsContext.Add(sensor);
            await _sensorsContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut(template: "{id}")]
        //public async Task<ActionResult<List<Sensor>>> UpdateSensor(int id, Sensor sensor)
        //{

        //}
    }
}
