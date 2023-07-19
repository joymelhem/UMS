namespace DomainLibrary.Entities;

public partial class SessionTime
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public long Id { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<TeacherPerCoursePerSessionTime> TeacherPerCoursePerSessionTimes { get; set; } = new List<TeacherPerCoursePerSessionTime>();

    public SessionTime(DateTime startTime, DateTime endTime, int duration)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
    }
}
