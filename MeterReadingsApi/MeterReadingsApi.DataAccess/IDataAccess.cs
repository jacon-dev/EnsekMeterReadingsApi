using MeterReadingsApi.Core;
using System;
using System.Collections.Generic;

namespace MeterReadingsApi.DataAccess
{
    public interface IDataAccess
    {
        IEnumerable<MeterReading> GetMeterReadings();

        void AddMeterReading(MeterReading meterReading);

        bool ReadingExists(MeterReading meterReading);

        bool AccountReferenceExists(int accountReference);

        void AddAccountReference(Account account);

        Tuple<bool, int> ReadingIsValid(string reading);
    }
}
