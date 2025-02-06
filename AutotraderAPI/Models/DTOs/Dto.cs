namespace AutotraderAPI.Models.DTOs
{
    public record CreateCarDto(string Brand, string Type, string Color, DateTime Myear);

    public record UpdateCarDto(string Brand, string Type, string Color, DateTime Myear);

}
