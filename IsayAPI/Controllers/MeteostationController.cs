using IsayAPI.Models;
using IsayAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeteostationController: ControllerBase
    {
        private readonly User8Context _meteostationsContext;
        public MeteostationController(User8Context context) 
        {
            _meteostationsContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<MeteostationResponse>>> GeMeteostations()
        {

            var meteostations = await _meteostationsContext.Meteostations
                .Select(m => new MeteostationResponse {StationId = m.StationId, StationName = m.StationName, StationLatitude = m.StationLatitude, StationLongitude = m.StationLongitude }).ToListAsync();
            foreach (var meteostation in meteostations)
            {
                meteostation.meteostationsSensors = await _meteostationsContext.MeteostationsSensors.Where(mes => mes.StationId == meteostation.StationId).Join(
                    _meteostationsContext.Sensors,
                    m => m.SensorId,
                    s => s.SensorId,
                    (m, s) => new MeteostationsSensorsResponse
                    {
                        SensorInventoryNumber = m.SensorInventoryNumber,
                        SensorId = s.SensorId,
                        SensorName = s.SensorName,
                        AddedTs = m.AddedTs,
                        RemovedTs = m.RemovedTs
                    }
                    ).ToListAsync();
            }
            return meteostations;
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<MeteostationResponse>> GetMeteostationsItem(int id)
        {
            Meteostation ms = _meteostationsContext.Meteostations.Find(id);
            var meteostation = new MeteostationResponse { StationId = ms.StationId, StationName = ms.StationName, StationLatitude = ms.StationLatitude, StationLongitude = ms.StationLongitude };
            meteostation.meteostationsSensors = await _meteostationsContext.MeteostationsSensors.Where(mes => mes.StationId == meteostation.StationId).Join(
                _meteostationsContext.Sensors,
                m => m.SensorId,
                s => s.SensorId,
                (m, s) => new MeteostationsSensorsResponse
                {
                    SensorInventoryNumber = m.SensorInventoryNumber,
                    SensorId = s.SensorId,
                    SensorName = s.SensorName,
                    AddedTs = m.AddedTs,
                    RemovedTs = m.RemovedTs
                }
                ).ToListAsync();
            return meteostation ?? throw new InvalidOperationException();
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<List<Meteostation>>> DeleteSensor(int id)
        {
            Meteostation? meteostation = await _meteostationsContext.Meteostations.FindAsync(id);
            if (meteostation!= null) { _meteostationsContext.Remove(meteostation); }
            await _meteostationsContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPost]
        public async Task<ActionResult<List<Meteostation>>> AddSensor(Meteostation meteostation)
        {
            _meteostationsContext.Add(meteostation);
            await _meteostationsContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut(template: "{id}, {sensor}")]
        public async Task<ActionResult<List<Meteostation>>> UpdateSensor(int id, Meteostation meteostation)
        {
            Meteostation need_meteostation = await _meteostationsContext.Meteostations.FindAsync(id);
            if (need_meteostation != null)
            {
                need_meteostation.StationId = meteostation.StationId;
                need_meteostation.StationName = meteostation.StationName;
                need_meteostation.StationLatitude = meteostation.StationLatitude;
                need_meteostation.StationLongitude = meteostation.StationLongitude;
                await _meteostationsContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
