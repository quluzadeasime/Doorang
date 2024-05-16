using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doorang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            var cards = _cardService.GetAllCard();

            return View(cards);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Card card)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _cardService.Add(card);
            }
            catch(NullCardException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _cardService.Delete(id);
            }
            catch(NullCardException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            var card = _cardService.GetCard(x=>x.Id==id);
            if (card == null) return View(card);
            return View(card);
        }

        [HttpPost]
        public IActionResult Update(Card card)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _cardService.Update(card.Id, card);
            return RedirectToAction(nameof(Index));
        }
    }
}
