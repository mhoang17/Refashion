using System;
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

            commandBuilder.AddEqualParameter("parameter");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddEqualsParameter_Adds_OR_When_There_Are_Multiple_Parameters()
        {
            string expected = " OR parameter2 = @parameter2";
            CommandBuilder commandBuilder = new CommandBuilder("");
            commandBuilder.AddEqualParameter("parameter");

            commandBuilder.AddEqualParameter("parameter2");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddLikeParameter_Adds_Single_Parameter_To_Query()
        {
            string expected = "parameter LIKE @parameter";
            CommandBuilder commandBuilder = new CommandBuilder("");

            commandBuilder.AddLikeParameter("parameter");
            string actual = commandBuilder.Query.ToString();

            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void AddLikeParameter_Adds_OR_When_There_Are_Multiple_Parameters()
        {
            string expected = "OR parameter2 LIKE @parameter2";
            CommandBuilder commandBuilder = new CommandBuilder("");
            commandBuilder.AddLikeParameter("parameter");

            commandBuilder.AddLikeParameter("parameter2");
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

    }
}
