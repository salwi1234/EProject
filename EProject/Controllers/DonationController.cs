using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class DonationController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DONATIONS
    public async Task<IActionResult> Index()
    {
        var donations = await _context.Donation
            .Include(d => d.User)
            .Include(d => d.NGO)
            .ToListAsync();

        return View(donations);
    }

    // GET: DONATIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donation = await _context.Donation
            .Include(d => d.User)
            .Include(d => d.NGO)
            .FirstOrDefaultAsync(m => m.DonationId == id);

        if (donation == null)
        {
            return NotFound();
        }

        return View(donation);
    }

    // GET: DONATIONS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DONATIONS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("DonationId,Amount,CauseCategory,DummyCardNumber,ExpiryDate,CVV,UserId,User,NGOId,NGO")]
        Donation donation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(donation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(donation);
    }

    // GET: DONATIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donation = await _context.Donation.FindAsync(id);

        if (donation == null)
        {
            return NotFound();
        }

        return View(donation);
    }

    // POST: DONATIONS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("DonationId,Amount,CauseCategory,DummyCardNumber,ExpiryDate,CVV,UserId,User,NGOId,NGO")]
        Donation donation)
    {
        if (id != donation.DonationId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(donation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(donation.DonationId))
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

        return View(donation);
    }

    // GET: DONATIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donation = await _context.Donation
            .Include(d => d.User)
            .Include(d => d.NGO)
            .FirstOrDefaultAsync(m => m.DonationId == id);

        if (donation == null)
        {
            return NotFound();
        }

        return View(donation);
    }

    // POST: DONATIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var donation = await _context.Donation.FindAsync(id);

        if (donation != null)
        {
            _context.Donation.Remove(donation);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool DonationExists(int? id)
    {
        return _context.Donation.Any(e => e.DonationId == id);
    }
}