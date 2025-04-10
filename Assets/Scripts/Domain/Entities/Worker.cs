using System;

public class Worker
{
    public float WorkDurationSeconds { get; private set; }
    public DateTime LastWorkedAt { get; private set; }

    public Worker() { }
    public Worker(float workDurationSeconds)
    {
        WorkDurationSeconds = workDurationSeconds;
        LastWorkedAt = DateTime.MinValue;
    }

    public Worker(float workDurationSeconds, DateTime lastWorkedAt) : this(workDurationSeconds)
    {
        LastWorkedAt = lastWorkedAt;
    }

    public bool CanWork(DateTime now)
    {
        return (now - LastWorkedAt).TotalSeconds >= WorkDurationSeconds;
    }

    public void MarkWorked(DateTime now)
    {
        LastWorkedAt = now;
    }
}
