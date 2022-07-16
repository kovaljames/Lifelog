using Lifelog.Domain.Entities;
using Lifelog.Domain.Queries;

namespace Lifelog.Domain.Tests.EntityTests;

[TestClass]
public class TaskItemQueryTests
{
    private List<TaskItem> _items;
    private DateTime _date;

    public TaskItemQueryTests()
    {
        _date = DateTime.Now;
        _items = new List<TaskItem>();
        _items.Add(new TaskItem("Tarefa 1", _date, _date.AddDays(1),
            "usuarioA", null, null, ""));
        _items.Add(new TaskItem("Tarefa 2", _date, _date.AddDays(1),
            "usuarioA", null, null, ""));
        _items.Add(new TaskItem("Tarefa 3", _date, _date.AddDays(1),
            "andre.baltieri", null, null, ""));
        _items.Add(new TaskItem("Tarefa 4", _date, _date.AddDays(2),
            "andre.baltieri", null, null, ""));
        _items.Add(new TaskItem("Tarefa 5", _date, _date.AddDays(1),
            "andre.baltieri", null, null, ""));
        _items[2].MarkAsDone(_items[2].DateEnd);
        _items[3].MarkAsDone(_items[3].DateEnd);
        _items[4].MarkAsDone(_items[4].DateEnd);
        _items[4].MarkAsUndone();
    }

    [TestMethod]
    public void Deve_retornar_tarefas_do_andrebaltieri()
    {
        var result =
            _items.AsQueryable()
            .Where(x => x.UserSlug == "andre.baltieri");
        Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void Deve_retornar_tarefas_concluidas_do_andrebaltieri()
    {
        var result =
            _items.AsQueryable()
            .Where(x => x.UserSlug == "andre.baltieri" && x.Done == true);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void Deve_retornar_tarefas_nao_concluidas_do_andrebaltieri()
    {
        var result =
            _items.AsQueryable()
            .Where(x => x.UserSlug == "andre.baltieri" && x.Done == false);
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public void Deve_retornar_tarefas_concluidas_do_andrebaltieri_periodo_1h()
    {
        var result =
            _items.AsQueryable()
            .Where(x => x.UserSlug == "andre.baltieri" && 
                        x.DateInit == _date.AddDays(1) && x.Done == true);
        Assert.AreEqual(1, result.Count());
    }
}