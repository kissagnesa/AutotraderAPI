using AutotraderAPI.Models.DTOs;
using AutotraderApi.Models;
using AutotraderBackend.Models;
using AutotraderBackend.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutotraderBackend.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly AutotraderContext _context;

        public CarsController(AutotraderContext context)
        {
            _context = context;
        }

        [HttpDelete("carbyid")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            // using (var context = new AutotraderContext())
            // {
            try
            {
                Car car = new Car { Id = id };
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { result = car, message = "Sikeres törlés" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //}
        }


        [HttpPost]
        public async Task<ActionResult> AddNewCar(CreateCarDto createCarDto)
        {

            var car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = createCarDto.Brand,
                Type = createCarDto.Type,
                Color = createCarDto.Color,
                Myear = createCarDto.Myear
            };

            //using (var context = new AutotraderContext())
            //{
            try
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();
                return StatusCode(201, new { result = car, message = "Sikeres felvétel" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //}
        }

        [HttpPut("carbyid")]
        public async Task<ActionResult> UpdateCarById(Guid id, UpdateCarDto updateCarDto)
        {
            //using (var context = new AutotraderContext())
            //{
            try
            {
                Car car = new Car { Id = id };
                car = _context.Cars.FirstOrDefault(x => x.Id == id);
                car.Brand = updateCarDto.Brand;
                car.Type = updateCarDto.Type;
                car.Color = updateCarDto.Color;
                car.Myear = updateCarDto.Myear;
                car.UpdatedTime = DateTime.Now;
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { result = car, message = "Sikeres módosítás" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //}
        }

        [HttpGet]
        public ActionResult GetCars()
        {
            List<Car> response = new List<Car>();
            //using (var context = new AutotraderContext())
            //{
            try
            {
                response = _context.Cars.ToList();
                return StatusCode(200, new { result = response, message = "Sikeres lekérdezés" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //}
        }

        [HttpGet("carbyid")]
        public ActionResult GetCarById(Guid id)
        {
            //using (var context = new AutotraderContext())
            //{
            try
            {
                Car car = new Car { Id = id };
                car = _context.Cars.FirstOrDefault(x => x.Id == id);
                return StatusCode(200, new { result = car, message = "Sikeres lekérdezés" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //}
        }
    }
}