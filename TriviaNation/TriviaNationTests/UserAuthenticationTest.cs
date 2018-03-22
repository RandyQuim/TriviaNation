﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TriviaNation
{
    [TestClass]
    public class UserAuthenticationTest
    {
        [TestMethod]
        public void IfEmailAndPasswordsMatchMethodShouldReturnTrue()
        {
            // Arrange
            IUser user = new User();
            Mock<IDataBaseTable> mockDatabase = new Mock<IDataBaseTable>();
            mockDatabase.Setup(r => r.RetrieveNumberOfRowsInTable()).Returns(1);
            mockDatabase.Setup(r => r.TableName).Returns("Table Name");
            mockDatabase.Setup(r => r.RetrieveTableRow("Table Name", 1)).Returns("Randy\nfrt@uwf.edu\npassword\nscore");
            IUserAuthentication sut = new UserAuthentication(mockDatabase.Object, user);

            // Act
            Boolean test = sut.AuthenticateUser("frt@uwf.edu", "password");

            // Assert 
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void AquiringAUserNameShouldReturnUserName()
        {
            // Arrange
            IDataBaseTable database = new UserTable();
            Mock<IUser> mockAnswer = new Mock<IUser>();
            mockAnswer.Setup(r => r.UserName).Returns("Max Powers");
            IUserAuthentication sut = new UserAuthentication(database, mockAnswer.Object);

            // Act
            string test = sut.GetUserName();

            // Assert
            Assert.AreEqual("Max Powers", test);
        }

    }
}
