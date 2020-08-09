using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesManagement.Database.UnitsOfWork;
using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;
using BoardGamesManagement.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesManagement.Controllers
{
    public class GamesListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly  IHelper<Game, GameDTO> _gameHelper;
        private readonly IHelper<Game, GamesListDTO> _gameListHelper;

        public GamesListController(IUnitOfWork unitOfWork, 
            IHelper<Game, GameDTO> gameHelper, 
            IHelper<Game, GamesListDTO> gameListHelper)
        {
            _unitOfWork = unitOfWork;
            _gameHelper = gameHelper;
            _gameListHelper = gameListHelper;
        }

        // GET: GamesListController
        public async Task<ActionResult> Index()
        {
            var games = await _unitOfWork.GamesRepository.GetGamesAsync(null);

            var gamesDTOs = games.Select(game => _gameListHelper.GetDTO(game));

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

            var gameDTO = _gameHelper.GetDTO(game);

            return View(gameDTO);
        }
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
                    _unitOfWork.GamesRepository.Update(game);
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
