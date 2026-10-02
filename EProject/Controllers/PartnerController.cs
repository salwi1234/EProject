using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class PartnerController : Controller
{
    private readonly ApplicationDbContext _context;

    public PartnerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Partner
    public async Task<IActionResult> Index()
    {
        return View(await _context.Partners.ToListAsync());
    }

    // GET: Partner/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var partner = await _context.Partners
            .FirstOrDefaultAsync(m => m.PartnerId == id);

        if (partner == null)
        {
            return NotFound();
        }

        return View(partner);
    }

    // GET: Partner/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Partner/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("PartnerId,PartnerName,Email,LogoPath,Description")]
        Partner partner)
    {
        if (ModelState.IsValid)
        {
            _context.Add(partner);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(partner);
    }

    // GET: Partner/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var partner = await _context.Partners.FindAsync(id);

        if (partner == null)
        {
            return NotFound();
        }

        return View(partner);
    }

    // POST: Partner/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("PartnerId,PartnerName,Email,LogoPath,Description")]
        Partner partner)
    {
        if (id != partner.PartnerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(partner);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerExists(partner.PartnerId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(partner);
    }

    // GET: Partner/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var partner = await _context.Partners
            .FirstOrDefaultAsync(m => m.PartnerId == id);

        if (partner == null)
        {
            return NotFound();
        }

        return View(partner);
    }

    // POST: Partner/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var partner = await _context.Partners.FindAsync(id);

        if (partner != null)
        {
            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool PartnerExists(int id)
    {
        return _context.Partners.Any(e => e.PartnerId == id);
    }
}