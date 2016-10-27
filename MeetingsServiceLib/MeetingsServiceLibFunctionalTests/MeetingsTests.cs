using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MeetingsServiceLib;

namespace MeetingsServiceLibFunctionalTests
{
    [TestClass]
    public class MeetingsTests
    {
        MeetingData meetingDataA;
        MeetingData meetingDataB;
        MeetingData meetingDataC;
        MeetingData meetingDataD;

        private MeetingsServiceLib.Service service;
        private MeetingsServiceLib.IMeetingsRepository localMeetingsRepository;

        [TestInitialize]
        public void Initialize()
        {
            meetingDataA = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            meetingDataB = new MeetingData("BB", "BB", "2005-04-05 22:12", 10);
            meetingDataC = new MeetingData("CC", "CC", "2005-05-05 22:12", 10);
            meetingDataD = new MeetingData("DD", "DD", "2005-06-05 22:12", 10);

            localMeetingsRepository = new MeetingsServiceLib.LocalMeetingsRepository();
            service = new MeetingsServiceLib.Service(localMeetingsRepository, null);
        }

        [TestMethod]
        public void AddMeetingWithCorrectData()
        {
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            var explectedListOfMeeting = new List<Meeting> { meetingDataA.convertToMeeting()};
            CollectionAssert.AreEqual(explectedListOfMeeting, service.getAllMeetingsList());
        }

        [TestMethod]
        public void AddManyMeetingWithCorrectData()
        {
            var explectedListOfMeeting = new List<Meeting>();

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            explectedListOfMeeting.Add(meetingDataA.convertToMeeting());

            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            explectedListOfMeeting.Add(meetingDataB.convertToMeeting());

            CollectionAssert.AreEqual(explectedListOfMeeting, service.getAllMeetingsList());
        }

        [TestMethod]
        public void AddManyTimesTheSameMeeting()
        {
            var explectedListOfMeeting = new List<Meeting>();

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            explectedListOfMeeting.Add(meetingDataA.convertToMeeting());

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);

            CollectionAssert.AreEqual(explectedListOfMeeting, service.getAllMeetingsList());
        }


        [TestMethod]
        public void AddMeetingWitOneNullData()
        {
            meetingDataA.Location = null;
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.AreEqual(0, service.getAllMeetingsList().Count);
        }

        [TestMethod]
        public void AddMeetingDateInWrongFormat()
        {
            meetingDataA.Date = "2005-05 22:12";
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.AreEqual(0, service.getAllMeetingsList().Count);
        }

        [TestMethod]
        public void AddMeetingDateIsNull()
        {
            meetingDataA.Date = null;
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.AreEqual(0, service.getAllMeetingsList().Count);
        }

        [TestMethod]
        public void GetMeetingByName()
        {
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);

            Assert.AreEqual(meetingDataA.convertToMeeting(), service.getMeetingsByName(meetingDataA.Name));
        }

        [TestMethod]
        public void GetMeetingByNameWhenThereIsNotMeetingWithGivenName()
        {
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);

            Assert.AreEqual(null, service.getMeetingsByName("ZZ"));
        }

        [TestMethod]
        public void GetAllMeetingBetweenGivenDate()
        {
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);

            DateTime startDate = meetingDataA.convertToMeeting().Time.AddDays(2);
            DateTime endDate = meetingDataD.convertToMeeting().Time.AddDays(-2);

            var expectedListOfMeeting = new List<Meeting>
            {
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
            };

            CollectionAssert.AreEqual(expectedListOfMeeting, service.getMeetingsByDate(startDate, endDate));
        }

        [TestMethod]
        public void GetAllMeetingBeforGivenDate()
        {
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);

            DateTime endDate = meetingDataD.convertToMeeting().Time.AddDays(-2);

            var expectedListOfMeeting = new List<Meeting>
            {
                meetingDataA.convertToMeeting(),
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
            };

            CollectionAssert.AreEqual(expectedListOfMeeting, service.getMeetingsByDate(endDate : endDate));
        }

        [TestMethod]
        public void GetAllMeetingAfterGivenDate()
        {
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);

            DateTime startDate = meetingDataA.convertToMeeting().Time.AddDays(2);

            var expectedListOfMeeting = new List<Meeting>
            {
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
                meetingDataD.convertToMeeting()
            };

            CollectionAssert.AreEqual(expectedListOfMeeting, service.getMeetingsByDate(startDate : startDate));
        }
    }
}
