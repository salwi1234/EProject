using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class AboutPageController : Controller
{
    private readonly ApplicationDbContext _context;

    public AboutPageController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: AboutPage
    public async Task<IActionResult> Index()
    {
        return View(await _context.AboutPages.ToListAsync());
    }

    // GET: AboutPage/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aboutpage = await _context.AboutPages
            .FirstOrDefaultAsync(m => m.AboutPageId == id);

        if (aboutpage == null)
        {
            return NotFound();
        }

        return View(aboutpage);
    }

    // GET: AboutPage/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: AboutPage/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("AboutPageId,Title,Content,ImagePath,UpdatedDate")]
        AboutPage aboutpage)
    {
        if (ModelState.IsValid)
        {
            _context.Add(aboutpage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(aboutpage);
    }

    // GET: AboutPage/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aboutpage = await _context.AboutPages.FindAsync(id);

        if (aboutpage == null)
        {
            return NotFound();
        }

        return View(aboutpage);
    }

    // POST: AboutPage/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("AboutPageId,Title,Content,ImagePath,UpdatedDate")]
        AboutPage aboutpage)
    {
        if (id != aboutpage.AboutPageId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(aboutpage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutPageExists(aboutpage.AboutPageId))
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

        return View(aboutpage);
    }

    // GET: AboutPage/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aboutpage = await _context.AboutPages
            .FirstOrDefaultAsync(m => m.AboutPageId == id);

        if (aboutpage == null)
        {
            return NotFound();
        }

        return View(aboutpage);
    }

    // POST: AboutPage/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var aboutpage = await _context.AboutPages.FindAsync(id);

        if (aboutpage != null)
        {
            _context.AboutPages.Remove(aboutpage);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool AboutPageExists(int id)
    {
        return _context.AboutPages.Any(e => e.AboutPageId == id);
    }
}