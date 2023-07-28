using MediatR;

namespace App.Queries;

public class GetCommonStudentsQuery : IRequest<List<string>>
{
    public long TeacherId1 { get; set; }
    public long TeacherId2 { get; set; }

    public GetCommonStudentsQuery(long teacherId1, long teacherId2)
    {
        TeacherId1 = teacherId1;
        TeacherId2 = teacherId2;
    }
}