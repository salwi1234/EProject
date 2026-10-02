using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class DonationCauseController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationCauseController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DonationCause
    public async Task<IActionResult> Index()
    {
        return View(await _context.DonationCauses.ToListAsync());
    }

    // GET: DonationCause/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donationCause = await _context.DonationCauses
            .FirstOrDefaultAsync(m => m.DonationCauseId == id);

        if (donationCause == null)
        {
            return NotFound();
        }

        return View(donationCause);
    }

    // GET: DonationCause/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DonationCause/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("DonationCauseId,CauseName,Description,IsActive")]
        DonationCause donationCause)
    {
        if (ModelState.IsValid)
        {
            _context.Add(donationCause);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(donationCause);
    }

    // GET: DonationCause/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donationCause = await _context.DonationCauses.FindAsync(id);

        if (donationCause == null)
        {
            return NotFound();
        }

        return View(donationCause);
    }

    // POST: DonationCause/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("DonationCauseId,CauseName,Description,IsActive")]
        DonationCause donationCause)
    {
        if (id != donationCause.DonationCauseId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(donationCause);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationCauseExists(donationCause.DonationCauseId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(donationCause);
    }

    // GET: DonationCause/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donationCause = await _context.DonationCauses
            .FirstOrDefaultAsync(m => m.DonationCauseId == id);

        if (donationCause == null)
        {
            return NotFound();
        }

        return View(donationCause);
    }

    // POST: DonationCause/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var donationCause = await _context.DonationCauses
            .FindAsync(id);

        if (donationCause != null)
        {
            _context.DonationCauses.Remove(donationCause);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool DonationCauseExists(int id)
    {
        return _context.DonationCauses
            .Any(e => e.DonationCauseId == id);
    }
}