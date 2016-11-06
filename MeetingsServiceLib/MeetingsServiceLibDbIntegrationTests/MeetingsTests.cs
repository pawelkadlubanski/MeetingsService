using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingsServiceLib;
using System.Collections.Generic;

namespace MeetingsServiceLibDbIntegrationTests
{
    [TestClass]
    public class MeetingTests
    {
        MeetingData meetingDataA;
        MeetingData meetingDataB;
        MeetingData meetingDataC;
        MeetingData meetingDataD;

        private Service service;
        private IMeetingsRepository meetingsRepository;

        [TestInitialize]
        public void Initialize()
        {
            meetingDataA = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            meetingDataB = new MeetingData("BB", "BB", "2005-04-05 22:12", 10);
            meetingDataC = new MeetingData("CC", "CC", "2005-05-05 22:12", 10);
            meetingDataD = new MeetingData("DD", "DD", "2005-06-05 22:12", 10);

            meetingsRepository = new DbMeetingRepository();
            service = new MeetingsServiceLib.Service(meetingsRepository, null, null);
        }

        [TestMethod]
        public void AddMeeting()
        {
            var explectedListOfMeeting = new List<Meeting>();

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            explectedListOfMeeting.Add(meetingDataA.convertToMeeting());

            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            explectedListOfMeeting.Add(meetingDataB.convertToMeeting());

            CollectionAssert.AreEqual(explectedListOfMeeting, service.getAllMeetingsList());
        }
    }
}
