using ExchangeRate.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Services.Contracts
{
    public interface IArticlesService
    {
        void CreateArticle();

        void EditArticle();

        void DeleteArticle();

        ICollection<Article> GetAllArtiles();        
    }
}
