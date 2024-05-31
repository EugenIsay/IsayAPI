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
        private readonly MTContext _mtContext;
        public MTController(MTContext context) 
        {
            _mtContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Measurement_Type>>> GetMesType()
        {
            return await _mtContext.Measurements_Type.Select(Measurement_Type =>  Measurement_Type).ToListAsync();
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Measurement_Type>> GetMesTypeItem(int id)
        {
            return await _mtContext.Measurements_Type.FindAsync(id) ?? throw new InvalidOperationException();
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<List<Measurement_Type>>> DeleteMesType(int id)
        {
            Measurement_Type? mt = await _mtContext.Measurements_Type.FindAsync(id);
            if (mt != null) { _mtContext.Remove(mt); }
            await _mtContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPost]
        public async Task<ActionResult<List<Measurement_Type>>> AddMesType(Measurement_Type mt)
        {
            _mtContext.Add(mt);
            await _mtContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut(template: "{id}, {sensor}")]
        public async Task<ActionResult<List<Measurement_Type>>> UpdateMesType(int id, Measurement_Type mt)
        {
            Measurement_Type need_mt = await _mtContext.Measurements_Type.FindAsync(id);
            if (need_mt != null)
            {
                need_mt.type_id = mt.type_id;
                need_mt.type_name = mt.type_name;
                need_mt.type_units = mt.type_units;
                await _mtContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
