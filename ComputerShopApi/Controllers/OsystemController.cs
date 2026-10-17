using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("Osystems")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        public ComputershopContext context = new ComputershopContext();

        [HttpPost("newOsystem")]
        public object AddNewOsystem([FromBody]AddNewOsystemDto addNewOsystemDto)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = addNewOsystemDto.Name,
                Version = addNewOsystemDto.Version,
                RegisterTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            if (osystem != null)
            {
                context.Osystems.Add(osystem);
                context.SaveChanges();

                return StatusCode(201, new { message = "Sikeres felvétel", result = osystem});

            }
            
            return StatusCode(400, new { message = "Sikertelen felvétel", result = osystem });
        }

        [HttpGet("allOsystem")]
        public object AllOsystem()
        {
            var osystems = context.Osystems.ToList();

            if(osystems.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystems });
            }

            return StatusCode(400, new { message = "Üres tábla", result = osystems });
        }

        [HttpGet("osystemById")]
        public object OsystemById([FromQuery] Guid id)
        {
            var osystem = context.Osystems.SingleOrDefault(o => o.Id == id);

            if (osystem != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystem });
            }

            return StatusCode(400, new { message = "Nincs ilyen id-vel osystem", result = osystem });
        }
    }
}
