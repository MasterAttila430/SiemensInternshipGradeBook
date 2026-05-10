using System.Text.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using Microsoft.Extensions.Logging;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ItemRepository> _logger;

    private const string DataUrl = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw";

    public ItemRepository(HttpClient httpClient, ILogger<ItemRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    private class ItemResponse
    {
        public List<Item> Items { get; set; } = new();
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        try
        {
            var jsonResponse = await _httpClient.GetStringAsync(DataUrl);

            var response = JsonSerializer.Deserialize<ItemResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return response?.Items ?? new List<Item>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hiba történt az adatok lekérésekor a Gist végpontról.");
            return new List<Item>();
        }
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        var allItems = await GetAllAsync();
        return allItems.FirstOrDefault(i => i.Id == id);
    }
}