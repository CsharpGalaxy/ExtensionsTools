using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class CollectionHelperTests
{
    [Fact]
    public void RandomList_ShouldReturnRequestedCount()
    {
        var list = CollectionHelper.RandomList(() => "test", count: 10);
        Assert.Equal(10, list.Count);
    }

    [Fact]
    public void RandomList_ShouldReturnEmptyListForZeroCount()
    {
        var list = CollectionHelper.RandomList(() => "test", count: 0);
        Assert.Empty(list);
    }

    [Fact]
    public void RandomList_ShouldCallFunctionCorrectly()
    {
        int callCount = 0;
        var list = CollectionHelper.RandomList(() => { callCount++; return "test"; }, count: 5);
        Assert.Equal(5, callCount);
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void UniqueList_ShouldReturnRequestedCount()
    {
        var list = CollectionHelper.UniqueList(() => Guid.NewGuid().ToString(), count: 5);
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void UniqueList_ShouldContainUniqueElements()
    {
        var list = CollectionHelper.UniqueList(() => Guid.NewGuid().ToString(), count: 10);
        var uniqueList = list.Distinct().ToList();
        Assert.Equal(list.Count, uniqueList.Count);
    }

    [Fact]
    public void RandomItem_ShouldReturnItemFromList()
    {
        var items = new List<string> { "A", "B", "C", "D", "E" };
        var item = CollectionHelper.RandomItem(items);
        Assert.Contains(item, items);
    }

    [Fact]
    public void RandomItem_ShouldThrowForEmptyList()
    {
        var emptyList = new List<string>();
        Assert.Throws<InvalidOperationException>(() => CollectionHelper.RandomItem(emptyList));
    }

    [Fact]
    public void RandomItems_ShouldReturnRequestedCount()
    {
        var items = new List<string> { "A", "B", "C", "D", "E" };
        var selected = CollectionHelper.RandomItems(items, count: 3);
        Assert.Equal(3, selected.Count);
    }

    [Fact]
    public void RandomItems_ShouldReturnItemsFromOriginalList()
    {
        var items = new List<string> { "A", "B", "C", "D", "E" };
        var selected = CollectionHelper.RandomItems(items, count: 3);
        Assert.All(selected, item => Assert.Contains(item, items));
    }

    [Fact]
    public void RandomItems_ShouldReturnEmptyForZeroCount()
    {
        var items = new List<string> { "A", "B", "C" };
        var selected = CollectionHelper.RandomItems(items, count: 0);
        Assert.Empty(selected);
    }

    [Fact]
    public void Shuffle_ShouldReturnSameCountOfElements()
    {
        var items = new List<int> { 1, 2, 3, 4, 5 };
        var shuffled = CollectionHelper.Shuffle(items);
        Assert.Equal(items.Count, shuffled.Count);
    }

    [Fact]
    public void Shuffle_ShouldContainAllOriginalElements()
    {
        var items = new List<int> { 1, 2, 3, 4, 5 };
        var shuffled = CollectionHelper.Shuffle(items);
        foreach (var item in items)
        {
            Assert.Contains(item, shuffled);
        }
    }

    [Fact]
    public void Shuffle_ShouldNotModifyOriginalList()
    {
        var items = new List<int> { 1, 2, 3, 4, 5 };
        var original = new List<int>(items);
        var shuffled = CollectionHelper.Shuffle(items);
        Assert.Equal(original, items);
    }

    [Fact]
    public void Shuffle_ShouldProduceRandomOrder()
    {
        var items = new List<int> { 1, 2, 3, 4, 5 };
        var orders = new HashSet<string>();
        
        for (int i = 0; i < 20; i++)
        {
            var shuffled = CollectionHelper.Shuffle(items);
            orders.Add(string.Join(",", shuffled));
        }

        // With 20 shuffles, should get at least 2 different orders
        Assert.True(orders.Count > 1);
    }

    [Fact]
    public void Batch_ShouldReturnCorrectNumberOfBatches()
    {
        var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var batches = CollectionHelper.Batch(items, batchSize: 3);
        Assert.Equal(4, batches.Count); // 3 + 3 + 3 + 1
    }

    [Fact]
    public void Batch_ShouldContainAllElements()
    {
        var items = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        var batches = CollectionHelper.Batch(items, batchSize: 2);
        var flattened = batches.SelectMany(b => b).ToList();
        Assert.Equal(items.Count, flattened.Count);
    }

    [Fact]
    public void Batch_ShouldHaveCorrectBatchSizes()
    {
        var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var batches = CollectionHelper.Batch(items, batchSize: 4);
        
        // First 2 batches should have 4 items, last should have 1
        Assert.Equal(4, batches[0].Count);
        Assert.Equal(4, batches[1].Count);
        Assert.Equal(1, batches[2].Count);
    }

    [Fact]
    public void ToDataTable_ShouldReturnValidDataTable()
    {
        var items = new List<TestItem>
        {
            new TestItem { Id = 1, Name = "Item1" },
            new TestItem { Id = 2, Name = "Item2" }
        };

        var table = CollectionHelper.ToDataTable(items);
        Assert.NotNull(table);
        Assert.Single(table.Rows.Cast<System.Data.DataRow>().Where(r => r["Id"].Equals(1)));
    }

    [Fact]
    public void ToDataTable_ShouldHaveCorrectColumns()
    {
        var items = new List<TestItem>
        {
            new TestItem { Id = 1, Name = "Item1" }
        };

        var table = CollectionHelper.ToDataTable(items);
        Assert.Contains("Id", table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName));
        Assert.Contains("Name", table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName));
    }

    [Fact]
    public void ToDataTable_ShouldPopulateData()
    {
        var items = new List<TestItem>
        {
            new TestItem { Id = 1, Name = "Item1" },
            new TestItem { Id = 2, Name = "Item2" }
        };

        var table = CollectionHelper.ToDataTable(items);
        Assert.Equal(1, table.Rows[0]["Id"]);
        Assert.Equal("Item1", table.Rows[0]["Name"]);
        Assert.Equal(2, table.Rows[1]["Id"]);
        Assert.Equal("Item2", table.Rows[1]["Name"]);
    }

    private class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
