using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesManagement.Database.UnitsOfWork;
using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesManagement.Controllers
{
    public class GamesListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamesListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GamesListController
        public async Task<ActionResult> Index()
        {
            var games = await _unitOfWork.GamesRepository.GetGamesAsync(null);

            var gamesDTOs = games.Select(game => new GamesListDTO
            {
                Id = game.Id,
                Name = game.Name
            });

            return View(gamesDTOs);
        }

        // GET: GamesListController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var game = await _unitOfWork.GamesRepository.GetGameByIdAsync(id);
            if (game == null)
                return RedirectToAction(nameof(Index));

            await _unitOfWork.GamesHistoryRepository.AddToVisitHistory(new GameHistory
            {
                DisplayDate = DateTime.UtcNow,
                GameId = id,
                Game = game,
                Source = "Web",
            });

            await _unitOfWork.Save();

            var gameDTO = new GameDTO
            {
                Id = game.Id,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                MinRecommendedAge = game.MinRecommendedAge,
                Name = game.Name,
                GameHistories = game.GameHistory.OrderBy(p => p.DisplayDate).Select(p => new GameHistoryDTO
                {
                    Source = p.Source,
                    DisplayDate = p.DisplayDate
                }).ToList()
            };

            return View(gameDTO);
        }

        // GET: GamesListController/Create
        public ActionResult Create()
        {
            return View(new Game());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdGame = await _unitOfWork.GamesRepository.AddAsync(game);
                    await _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Unable to save item with id: {game.Id}\n Error msg: {e.Message}");
            }
            return View(game);
        }

        // GET: GamesListController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var game = await _unitOfWork.GamesRepository.GetGameByIdAsync(id);
            if(game != null)
                return View(game);

            return RedirectToAction(nameof(Index));
        }

        // POST: GamesListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.GamesRepository.UpdateAsync(game);
                    await _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", $"Unable to save item with id: {game.Id}\n Error msg: {e.Message}");
            }
            return View();
        }

        // GET: GamesListController/Delete/5
        public async  Task<ActionResult> Delete(Guid id)
        {
            var game = await _unitOfWork.GamesRepository.GetGameByIdAsync(id);
            if (game != null)
                return View(game);

            return RedirectToAction(nameof(Index));
        }

        // POST: GamesListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _unitOfWork.GamesRepository.DeleteAsync(id);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", $"Unable to delete item with id: {id}\n Error msg: {e.Message}");
            }
            return View();
        }
    }
}
