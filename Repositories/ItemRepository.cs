using System.Text.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly HttpClient _httpClient;

    private const string DataUrl = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw";

    public ItemRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
            Console.WriteLine($"Error: {ex.Message}");
            return new List<Item>();
        }
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        // Fetch all and find by ID
        var allItems = await GetAllAsync();
        return allItems.FirstOrDefault(i => i.Id == id);
    }
}