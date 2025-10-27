// See https://aka.ms/new-console-template for more information
using CsharpGalexy.LibraryExtention.Helpers.Collections;

Console.WriteLine("Hello, World!");
 List<TestEntity> GetSampleData() => new()
    {
        new TestEntity { Id = 1, Name = "سیب", Created = DateTime.Today.AddDays(-1) },
        new TestEntity { Id = 2, Name = "موز", Created = DateTime.Today },
        new TestEntity { Id = 3, Name = "انگور", Created = DateTime.Today.AddDays(-2) },
    };

 IQueryable<TestEntity> GetQueryable() => GetSampleData().AsQueryable();
var result = await GetQueryable().ToPagedList();
public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime Created { get; set; }
}
