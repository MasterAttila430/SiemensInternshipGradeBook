using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService : IGradeService
{
    private readonly IItemRepository _repository;

    public GradeService(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Item>> GetFirstNPassingGradesAsync(int n)
    {
        var allItems = await _repository.GetAllAsync();

        return allItems
            .Where(i => i.IsActive && i.Value >= 5)
            .Take(n);
    }
}