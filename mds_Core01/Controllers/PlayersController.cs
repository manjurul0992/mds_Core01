using mds_Core01.Models;
using mds_Core01.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace mds_Core01.Controllers
{
    public class PlayersController : Controller
    {
        private readonly mds_Core01Context _context;
        private readonly IWebHostEnvironment _environment;
        public PlayersController(mds_Core01Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.Include(x => x.SeriesEntries).ThenInclude(y => y.Format).ToListAsync());
        }
        public IActionResult AddNewFormats(int? id)
        {
            ViewBag.format = new SelectList(_context.Formats, "FormatId", "FormatName", id.ToString() ?? "");
            return PartialView("_addNewFormats");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerVM playerVM, int[] FormatId)
        {
            if (ModelState.IsValid)
            {
                Player player = new Player
                {
                    PlayerName = playerVM.PlayerName,
                    BirthDate = playerVM.BirthDate,
                    Phone = playerVM.Phone,
                    MaritalStatus = playerVM.MaritalStatus,

                };
                var file = playerVM.PicturePath;
                string webroot = _environment.WebRootPath;
                string folder = "Images";
                string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
                string fileToSave = Path.Combine(webroot, folder, pictureFileName);
                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        playerVM.PicturePath.CopyTo(stream);
                        player.Picture = "/" + folder + "/" + pictureFileName;
                    }
                }
                foreach (var item in FormatId)
                {
                    SeriesEntry playerFormats = new SeriesEntry()
                    {
                        Player = player,
                        PlayerId = player.PlayerId,
                        FormatId = item
                    };
                    _context.SeriesEntries.Add(playerFormats);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                BirthDate = player.BirthDate,
                Phone = player.Phone,
                Picture = player.Picture,
                MaritalStatus = player.MaritalStatus
            };
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var item in existFormat)
            {
                playerVM.FormatList.Add(item.FormatId);
            }
            return View(playerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerVM playerVM, int[] FormatId)
        {
            if (ModelState.IsValid)
            {
                Player player = new Player
                {
                    PlayerId = playerVM.PlayerId,
                    PlayerName = playerVM.PlayerName,
                    BirthDate = playerVM.BirthDate,
                    Phone = playerVM.Phone,
                    MaritalStatus = playerVM.MaritalStatus,
                    Picture = playerVM.Picture

                };
                var file = playerVM.PicturePath;
                if (file != null)
                {
                    string webroot = _environment.WebRootPath;
                    string folder = "Images";
                    string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
                    string fileToSave = Path.Combine(webroot, folder, pictureFileName);
                    if (file != null)
                    {
                        using (var stream = new FileStream(fileToSave, FileMode.Create))
                        {
                            playerVM.PicturePath.CopyTo(stream);
                            player.Picture = "/" + folder + "/" + pictureFileName;
                        }
                    }
                }
                var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == player.PlayerId).ToList();
                foreach (var item in existFormat)
                {
                    _context.SeriesEntries.Remove(item);
                }
                foreach (var item in FormatId)
                {
                    SeriesEntry seriesEntry = new SeriesEntry()
                    {

                        PlayerId = player.PlayerId,
                        FormatId = item
                    };
                    _context.SeriesEntries.Add(seriesEntry);

                }
                _context.Update(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playerVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var item in existFormat)
            {
                _context.SeriesEntries.Remove(item);
            }
            _context.Remove(player);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
