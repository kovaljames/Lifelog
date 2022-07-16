using Lifelog.Domain.Commands;
using Lifelog.Domain.Handlers;
using Lifelog.Domain.Tests.Repositories;

namespace Lifelog.Domain.Tests.HandlerTests;

[TestClass]
public class CreateTaskItemHandlerTests
{
    private readonly CreateTaskItemCommand _invalidCommand =
        new CreateTaskItemCommand("", DateTime.Now, 
            DateTime.Now.AddDays(-1), "", null, null);
    private readonly CreateTaskItemCommand _validCommand = new CreateTaskItemCommand(
        "Tarefa 1", DateTime.Now, DateTime.Now.AddDays(1),
            "usuarioA", null, null);
    private readonly TaskItemHandler _handler =
        new TaskItemHandler(new FakeTaskItemRepository());

    public CreateTaskItemHandlerTests()
    {

    }

    [TestMethod]
    public void Dado_comando_invalido_deve_interromper_execucao()
    {
        var result = (GenericCommandResult)_handler.Handle(_invalidCommand);
        Assert.AreEqual(result.Success, false);
    }

    [TestMethod]
    public void Dado_comando_valido_deve_criar_a_tarefa()
    {
        var result = (GenericCommandResult)_handler.Handle(_validCommand);
        Assert.AreEqual(result.Success, true);
    }
}