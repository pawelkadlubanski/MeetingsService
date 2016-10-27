using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingsServiceLib;

namespace MeetingsServiceLibFunctionalTests
{
    [TestClass]
    public class UsersTests
    {
        private Service service;
        private IUserRepository localUserRepository;

        [TestInitialize]
        public void Initialize()
        {
            localUserRepository = new LocalUserRepository();
            service = new Service(null, localUserRepository);
        }

        [TestMethod]
        public void GetListOfUsersWhenRepositoryIsEmpty()
        {
           Assert.AreEqual(0, service.getAllUsers().Count);
        }

        [TestMethod]
        public void AddUserWithCorrectData()
        {
            var givenUserData = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserData.Name, givenUserData.Surname, givenUserData.Login, givenUserData.Email);
            Assert.AreEqual(givenUserData.convertToUser(), service.getUserByLogin(givenUserData.Login));
        }

        [TestMethod]
        public void AddUserWithLoginThatExists()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);

            var givenUserDataB = new UserData("Adam", "Mrozek", "jankow", "adam.mrozek@gmail.com");
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<User> { givenUserDataA.convertToUser() }; 
            CollectionAssert.AreEqual(expectedUserList, service.getAllUsers());
        }

        [TestMethod]
        public void AddUserWithEmailThatExists()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);

            var givenUserDataB = new UserData("Adam", "Mrozek", "Mrozek", "jan.kowalski@gmail.com");
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<User> { givenUserDataA.convertToUser() };
            CollectionAssert.AreEqual(expectedUserList, service.getAllUsers());
        }

        [TestMethod]
        public void AddUserWithEmailThatExistsButEmailIsGivenWithUppercase()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);

            var givenUserDataB = new UserData("Adam", "Mrozek", "Mrozek", "Jan.Kowalski@gmail.com");
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<User> { givenUserDataA.convertToUser() };
            CollectionAssert.AreEqual(expectedUserList, service.getAllUsers());
        }

        [TestMethod]
        public void AddTwoUserWithCorrectData()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);

            var givenUserDataB = new UserData("Adam", "Mrozek", "Mrozek", "mrozek@gmail.com");
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<User> { givenUserDataA.convertToUser(), givenUserDataB.convertToUser() };
            CollectionAssert.AreEqual(expectedUserList, service.getAllUsers());
        }

        [TestMethod]
        public void RemoveUserByLogin()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var givenUserDataB = new UserData("Adam", "Mrozek", "Mrozek", "mrozek@gmail.com");

            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            service.removeUser(givenUserDataA.Name);

            var expectedUserList = new List<User> { givenUserDataB.convertToUser() };
            CollectionAssert.AreEqual(expectedUserList, service.getAllUsers());
        }

        [TestMethod]
        public void RemoveNotExistingUser()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);

            service.removeUser("user_login");

            var expectedUserList = new List<User> { givenUserDataA.convertToUser() };
            CollectionAssert.AreEqual(expectedUserList, service.getAllUsers());
        }
    }
}
