namespace Proy.Domain.Dtos.StrategicAxis;

public record StrategicActionRequestDto(
    int Id,
    string Name,
    int StrategicLineId
    );