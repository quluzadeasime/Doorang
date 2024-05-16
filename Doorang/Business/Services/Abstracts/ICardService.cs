using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ICardService
    {
        void Add(Card card);
        void Delete(int id);
        void Update(int id, Card card);
        Card GetCard(Func<Card, bool> func = null);
        List<Card> GetAllCard(Func<Card, bool> func = null);
    }
}
