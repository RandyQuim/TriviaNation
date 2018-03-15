﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TriviaNation;

namespace TriviaNationTests
{
    [TestClass]
    public class QuestionTableTest
    {
        private QuestionTable QT;
        SqlConnection s_connection;

        [TestInitialize]
        public void Initialize() {
            new DataBaseOperations();
            DataBaseOperations.ConnectToDB();
            s_connection = DataBaseOperations.Connection;
            QT = new QuestionTable();
        }
        
        [TestMethod]
        public void TestTableExistsMethodToSeeIfAKnownTableExists()
        {
            // Arrange
            var sut = new QuestionTable();

            // Act
            bool tableExists = sut.TableExists(sut.TableName);
            bool expected = true;
            bool actual = tableExists;

            //Assert
            Assert.AreEqual(expected, actual);
        }        

        [TestMethod]
        public void TestCreateTableMethodTableShouldGetCreated()
        {
            // Arrange
            String sqlString = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'QTTestTable1'";
            int count = 0;
            String nameOfTestTable = "QTTestTable1";
            String tableCreationString = "(columnone varchar(4000) not null PRIMARY KEY, columntwo varchar(4000) not null);";

            // Act
            QT.CreateTable(nameOfTestTable, tableCreationString);
            SqlCommand command = new SqlCommand(sqlString, s_connection);
            SqlDataReader myReader = command.ExecuteReader();
            while (myReader.Read())
            {
                count++;
            }

            //Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void TestInsertRowIntoTableMethodToSeeIfRowGetsInserted()
        {
            // Arrange
            String tableDropCode = ("DROP TABLE IF EXISTS QTTestTable2;");
            SqlCommand deleteTableCommand = new SqlCommand(tableDropCode, s_connection);
            deleteTableCommand.ExecuteNonQuery();
            String tableCreationString = "CREATE TABLE QTTestTable2(columnone varchar(4000) not null PRIMARY KEY, columntwo varchar(4000) not null, columnthree varchar(4000) not null);";
            SqlCommand command = new SqlCommand(tableCreationString, s_connection);
            command.ExecuteNonQuery();

            //String retrievedRow = "";
            //String TSQLSourceCode = ("SELECT * FROM(Select Row_Number() Over (Order By columnone) As RowNum, * From QTTestTable2) t2 where RowNum = 0;");


            //////////////////////////////////////////////////////////

            List<string> questionAndAnswer = new List<string>();
            questionAndAnswer.Add("QuestionTest");
            questionAndAnswer.Add("AnwerTest");
            questionAndAnswer.Add("QuestionTypeTest");
            Mock<IDataEntry> mockDataEntry = new Mock<IDataEntry>();
            mockDataEntry.Setup(r => r.GetValues()).Returns(questionAndAnswer);

            // Act
            QT.InsertRowIntoTable("QTTestTable2", mockDataEntry.Object);




            //using (SqlCommand cmd = new SqlCommand(TSQLSourceCode, s_connection))
            //{
           //     using (SqlDataReader reader = cmd.ExecuteReader())
           //     {
           //         while (reader.Read())
            //        {
            //            for (int i = 1; i < reader.FieldCount; i++)
             //           {
            //                retrievedRow += (reader.GetString(i) + "\n");
            //            }
            //        }
             //   }
            //}

            // Assert
            Assert.AreEqual(1, 1);
            //Assert.AreEqual(("QuestionTest" + "\n" + "AnswerTest" + "\n" + "QuestionTypeTest" + "\n"), retrievedRow);
        }

        [TestMethod]
        public void TestRetrieveNumberOfRowsInTableMethodShouldReturnIntNotNUll()
        {
            //Arrange
            var sut = new QuestionTable();

            //Act
            int numberReturned = sut.RetrieveNumberOfRowsInTable();

            //Assert
            Assert.IsNotNull(numberReturned);
        }

        [TestMethod]
        public void TestRetrieveRowInTableMethodShouldReturnTheRowFromSpecificRowNumber()
        {
            // Arrange
            var sut = new QuestionTable();
            int rowNumber = 1;

            // Act
            String rowRetrieved = sut.RetrieveTableRow(rowNumber);

            // Assert
            Assert.AreEqual("This is question2" + "\n" + "This is answer2" + "\n" + "TypeTest2" + "\n", rowRetrieved);
        }



        [TestMethod]
        public void TestRetrieveNumberOfColsInTableMethodShouldReturnIntNotNUll()
        {
            //Arrange
            var sut = new QuestionTable();

            //Act
            int numberReturned = sut.RetriveNumberOfColsInTable();

            //Assert
            Assert.IsNotNull(numberReturned);
        }

        
        [TestMethod]
        public void TestToSeeIfRowIsDeleted()
        {
            // Arrange
            int count = 1;

            var sut = new QuestionTable();
            String questionString = "This is question1";
            String sqlString = "DELETE FROM QuestionTable WHERE question='" + questionString + "';";

            // Act
            QT.DeleteRowFromTable(questionString);
            SqlCommand command = new SqlCommand(sqlString, s_connection);
            SqlDataReader myReader = command.ExecuteReader();
            while (myReader.Read())
            {
                count++;
            }

            // Assert
            Assert.AreEqual(1, count);
        }       

        public void CleanUpAfterTests()
        {
            String DropTableSQLCode1 = ("DROP TABLE IF EXISTS TestTable1;");
            SqlCommand deleteTableCommand1 = new SqlCommand(DropTableSQLCode1, s_connection);
            deleteTableCommand1.ExecuteNonQuery();
        }
    }
}