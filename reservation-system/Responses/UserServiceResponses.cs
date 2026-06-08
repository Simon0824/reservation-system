namespace reservation_system.Responses;
public class UserServiceResponses
{
    public bool Succeeded { get; init; }
    public string? Token { get; init; }
    public string? Error { get; init; }
}