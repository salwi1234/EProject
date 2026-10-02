using EProject.Data;
using EProject.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

public class GalleryController : Controller
{
    private readonly ApplicationDbContext _context;

    public GalleryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Gallery
    public async Task<IActionResult> Index()
    {
        return View(await _context.Galleries.ToListAsync());
    }

    // GET: Gallery/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gallery = await _context.Galleries
            .FirstOrDefaultAsync(m => m.GalleryId == id);

        if (gallery == null)
        {
            return NotFound();
        }

        return View(gallery);
    }

    // GET: Gallery/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Gallery/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("GalleryId,Title,ImagePath,Description,CreatedDate")] Gallery gallery)
    {
        if (ModelState.IsValid)
        {
            _context.Add(gallery);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(gallery);
    }

    // GET: Gallery/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gallery = await _context.Galleries.FindAsync(id);

        if (gallery == null)
        {
            return NotFound();
        }

        return View(gallery);
    }

    // POST: Gallery/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("GalleryId,Title,ImagePath,Description,CreatedDate")] Gallery gallery)
    {
        if (id != gallery.GalleryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(gallery);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryExists(gallery.GalleryId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(gallery);
    }

    // GET: Gallery/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gallery = await _context.Galleries
            .FirstOrDefaultAsync(m => m.GalleryId == id);

        if (gallery == null)
        {
            return NotFound();
        }

        return View(gallery);
    }

    // POST: Gallery/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var gallery = await _context.Galleries.FindAsync(id);

        if (gallery != null)
        {
            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool GalleryExists(int id)
    {
        return _context.Galleries.Any(e => e.GalleryId == id);
    }
}
