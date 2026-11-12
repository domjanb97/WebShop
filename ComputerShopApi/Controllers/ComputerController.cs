using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ComputerShopApi.Controllers
{
    [Route("computers")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public ComputershopContext context = new ComputershopContext();

        [HttpPost("newComputer")]
        public object AddNewComputer([FromBody] AddNewComputerDto addNewComputerDto)
        {
            var computer = new Computer
            {
                Id = Guid.NewGuid(),
                Brand = addNewComputerDto.Brand,
                Type = addNewComputerDto.Type,
                Display = addNewComputerDto.Display,
                Memory = addNewComputerDto.Memory,
                CreatedTime = DateTime.Now,
                OsId = addNewComputerDto.OsId
            };

            if (computer != null)
            {
                context.Computers.Add(computer);
                context.SaveChanges();

                return StatusCode(201, new { message = "Sikeres felvétel", result = computer });

            }

            return StatusCode(404, new { message = "Sikertelen felvétel", result = computer });
        }

        [HttpGet("allComputer")]
        public object AllComputer()
        {
            var computer = context.Computers.ToList();

            if (computer.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computer });
            }

            return StatusCode(404, new { message = "Üres tábla", result = computer });
        }

        [HttpGet("ComputerById")]
        public object ComputerById([FromQuery] Guid id)
        {
            var computer = context.Computers.SingleOrDefault(c => c.Id == id);

            if (computer != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computer });
            }

            return StatusCode(404, new { message = "Nincs ilyen id-vel computer", result = computer });
        }

        [HttpPut("updateOsystem")]
        public object UpdateComputer([FromQuery] Guid id, [FromBody] UpdateComputerDto updateComputerDto)
        {
            var computer = context.Computers.Find(id);

            if (computer != null)
            {
                computer.Brand = updateComputerDto.Brand;
                computer.Type = updateComputerDto.Type;
                computer.Display = updateComputerDto.Display;
                computer.Memory = updateComputerDto.Memory;
                computer.OsId = updateComputerDto.OsId;

                context.Computers.Update(computer);
                context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = computer });
            }

            return StatusCode(404, new { message = "Nincs ilyen id-vel computer", result = computer });
        }

        [HttpDelete("deleteComputer")]
        public object DeleteComputer([FromQuery] Guid id)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == id);
            if (computer != null)
            {
                context.Computers.Remove(computer);
                context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres törlés" });
            }

            return StatusCode(404, new { message = "Nincs ilyen id-vel computer" });
        }

        [HttpGet("computerWithOs")]
        public object ComputerWithOs()
        {
            var computers = context.Computers.Join(context.Osystems, c => c.OsId, o => o.Id, (c,o) => new {c.Id, c.Brand, c.Type, c.Display, c.Memory, o.Name,o.Version}).ToList();

            if (computers.Count > 0)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = computers });
            }

            return StatusCode(404, new { message = "Üres tábla", result = computers });
        }
    }
}
