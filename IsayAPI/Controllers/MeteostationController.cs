using IsayAPI.Models;
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
        public async Task<ActionResult<List<Meteostation>>> GeMeteostations()
        {
            return await _meteostationsContext.Meteostations.Select(meteostation =>  meteostation).ToListAsync();
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Meteostation>> GetMeteostationsItem(int id)
        {
            return await _meteostationsContext.Meteostations.FindAsync(id) ?? throw new InvalidOperationException();
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
