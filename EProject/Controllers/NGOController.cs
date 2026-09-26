using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class NGOController : Controller
{
    private readonly ApplicationDbContext _context;

    public NGOController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: NGO
    public async Task<IActionResult> Index()
    {
        return View(await _context.NGOs.ToListAsync());
    }

    // GET: NGO/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ngo = await _context.NGOs
            .FirstOrDefaultAsync(m => m.NGOId == id);

        if (ngo == null)
        {
            return NotFound();
        }

        return View(ngo);
    }

    // GET: NGO/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: NGO/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("NGOId,NGOName,MissionStatement,LogoPath,ContactEmail")] NGO ngo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ngo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(ngo);
    }

    // GET: NGO/Edit/1
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ngo = await _context.NGOs.FindAsync(id);

        if (ngo == null)
        {
            return NotFound();
        }

        return View(ngo);
    }

    // POST: NGO/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("NGOId,NGOName,MissionStatement,LogoPath,ContactEmail")] NGO ngo)
    {
        if (id != ngo.NGOId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ngo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NGOExists(ngo.NGOId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(ngo);
    }

    // GET: NGO/Delete/1
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ngo = await _context.NGOs
            .FirstOrDefaultAsync(m => m.NGOId == id);

        if (ngo == null)
        {
            return NotFound();
        }

        return View(ngo);
    }

    // POST: NGO/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ngo = await _context.NGOs.FindAsync(id);

        if (ngo != null)
        {
            _context.NGOs.Remove(ngo);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Check NGO exists
    private bool NGOExists(int id)
    {
        return _context.NGOs.Any(e => e.NGOId == id);
    }
}