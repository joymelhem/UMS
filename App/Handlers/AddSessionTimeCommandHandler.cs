using App.Commands;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces;
using MediatR;

namespace App.Handlers;

public class AddSessionTimeCommandHandler : IRequestHandler<AddSessionTimeCommand, bool>
{
    private readonly ISessionTimeRepository _sessionTimeRepository;

    public AddSessionTimeCommandHandler(ISessionTimeRepository sessionTimeRepository)
    {
        _sessionTimeRepository = sessionTimeRepository;
    }

    public async Task<bool> Handle(AddSessionTimeCommand request, CancellationToken cancellationToken)
    {
        var course = new SessionTime(request.StartTime, request.EndTime, request.duration);
        _sessionTimeRepository.Add(course);
        await _sessionTimeRepository.SaveChangesAsync();
        return true; 
    }
}