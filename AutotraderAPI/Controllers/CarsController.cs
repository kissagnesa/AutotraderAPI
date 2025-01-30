using AutotraderAPI.Models;
using AutotraderAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutotraderAPI.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewCar(CreateCarDto createCarDto)
        {
            var car = new Car 
            { 
                Id=Guid.NewGuid(),
                Brand = createCarDto.Brand,
                Type = createCarDto.Type,
                Color = createCarDto.Color,
                Myear = createCarDto.Myear,
            };

            using (var context=new AutotraderContext())
            {
                context.Cars.Add(car);
                context.SaveChanges();

                return StatusCode(201, new {result=car, message="Sikeres felvétel."});
            }
                
        }
    }
}
