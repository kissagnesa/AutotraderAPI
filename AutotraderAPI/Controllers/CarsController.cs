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
        public ActionResult GetCars()
        {
            List<Car> response = new List<Car>();
            using (var context = new AutotraderContext())
            {
                try
                {
                    response = context.Cars.ToList();
                    return StatusCode(200, new { result = response, message = "Sikeres lekérdezés." });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        public ActionResult PutCarById(Guid id, string brand, string type, string color, DateTime myear)
        {
            using (var context = new AutotraderContext())
            {
                try
                {
                    Car car = new Car { };
                    car.Id = id;
                    car.Brand = brand;
                    car.Type = type;
                    car.Color = color;
                    car.Myear = myear;
                    context.Cars.Update(car);
                    context.SaveChanges();
                    return StatusCode(200, new { result = car, message = "Sikeres módosítás." });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete]
        public ActionResult DeleteCar(Guid id)
        {
            using (var context = new AutotraderContext())
            {
                try
                {
                    Car car = new Car { Id = id };
                    context.Cars.Remove(car);
                    context.SaveChanges();
                    return StatusCode(200, new { result = car, message = "Sikeres törlés." });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
