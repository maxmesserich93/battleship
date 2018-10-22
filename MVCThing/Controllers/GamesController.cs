using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCThing.Models;
using MVCThing.ViewModel;

namespace MVCThing.Controllers
{
    public class GamesController : Controller
    {
        private readonly MVCThingContext _context;

        public GamesController(MVCThingContext context)
        {
            _context = context;
        }

        private GameViewModel CreateGameViewModel(Game dbGame)
        {
            var p1 = _context.Player.Where(player => player.ID.Equals(dbGame.PlayerOneID)).FirstOrDefault();
            var p2 = _context.Player.Where(player => player.ID.Equals(dbGame.PlayerTwoID)).FirstOrDefault();
            Player winner = p1;
            if (dbGame.Winner == 1)
            {
                winner = p2;
            }
            return new GameViewModel(dbGame.ID, p1, p2, winner, dbGame.Date, dbGame.Player1Hits, dbGame.Player2Hits);
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {



            var gameVms = _context.Game.ToList().Select(dbGame => CreateGameViewModel(dbGame)).ToList();

            return View(gameVms);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            var model = CreateGameViewModel(game);
            return View(model);
        }


        // GET: Games/Create
        public IActionResult Create()
        {
            var vm = new Game();
            var players = _context.Player.ToList();

            //var a = players.Select(player => new SelectListItem { Value = player.ID.ToString(), Text = player.Name }).ToList();

            ViewBag.PlayerList1 = new SelectList(players, "ID", "Name");
            ViewBag.PlayerList2 = new SelectList(players, "ID", "Name");
            //ViewBag.Player2 = new SelectList(players, "ID", "ID");
            return View(vm);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PlayerOneID, PlayerTwoID, Date,Player1Hits,Player2Hits,Winner")] Game game)
        {
            //return null;
            if (ModelState.IsValid)
            {
                Debug.WriteLine( "Player1ID: " + game.PlayerOneID + " Player2ID: " + game.PlayerTwoID);

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var players = _context.Player.ToList();
            ViewBag.PlayerList1 = new SelectList(players, "ID", "Name");
            ViewBag.PlayerList2 = new SelectList(players, "ID", "Name");
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Player1, Player2, Date,Player1Hits,Player2Hits,Winner")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.ID == id);
        }
    }
}
