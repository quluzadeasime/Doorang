using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CardService(ICardRepository cardRepository, IWebHostEnvironment webHostEnvironment)
        {
            _cardRepository = cardRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Add(Card card)
        {
            if (card == null) throw new NullCardException("", "Card bos ola bilmez");
            if (!card.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Content sehvdir");
            if (card.PhotoFile.Length > 2097152) throw new FileSizeException("", "Max olcu 2 mb ola biler!!");

            string path = _webHostEnvironment.WebRootPath + @"\uploads\" + card.PhotoFile.FileName;
            using (FileStream fileName = new FileStream(path, FileMode.Create))
            {
                card.PhotoFile.CopyTo(fileName);
            }

            card.ImgUrl = card.PhotoFile.FileName;
            _cardRepository.Add(card);
            _cardRepository.Commit();
        }

        public void Delete(int id)
        {
            var existCard = _cardRepository.GetCard(x => x.Id == id);
            if (existCard == null) throw new NullCardException("","Card yoxdur");
            string path = _webHostEnvironment.WebRootPath + @"\uploads\" + existCard.ImgUrl;

            if (!File.Exists(path))
                throw new Exceptions.FileNotFoundException("PhotoFile", "File yoxdur");

            File.Delete(path);
            _cardRepository.Delete(existCard);
            _cardRepository.Commit();


        }

        public List<Card> GetAllCard(Func<Card, bool> func = null)
        {
            return _cardRepository.GetAllCard(func);
        }

        public Card GetCard(Func<Card, bool> func = null)
        {
            return _cardRepository.GetCard(func);
        }

        public void Update(int id, Card card)
        {
            var existCard = _cardRepository.GetCard(x => x.Id == id);
            if (existCard == null) throw new NullCardException("", "Card yoxdur");
            if (card == null) throw new CardNotFoundException("", "Card bos ola bilmez");

            if (card.PhotoFile != null)
            {
                if (!card.PhotoFile.FileName.Contains("image/")) throw new ContentTypeException("", "Content sehvdir");
                if (card.PhotoFile.Length > 2097152) throw new FileSizeException("", "Max olcu 2 mb ola biler!!");

                string path = _webHostEnvironment.WebRootPath + @"\uploads\" + card.PhotoFile.FileName;
                using (FileStream fileName = new FileStream(path, FileMode.Create))
                {
                    card.PhotoFile.CopyTo(fileName);
                }

                existCard.ImgUrl = card.PhotoFile.FileName;
            }
            existCard.Title = card.Title;
            existCard.Subtitle = card.Subtitle;
            existCard.Description = card.Description;
            _cardRepository.Commit();
        }
    }
}
