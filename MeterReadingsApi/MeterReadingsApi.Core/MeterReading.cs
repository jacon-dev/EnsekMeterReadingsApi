using System;

namespace MeterReadingsApi.Core
{
    public class MeterReading
    {
        public int AccountId { get; set; }

        public DateTime ReadingDateTime { get; set; }

        public int Reading { get; set; } 
    }
}
