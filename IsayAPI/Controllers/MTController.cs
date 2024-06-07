using IsayAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MTController: ControllerBase
    {
        private readonly User8Context _mtContext;
        public MTController(User8Context context) 
        {
            _mtContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<MeasurementsType>>> GetMesType()
        {
            return await _mtContext.MeasurementsTypes.Select(Measurement_Type =>  Measurement_Type).ToListAsync();
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<MeasurementsType>> GetMesTypeItem(int id)
        {
            return await _mtContext.MeasurementsTypes.FindAsync(id) ?? throw new InvalidOperationException();
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<List<MeasurementsType>>> DeleteMesType(int id)
        {
            MeasurementsType? mt = await _mtContext.MeasurementsTypes.FindAsync(id);
            List<SensorsMeasurement> ToDel = _mtContext.SensorsMeasurements.Where(e => e.TypeId == id).ToList();
            if (mt != null && _mtContext.Measurements.Where(e => e.MeasurementType == id).Count() == 0)
            {
                foreach (var SM in ToDel)
                {
                    _mtContext.SensorsMeasurements.Remove(SM);
                }
                _mtContext.MeasurementsTypes.Remove(mt);
            }
            await _mtContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPost]
        public async Task<ActionResult<List<MeasurementsType>>> AddMesType(MeasurementsType mt)
        {
            _mtContext.MeasurementsTypes.Add(mt);
            await _mtContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut(template: "{id}, {sensor}")]
        public async Task<ActionResult<List<MeasurementsType>>> UpdateMesType(int id, MeasurementsType mt)
        {
            MeasurementsType need_mt = await _mtContext.MeasurementsTypes.FindAsync(id);
            
            if (need_mt != null)
            {
                need_mt.TypeId = mt.TypeId;
                need_mt.TypeName = mt.TypeName;
                need_mt.TypeUnits = mt.TypeUnits;
                await _mtContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
