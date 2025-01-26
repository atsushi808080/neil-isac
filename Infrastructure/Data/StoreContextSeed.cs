using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        try
        {
            if (!context.News.Any())
            {
                Console.WriteLine("開始讀取種子資料...");
                var newsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                Console.WriteLine($"檔案內容長度: {newsData.Length}");

                var news = JsonSerializer.Deserialize<List<News>>(newsData);
                Console.WriteLine($"解析資料數量: {news?.Count ?? 0}");

                if (news == null) return;

                context.News.AddRange(news);
                await context.SaveChangesAsync();
                Console.WriteLine("寫入完成");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"錯誤: {ex.Message}");
            throw;
        }
    }
}
