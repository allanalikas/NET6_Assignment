namespace helmes_task3.Data;

public class Alarm
{
    public Alarm(TimeSpan newAlarmTime)
    {
        this.ID = new Guid();
        this.time = newAlarmTime;
    }

    public Guid ID { get; set; }
    public TimeSpan time { get; set; }
}
