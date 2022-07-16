using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Tests.EntityTests;

[TestClass]
public class TaskItemTests
{
    private readonly TaskItem _validTask =
        new TaskItem("Tarefa 1", DateTime.Now, DateTime.Now.AddDays(1),
            "usuarioA", null, null, "");

    [TestMethod]
    public void Dado_um_novo_task_nao_pode_ser_concluido()
    {
        Assert.AreEqual(_validTask.Done, false);
    }
}