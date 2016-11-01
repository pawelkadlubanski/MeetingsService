using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingsServiceLib;

namespace MeetingsServiceLibFunctionalTests
{
    [TestClass]
    public class ParticipationTest
    {
        private Service service;
        private IMeetingsRepository localMeetingsRepository;
        private IUserRepository localUserRepository;
        private IParticipationRepository localParticipationRepository;

        [TestInitialize]
        public void Initialize()
        {
            localMeetingsRepository = new LocalMeetingsRepository();
            localUserRepository = new LocalUserRepository();
            localParticipationRepository = new LocalParticipationRepository();

            service = new Service(localMeetingsRepository, localUserRepository, localParticipationRepository);
        }

        [TestMethod]
        public void GetUsersMeetingWhenUserHasNoMeeting()
        {
            var userData = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");

            service.addUser(userData.Name, userData.Surname, userData.Login, userData.Email);

            var expectedMeetingList = new List<Meeting> { };

            CollectionAssert.AreEqual(expectedMeetingList, service.getUserMeetings(userData.Login));
        }


        [TestMethod]
        public void GetUsersMeeting()
        {
            var userData = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");

            var meetingDataA = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            var meetingDataB = new MeetingData("BB", "BB", "2005-04-05 22:12", 10);

            service.addUser(userData.Name, userData.Surname, userData.Login, userData.Email);
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);

            service.signUpUserToMeeting(userData.Login, meetingDataA.Name);
            service.signUpUserToMeeting(userData.Login, meetingDataB.Name);

            var expectedMeetingList = new List<Meeting>
            {
                meetingDataA.convertToMeeting(),
                meetingDataB.convertToMeeting()
            };

            CollectionAssert.AreEqual(expectedMeetingList, service.getUserMeetings(userData.Login));
        }

        [TestMethod]
        public void GetMeetingParticipantsWhenLackOfParticipant()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);

            var expectedUserList = new List<User> { };

            CollectionAssert.AreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name));
        }


        [TestMethod]
        public void GetMeetingParticipants()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            var userDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var userDataB = new UserData("Adam", "Mrozek", "mrozek", "adam.mrozek@gmail.com");

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);
            service.addUser(userDataA.Name, userDataA.Surname, userDataA.Login, userDataA.Email);
            service.addUser(userDataB.Name, userDataB.Surname, userDataB.Login, userDataB.Email);

            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);
            service.signUpUserToMeeting(userDataB.Login, meetingData.Name);

            var expectedUserList = new List<User>
            {
                userDataA.convertToUser(),
                userDataB.convertToUser()
            };

            CollectionAssert.AreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name));
        }

        [TestMethod]
        public void SignUserTwiceOnTheSameMeetingIsNotPosible()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            var userDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);
            service.addUser(userDataA.Name, userDataA.Surname, userDataA.Login, userDataA.Email);

            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);
            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);

            var expectedUserList = new List<User>{userDataA.convertToUser()};

            CollectionAssert.AreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name));
        }
        [TestMethod]
        public void SignUserToMeetoingWithoutFreePlacesIsNotPosible()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 1);
            var userDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var userDataB = new UserData("Adam", "Mrozek", "mrozek", "adam.mrozek@gmail.com");

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);
            service.addUser(userDataA.Name, userDataA.Surname, userDataA.Login, userDataA.Email);
            service.addUser(userDataB.Name, userDataB.Surname, userDataB.Login, userDataB.Email);

            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);
            service.signUpUserToMeeting(userDataB.Login, meetingData.Name);

            var expectedUserList = new List<User> { userDataA.convertToUser() };

            CollectionAssert.AreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name));
        }

    }
}
