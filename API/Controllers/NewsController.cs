using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController(INewsRepository repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<News>>> GetNews()
    {
        return Ok(await repo.GetNewsAsync());
    }

    [HttpGet("{id:int}")] //api/news/2
    public async Task<ActionResult<News>> GetNewsItem(int id)
    {
        var newsItem = await repo.GetNewsByIdAsync(id);

        if (newsItem == null) return NotFound();

        return newsItem;
    }

    [HttpPost]
    public async Task<ActionResult<News>> CreateNews(News news)
    {
        news.CreateDate = DateTime.Now;
        news.UpdateDate = DateTime.Now;

        repo.AddNews(news);

        if (await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetNewsItem", new { id = news.Id }, news);
        }
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, News news)
    {

        if (news.Id != id || !NewsExists(id))
            return BadRequest("Cannot update this News");

        repo.UpdateNews(news);

        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("problem updating the news");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteNews(int id)
    {
        var news = await repo.GetNewsByIdAsync(id);

        if (news == null) return NotFound();

        repo.DeleteNews(news);
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("problem updating the news");
    }
    private bool NewsExists(int id)
    {
        return repo.NewsExists(id);
    }
}
