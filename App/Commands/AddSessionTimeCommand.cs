using MediatR;
using NpgsqlTypes;

namespace App.Commands;

public class AddSessionTimeCommand : IRequest<bool>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int duration { get; set; }
}