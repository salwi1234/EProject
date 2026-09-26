using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class ProgrammeController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProgrammeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Programme
    public async Task<IActionResult> Index()
    {
        var programmes = await _context.Programme
            .Include(p => p.NGO)
            .ToListAsync();

        return View(programmes);
    }

    // GET: Programme/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var programme = await _context.Programme
            .Include(p => p.NGO)
            .FirstOrDefaultAsync(m => m.ProgrammeId == id);

        if (programme == null)
        {
            return NotFound();
        }

        return View(programme);
    }

    // GET: Programme/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Programme/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("ProgrammeId,Title,Description,ScheduledDate,NGOId")] Programme programme)
    {
        if (ModelState.IsValid)
        {
            _context.Add(programme);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(programme);
    }

    // GET: Programme/Edit/1
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var programme = await _context.Programme.FindAsync(id);

        if (programme == null)
        {
            return NotFound();
        }

        return View(programme);
    }

    // POST: Programme/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("ProgrammeId,Title,Description,ScheduledDate,NGOId")] Programme programme)
    {
        if (id != programme.ProgrammeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(programme);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeExists(programme.ProgrammeId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(programme);
    }

    // GET: Programme/Delete/1
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var programme = await _context.Programme
            .Include(p => p.NGO)
            .FirstOrDefaultAsync(m => m.ProgrammeId == id);

        if (programme == null)
        {
            return NotFound();
        }

        return View(programme);
    }

    // POST: Programme/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var programme = await _context.Programme.FindAsync(id);

        if (programme != null)
        {
            _context.Programme.Remove(programme);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ProgrammeExists(int id)
    {
        return _context.Programme.Any(e => e.ProgrammeId == id);
    }
}