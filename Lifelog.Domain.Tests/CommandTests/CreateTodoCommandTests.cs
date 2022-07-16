using Lifelog.Domain.Commands;

namespace Lifelog.Domain.Tests.CommandTests
{
    [TestClass]
    public class CreateTodoCommandTests
    {
        private readonly CreateTaskItemCommand _invalidCommand =
            new CreateTaskItemCommand("", DateTime.Now, 
                DateTime.Now.AddDays(-1), "",1, null);
        private readonly CreateTaskItemCommand _validCommand =
            new CreateTaskItemCommand(
                "Task Title", DateTime.Now, 
                DateTime.Now.AddDays(1).AddHours(1), "AndreBaltieri", 1, null);

        public CreateTodoCommandTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void Dado_um_comando_invalido()
        {
            Assert.AreEqual(_invalidCommand.IsValid, false);
        }

        [TestMethod]
        public void Dado_um_comando_valido()
        {
            Assert.AreEqual(_validCommand.IsValid, true);
        }
    }
}