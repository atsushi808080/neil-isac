using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class NewRepository(StoreContext context) : INewsRepository
{
    public void AddNews(News news)
    {
        //Dbset定義的名稱
        context.News.Add(news);
    }

    public void DeleteNews(News news)
    {
        context.News.Remove(news);
    }

    public async Task<News?> GetNewsByIdAsync(int id)
    {
        return await context.News.FindAsync(id);
    }

    public async Task<IReadOnlyList<News>> GetNewsAsync(string? title, string? description, string? sort)
    {
        var query = context.News.AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(x => x.Title == title);

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(x => x.Description == description);

        query = sort switch
        {
            "sortAsc" => query.OrderBy(x => x.Sort),
            "sortDesc" => query.OrderByDescending(x => x.Sort),
            _ => query.OrderBy(x => x.Title)
        };

        return await query.ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        //返回int表示異動了幾筆
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateNews(News news)
    {
        context.Entry(news).State = EntityState.Modified;
    }
    public bool NewsExists(int id)
    {
        return context.News.Any(x => x.Id == id);
    }

    public async Task<IReadOnlyList<string>> GetTitlesAsync()
    {
        return await context.News.Select(x => x.Title)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetDescriptionsAsync()
    {
        return await context.News.Select(x => x.Description)
       .Distinct()
       .ToListAsync();
    }
}
