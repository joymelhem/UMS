using DomainLibrary.Entities;
using MediatR;

namespace App.Queries;

public class GetAllCoursesQuery : IRequest<List<Course>>
{
    
}