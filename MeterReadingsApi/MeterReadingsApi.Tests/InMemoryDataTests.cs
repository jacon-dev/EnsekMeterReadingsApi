using MeterReadingsApi.DataAccess;
using NUnit.Framework;

namespace MeterReadingsApi.Tests
{
    public class InMemoryDataTests
    {
        IDataAccess mockDataContext;

        [SetUp]
        public void Setup()
        {
            mockDataContext = new InMemoryDataAccess();
        }

        [Test]
        public void DoesAccountExistTest()
        {
            var accountExists = mockDataContext.AccountReferenceExists(2344);
            var accountDoesntExist = mockDataContext.AccountReferenceExists(1111);

            Assert.AreEqual(true, accountExists);
            Assert.AreEqual(false, accountDoesntExist);

            Assert.Pass();
        }

        [Test]
        public void AreReadingsValidTest()
        {
            var validReading1 = "01234";
            var validReading2 = "98765";
            var invalidReading1 = "999999";
            var invalidReading2 = "AB82Z";
            var invalidReading3 = "-12345";

            var validTuple1 = mockDataContext.ReadingIsValid(validReading1);
            var validTuple2 = mockDataContext.ReadingIsValid(validReading2);
            var invalidTuple1 = mockDataContext.ReadingIsValid(invalidReading1);
            var invalidTuple2 = mockDataContext.ReadingIsValid(invalidReading2);
            var invalidTuple3 = mockDataContext.ReadingIsValid(invalidReading3);

            Assert.AreEqual(true, validTuple1.Item1);
            Assert.AreEqual(1234, validTuple1.Item2);
            Assert.AreEqual(true, validTuple2.Item1);
            Assert.AreEqual(98765, validTuple2.Item2);

            Assert.AreEqual(false, invalidTuple1.Item1);
            Assert.AreEqual(0, invalidTuple1.Item2);
            Assert.AreEqual(false, invalidTuple2.Item1);
            Assert.AreEqual(0, invalidTuple2.Item2);
            Assert.AreEqual(false, invalidTuple3.Item1);
            Assert.AreEqual(0, invalidTuple3.Item2);
        }
    }
}