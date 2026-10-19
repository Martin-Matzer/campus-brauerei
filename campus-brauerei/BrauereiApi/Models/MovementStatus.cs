namespace BrauereiApi.Models;

public enum MovementStatus
{
    Success,
    NotFound,
    InsufficientStock,
    ExceedsMaximum,
    BadRequest
}
