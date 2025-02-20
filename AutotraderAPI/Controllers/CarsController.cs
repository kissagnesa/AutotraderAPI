using AutotraderApi.Models;
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
                Id = Guid.NewGuid(),
                Brand = createCarDto.Brand,
                Type = createCarDto.Type,
                Color = createCarDto.Color,
                Myear = createCarDto.Myear,
            };

            using (var context = new AutotraderContext())
            {
                context.Cars.Add(car);
                context.SaveChanges();

                return StatusCode(201, new { result = car, message = "Sikeres felvétel." });
            }

        }

        [HttpGet]
        public ActionResult GetAllCar()
        {
            using (var context=new AutotraderContext())
            {
                var cars = context.Cars.ToList();

               if(cars!=null)
                {
                    return Ok(new { result = cars, message = "Sikeres lekérés." });
                }
                Exception e = new();
                return BadRequest(new {result="", message=e.Message});
            }
        }

        [HttpGet]

        public ActionResult GetCarById(Guid id)
        {
            using (var context=new AutotraderContext())
            {
                var car = context.Cars.FirstOrDefault(x=>x.Id==id);

                if(car!=null)
                {
                    return Ok(new { result = car, message = "Sikeres találat." });
                }
                return NotFound();
            }
            
        }


        [HttpPut]
        public ActionResult UpdateCar(Guid id, UpdateCarDto updateCarDto)
        {
            using (var context=new AutotraderContext())
            {
                var existingCar=context.Cars.FirstOrDefault(c=>c.Id==id);

                if(existingCar!=null)
                {
                    existingCar.Brand= updateCarDto.Brand;
                    existingCar.Type= updateCarDto.Type;
                    existingCar.Color= updateCarDto.Color;
                    existingCar.Myear= updateCarDto.Myear;
                    existingCar.UpdatedTime = DateTime.Now;

                    context.Cars.Update(existingCar);
                    context.SaveChanges();

                    if (existingCar != null)
                    {
                        return Ok(new { result = existingCar, message = "Sikeres módosítás." });
                    }
                }
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult DeleteCar(Guid id)
        {
            using (var context = new AutotraderContext())
            {
                var car = context.Cars.FirstOrDefault(x => x.Id==id);

                if(car!=null)
                {
                    context.Cars.Remove(car);
                    context.SaveChanges();
                    return Ok(new { result = car, message = "Sikeres törlés." });

                }
                return NotFound();

            }
        }
    }
}
