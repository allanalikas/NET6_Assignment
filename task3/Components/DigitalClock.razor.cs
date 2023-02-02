using System.Threading;
using helmes_task3.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace helmes_task3.Components
{
    public partial class DigitalClock
    {
        string currentTime = DateTime.Now.ToString("HH:mm tt");
        List<Alarm> alarms = new List<Alarm>();
        List<Alarm> passedAlarms = new List<Alarm>();
        private TimeSpan? newAlarmTime = new TimeSpan(00, 00, 00);
        Timer? timer;

        [Parameter]
        public EventCallback<string> OnAlarmNotification { get; set; }

        protected override void OnInitialized()
        {
            timer = new Timer(Tick, null, 0, 1000);        
        }

        private void Tick(object _)
        {
            var currentDateTime = DateTime.Now;
            foreach(Alarm alarm in alarms) {
                if(currentDateTime.TimeOfDay.ToString(@"hh\:mm\:ss").CompareTo(alarm.time.ToString(@"hh\:mm\:ss")) == 0) {
                    passedAlarms.Add(alarm);
                }
            }
            currentTime = currentDateTime.ToString("HH:mm:ss tt");
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private void AddAlarm()
        {
            if(newAlarmTime != null) {
                alarms.Add(new Alarm((TimeSpan)newAlarmTime));
            }
        }

        private void ClearAlarms()
        {
            alarms.Clear();
        }

        private void RemoveAlarm(Alarm alarm) {
            alarms.Remove(alarm);
        }

        private void RemoveAlert(Alarm alarm) {
            this.passedAlarms.Remove(alarm);
            this.alarms.Remove(alarm);
        }
    }
}