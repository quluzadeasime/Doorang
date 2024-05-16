using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        public CardRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
