﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriviaNation.Models;
using TriviaNation.Models.Abstract;

/**
TriviaNation is a networked trivia game designed for use in 
classrooms. Class members are each in control of a nation on 
a map. The goal of the game is to increase the size of the nation 
by winning trivia challenges and defeating other class members 
in contested territories. The focus is on gamifying learning and 
making it an enjoyable experience.


@author Timothy McWatters
@author Keenal Shah
@author Randy Quimby
@author Wesley Easton
@author Wenwen Xu

@version 1.0

CEN3032    "TriviaNation" SEII- Group 1's class project
File Name: Program.cs 

    This is the main class and the classes, interfaces are tested from here. 
*/

namespace TriviaNation
{
    class Program
    {
        static void Main(string[] args)
        {
            //connect to the DB
            new DataBaseOperations();
            DataBaseOperations.ConnectToDB();

            QuestionPackTable questionPackTable = new QuestionPackTable();
            questionPackTable.CreateTable(questionPackTable.TableName, questionPackTable.TableCreationString);
            

            //set up selection, creation or deletion of QuestionPacks
            ITriviaAdministration triviaAdmin = new TriviaAdministration();

            //Create 2 QuestionPacks
            IQuestionPack qp1 = triviaAdmin.AddQuestionPack("questionPack1", 5);
            IQuestionPack qp2 = triviaAdmin.AddQuestionPack("questionPack2", 10);
            

            //populate questionPacks
            qp1.AddQuestion("Is this qp1, q1?", "yes~no~maybe~blue~yes", "MC");
            qp1.AddQuestion("Is this qp1, q2?", "no~yes~maybe~blue~yes", "MC");
            qp1.AddQuestion("Is this qp1, q3?", "maybe~no~yes~blue~yes", "MC");
            qp1.AddQuestion("Is this qp1, q4?", "blue~no~maybe~yes~yes", "MC");
            qp1.AddQuestion("Is this qp1, q5?", "5~4~3~6~5", "MC");
            qp1.AddQuestion("Is this qp1, q6?", "q4~q6~maybe~blue~q6", "MC");
            qp1.AddQuestion("Is this qp1, q7?", "yes~no~maybe~blue~yes", "MC");
            qp1.AddQuestion("Is this qp1, q8?", "YES~NO~MAYBE~BLUE~YES", "MC");
            qp1.AddQuestion("Is this qp1, q9?", "red~purple~fox~yes~yes", "MC");
            qp1.AddQuestion("Is this qp1, q10?", "Yes~No~Maybe~Blue~Yes", "MC");

            qp2.AddQuestion("Is this qp2, q1?", "yes~no~maybe~blue~yes", "MC");
            qp2.AddQuestion("Is this qp2, q2?", "no~yes~maybe~blue~yes", "MC");
            qp2.AddQuestion("Is this qp2, q3?", "maybe~no~yes~blue~yes", "MC");
            qp2.AddQuestion("Is this qp2, q4?", "blue~no~maybe~yes~yes", "MC");
            qp2.AddQuestion("Is this qp2, q5?", "5~4~3~6~5", "MC");
            qp2.AddQuestion("Is this qp2, q6?", "q4~q6~maybe~blue~q6", "MC");
            qp2.AddQuestion("Is this qp2, q7?", "yes~no~maybe~blue~yes", "MC");
            qp2.AddQuestion("Is this qp2, q8?", "YES~NO~MAYBE~BLUE~YES", "MC");
            qp2.AddQuestion("Is this qp2, q9?", "red~purple~fox~yes~yes", "MC");

            //list all QuestionPacks
            IEnumerable<IQuestionPack> qpList = triviaAdmin.ListQuestionPacks();
            foreach (IQuestionPack qp in qpList)
            {
                Console.WriteLine(qp.QuestionPackName);
            }

            for (int i = 0; i < qp1.QuestionPackQuestions.Count; i++)
            {
                Console.WriteLine(qp1.QuestionPackQuestions[i].Question);
            }

            for (int i = 0; i < qp2.QuestionPackQuestions.Count; i++)
            {
                Console.WriteLine(qp2.QuestionPackQuestions[i].Question);
            }

            Console.WriteLine("Press any key to end the program");
            Console.ReadKey();
        }
    }
}
