using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly StoreContext context;

    public NewsController(StoreContext context)
    {
        this.context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> GetNews()
    {
        return await context.News.ToListAsync();
    }

    [HttpGet("{id:int}")] //api/news/2
    public async Task<ActionResult<News>> GetNewsItem(int id)
    {
        var newsItem = await context.News.FindAsync(id);

        if (newsItem == null) return NotFound();
        return newsItem;
    }

    [HttpPost]
    public async Task<ActionResult<News>> CreateNews(News news)
    {
        news.CreateDate = DateTime.Now;
        news.UpdateDate = DateTime.Now;
        context.News.Add(news);
        await context.SaveChangesAsync();
        return news;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, News news)
    {

        if (news.Id != id || !NewsExists(id))
            return BadRequest("Cannot update this News");

        context.Entry(news).State = EntityState.Modified;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteNews(int id)
    {
        var news = await context.News.FindAsync(id);

        if (news == null) return NotFound();

        context.News.Remove(news);

        await context.SaveChangesAsync();

        return NoContent();
    }
    private bool NewsExists(int id)
    {
        return context.News.Any(x => x.Id == id);
    }
}
