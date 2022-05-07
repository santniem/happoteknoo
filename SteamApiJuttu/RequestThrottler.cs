using System;
using System.Collections.Generic;
using System.Threading;

namespace SteamApiJuttu
{
    public class RequestRateHelper
    {
        private readonly Queue<DateTime> _history;
        private readonly TimeSpan _interval;
        private readonly int _requestsPerInterval;

        // public RequestRateHelper()
        //     : this(30, new TimeSpan(0, 1, 0)) { }

        public RequestRateHelper(int requestsPerInterval, TimeSpan interval)
        {
            _requestsPerInterval = requestsPerInterval;
            _history = new Queue<DateTime>();
            _interval = interval;
            TimesSlept = 0;
            TimeSlept = new TimeSpan(0);
        }

        // For debugging purposes
        public int TimesSlept { get; set; }
        public TimeSpan TimeSlept { get; set; }

        public void SleepAsNeeded()
        {
            var now = DateTime.Now;

            _history.Enqueue(now);

            if (_history.Count >= _requestsPerInterval)
            {
                var last = _history.Dequeue();
                var difference = now - last;

                if (difference >= _interval) return;
                TimesSlept += 1;

                var timeToSleep = _interval - difference;
                TimeSlept += timeToSleep;
                Thread.Sleep(timeToSleep);
            }
        }
    }
}