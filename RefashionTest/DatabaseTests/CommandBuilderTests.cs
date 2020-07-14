using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Refashion.Database;

namespace RefashionTest.DatabaseTests
{
    [TestClass]
    public class CommandBuilderTests
    {
        [TestMethod]
        public void Constructor_Adds_String_To_Query()
        {
            string expected = "queryText";

            CommandBuilder commandBuilder = new CommandBuilder(expected);
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        // Fragile test relying on string comparison
        [TestMethod]
        public void AddEqualsParameter_Adds_Single_Parameter_To_Query()
        {
            string expected = "parameter = @parameter";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddEqualsParameter("parameter", "");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddEqualsParameter_Adds_Single_Parameter_To_Command()
        {
            string parameterName = "parameter";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddEqualsParameter(parameterName, "value");
            commandBuilder.CreateCommand(new MySqlConnection(""));

            Assert.IsTrue(commandBuilder.Command.Parameters.Contains(parameterName));
        }

        [TestMethod]
        public void AddEqualsParameter_Adds_OR_When_There_Are_Multiple_Parameters()
        {
            string expected = " OR parameter2 = @parameter2";
            CommandBuilder commandBuilder = new CommandBuilder("");
            commandBuilder.AddEqualsParameter("parameter", "");

            commandBuilder.AddEqualsParameter("parameter2", "");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddLikeParameter_Adds_Single_Parameter_To_Query()
        {
            string expected = "parameter LIKE @parameter";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddLikeParameter("parameter", "");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddLikeParameter_Adds_Single_Parameter_To_Command()
        {
            string parameterName = "parameter";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddLikeParameter(parameterName, "value");
            commandBuilder.CreateCommand(new MySqlConnection(""));

            Assert.IsTrue(commandBuilder.Command.Parameters.Contains(parameterName));
        }

        [TestMethod]
        public void AddLikeParameter_Adds_OR_When_There_Are_Multiple_Parameters()
        {
            string expected = "OR parameter2 LIKE @parameter2";
            CommandBuilder commandBuilder = new CommandBuilder("");
            commandBuilder.AddLikeParameter("parameter", "");

            commandBuilder.AddLikeParameter("parameter2", "");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void CreateCommand_Creates_Command_With_Query_Parameters()
        {
            string expected = "parameter = @parameter";
            CommandBuilder commandBuilder = new CommandBuilder(expected);
            commandBuilder.CreateCommand(new MySqlConnection(""));

            Assert.IsTrue(commandBuilder.Command.CommandText.Contains(expected));
        }

        [TestMethod]
        public void AddInsertParameters_Adds_Parameters_To_Query()
        {
            string expected = "(parameter1,parameter2)";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddInsertParameters(new List<string>
            {
                "parameter1",
                "parameter2"
            });

            string actual = commandBuilder.Query.ToString();
            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddInsertParameters_Given_Empty_List_Does_Not_Change_Query()
        {
            string expected = "";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddInsertParameters(new List<string>());

            string actual = commandBuilder.Query.ToString();
            Assert.AreEqual(expected, actual);
        }

    }
}
