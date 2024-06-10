using IsayAPI.Models;
using IsayAPI.Models.Request;
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
        public async Task<ActionResult<List<MeteostationResponse>>> GetMeteostations()
        {

            var meteostations = await _meteostationsContext.Meteostations
                .Select(m => new MeteostationResponse {StationId = m.StationId, StationName = m.StationName, StationLatitude = m.StationLatitude, StationLongitude = m.StationLongitude }).ToListAsync();
            foreach (var meteostation in meteostations)
            {
                meteostation.meteostationsSensors = await _meteostationsContext.MeteostationsSensors.Where(mes => mes.StationId == meteostation.StationId).Join(
                    _meteostationsContext.Sensors,
                    m => m.SensorId,
                    s => s.SensorId,
                    (m, s) => new MSResponseForMet
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
                (m, s) => new MSResponseForMet
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
        public async Task<ActionResult<List<Meteostation>>> DeleteMeteostation(int id)
        {
            Meteostation? meteostation = await _meteostationsContext.Meteostations.FindAsync(id);
            List<int> sync = _meteostationsContext.MeteostationsSensors.Where(e => e.StationId == id).Select(s => s.SensorInventoryNumber).ToList();
            bool Go = true;
            foreach (var invnum in sync)
            {
                if (_meteostationsContext.Measurements
                        .Where(e => e.SensorInventoryNumber.ToString().Contains(invnum.ToString())).Count() != 0)
                {
                    Go = false;
                }
            }

            if (Go && meteostation != null)
            {
                _meteostationsContext.Meteostations.Remove(meteostation);
                foreach (var invnum in sync)
                {
                    _meteostationsContext.MeteostationsSensors.Remove(
                        _meteostationsContext.MeteostationsSensors.FirstOrDefault(
                            e => e.SensorInventoryNumber == invnum));
                }
            }
            await _meteostationsContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPost]
        public async Task<ActionResult<List<Meteostation>>> AddMeteostation(MeteostationRequest meteostation)
        { 
            _meteostationsContext.Meteostations.Add(new Meteostation(){ StationName = meteostation.StationName, StationLatitude = meteostation.StationLatitude, StationLongitude = meteostation.StationLongitude} );
            await _meteostationsContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut(template: "{id}, {sensor}")]
        public async Task<ActionResult<List<Meteostation>>> UpdateMeteostation(int id, Meteostation meteostation)
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
