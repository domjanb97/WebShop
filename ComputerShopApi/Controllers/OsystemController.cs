using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            
            return StatusCode(404, new { message = "Sikertelen felvétel", result = osystem });
        }

        [HttpGet("allOsystem")]
        public object AllOsystem()
        {
            var osystems = context.Osystems.ToList();

            if(osystems.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystems });
            }

            return StatusCode(404, new { message = "Üres tábla", result = osystems });
        }

        [HttpGet("osystemById")]
        public object OsystemById([FromQuery] Guid id)
        {
            var osystem = context.Osystems.SingleOrDefault(o => o.Id == id);

            if (osystem != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystem });
            }

            return StatusCode(404, new { message = "Nincs ilyen id-vel osystem", result = osystem });
        }

        [HttpPut("updateOsystem")]
        public object UpdateOsystem([FromQuery] Guid id, [FromBody]UpdateOsystemDto updateOsystemDto)
        {
            var osystem = context.Osystems.Find(id);

            if (osystem != null)
            {
                osystem.Name = updateOsystemDto.Name;
                osystem.Version = updateOsystemDto.Version;
                osystem.UpdateTime = DateTime.Now;

                context.Osystems.Update(osystem);
                context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = osystem });
            }

            return StatusCode(404, new { message = "Nincs ilyen id-vel osystem", result = osystem });
        }

        [HttpDelete("deleteOsystem")]
        public object DeleteOsystem([FromQuery]Guid id)
        {
            var osystem = context.Osystems.FirstOrDefault(o => o.Id == id);
            if (osystem != null)
            {
                context.Osystems.Remove(osystem);
                context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres törlés" });
            }

            return StatusCode(404, new { message = "Nincs ilyen id-vel osystem" });
        }

        //Több táblás lekérdezések
        //Lekérdezi az összes op. rendszert a hozzájuk tartozó számítógékeppel

        [HttpGet("osystemWithComputers")]
        public object OsystemWithComputers()
        {
            //var osystem = context.Osystems.Include(o => o.Computers).ToList();

            var osystems = context.Osystems.Select(o => new {o.Id,o.Name,o.Version, Computers = o.Computers.Select(c => new { c.Id,c.Brand,c.Type}) }).ToList();

            if(osystems.Count > 0)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = osystems });
            }

            return StatusCode(404, new { message = "Üres tábla", result = osystems });

            //return StatusCode(200, osystems);
        }

        [HttpGet("osystemComputerCount")]
        public object OsystemComputerCount()
        {
            var osystems = context.Osystems.Select(o => new {o.Name,o.Version,ComputerCount = o.Computers.Count()}).OrderByDescending(c => c.ComputerCount).ToList();

            if (osystems.Count > 0)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = osystems });
            }

            return StatusCode(404, new { message = "Üres tábla", result = osystems });
        }

        [HttpGet("osystemWithoutComputer")]
        public object OsystemWithoutComputer()
        {
            var osystems = context.Osystems.Where(o => !o.Computers.Any()).Select(o => new {o.Id,o.Name,o.Version,o.RegisterTime}).ToList();

            if (osystems.Count > 0)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = osystems });
            }

            return StatusCode(404, new { message = "Üres eredmény", result = osystems });
        }

        [HttpGet("osystemByBrand")]
        public object OsystemByBrand([FromQuery]string brand)
        {
            var osystems = context.Osystems.Where(o => o.Computers.Any(c => c.Brand == brand)).Select(o => new {o.Id,o.Name,o.Version,o.RegisterTime,BrandCount = o.Computers.Count(c => c.Brand == brand)}).ToList();

            if (osystems.Count > 0)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = osystems });
            }

            return StatusCode(404, new { message = "Üres eredmény", result = osystems });
        }

    }
}
