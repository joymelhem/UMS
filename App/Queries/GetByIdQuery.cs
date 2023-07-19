using DomainLibrary.Entities;
using MediatR;

namespace App.Queries;

public class GetByIdQuery: IRequest<Course>
{
    public long Id { get; }
    
    public GetByIdQuery(long id)
    {
        Id = id;
    }
}