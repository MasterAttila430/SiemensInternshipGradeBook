using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemRepository
{
    protected readonly List<Item> _items = new();
    protected int _nextId = 1;

    public virtual Task<Item?> GetByIdAsync(int id)
    {
        // Removed business logic (IsActive filter).
        var item = _items.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(item);
    }

    public virtual Task<IEnumerable<Item>> GetAllAsync()
    {
        // Removed business logic (IsActive filter).
        var items = _items.AsEnumerable();
        return Task.FromResult(items);
    }
}