using System;
using Core.Entities;

namespace Core.Interfaces;

public interface INewsRepository
{
    Task<IReadOnlyList<News>> GetNewsAsync(string? title, string? description,string? sort);
    //加?表示可能找不到此筆最新消息
    Task<News?> GetNewsByIdAsync(int id);

    Task<IReadOnlyList<string>> GetTitlesAsync();
    Task<IReadOnlyList<string>> GetDescriptionsAsync();

    //不使用非同步原因是這階段還沒異動資料庫 等需要異動資料時在做非同步
    void AddNews(News news);
    void UpdateNews(News news);
    void DeleteNews(News news);

    //判斷是否有傳入的這筆最新消息存在
    bool NewsExists(int id);
    //查看異動後的資料庫是否發生了變化 有變化傳true無則false
    Task<bool> SaveChangesAsync();
}
