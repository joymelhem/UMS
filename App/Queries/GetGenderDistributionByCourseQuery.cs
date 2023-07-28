using DomainLibrary.Entities;
using MediatR;

namespace App.Queries;

public class GetGenderDistributionByCourseQuery : IRequest<List<GenderDistributionDto>>
{
}