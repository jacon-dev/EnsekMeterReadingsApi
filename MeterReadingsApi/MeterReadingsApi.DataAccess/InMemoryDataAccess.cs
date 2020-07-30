using MeterReadingsApi.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeterReadingsApi.DataAccess
{
    public class InMemoryDataAccess : IDataAccess
    {
        private readonly List<Account> accounts;
        private readonly List<MeterReading> meterReadings;
        private readonly string meterReadingFileLocation = @".\Meter_Reading.csv";
        private readonly string testAccountFileLocation = @".\Test_Accounts.csv";

        public InMemoryDataAccess()
        {
            accounts = new List<Account>();
            meterReadings = new List<MeterReading>();
            ReadTestAccountsCsv();
            ReadMeterReadingCsv();
        }

        private void ReadMeterReadingCsv()
        {
            using (StreamReader sr = new StreamReader(meterReadingFileLocation))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(",");
                    var t = ReadingIsValid(columns[2]);

                    if(t.Item1 && int.TryParse(columns[0], out int accountRef) && DateTime.TryParse(columns[1], out DateTime dateTime))
                    {
                        var reading = new MeterReading { AccountId = accountRef, Reading = t.Item2, ReadingDateTime = dateTime };
                        AddMeterReading(reading);
                    }
                }
            }
        }

        private void ReadTestAccountsCsv()
        {
            using(StreamReader sr = new StreamReader(testAccountFileLocation))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(",");
                    if(int.TryParse(columns[0], out int accountRef))
                    {
                        var account = new Account { AccountId = accountRef, FirstName = columns[1], Surname = columns[2] };
                        AddAccountReference(account);
                    }
                }
            }
        }

        public bool AccountReferenceExists(int accountReference)
        {
            if (accounts != null && accounts.Any()) return accounts.SingleOrDefault(a => a.AccountId == accountReference) != null;
            return false;
        }

        public void AddAccountReference(Account account)
        {
            if (!AccountReferenceExists(account.AccountId)) accounts.Add(account);
        }

        public void AddMeterReading(MeterReading meterReading)
        {
            if (!ReadingExists(meterReading) && AccountReferenceExists(meterReading.AccountId)) meterReadings.Add(meterReading);
        }

        public IEnumerable<MeterReading> GetMeterReadings()
        {
            return meterReadings.ToList();
        }

        public bool ReadingExists(MeterReading meterReading)
        {
            var r = from read in meterReadings
                    where read.AccountId == meterReading.AccountId && read.Reading == meterReading.Reading && read.ReadingDateTime == meterReading.ReadingDateTime
                    select read;

            if (r.Any()) return true;
            return false;
        }

        public Tuple<bool, int> ReadingIsValid(string reading)
        {
            var valid = int.TryParse(reading, out int validReading);

            if (!valid) return Tuple.Create(valid, 0);

            if (validReading < 0 || validReading.ToString().Length > 5) return Tuple.Create(false, 0);

            return Tuple.Create(valid, validReading);


        }
    }
}
