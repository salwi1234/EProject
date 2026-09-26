
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EProject.Models;
using EProject.Data;

public class QueryController : Controller
{
    private readonly ApplicationDbContext _context;

    public QueryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Query
    public async Task<IActionResult> Index()
    {
        return View(await _context.Query.ToListAsync());
    }

    // GET: Query/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = await _context.Query
            .FirstOrDefaultAsync(m => m.QueryId == id);

        if (query == null)
        {
            return NotFound();
        }

        return View(query);
    }

    // GET: Query/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Query/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("QueryId,SenderName,SenderEmail,MessageContent,AdminReply,IsResolved")] Query query)
    {
        if (ModelState.IsValid)
        {
            _context.Add(query);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(query);
    }

    // GET: Query/Edit/1
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = await _context.Query.FindAsync(id);

        if (query == null)
        {
            return NotFound();
        }

        return View(query);
    }

    // POST: Query/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("QueryId,SenderName,SenderEmail,MessageContent,AdminReply,IsResolved")] Query query)
    {
        if (id != query.QueryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(query);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueryExists(query.QueryId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(query);
    }

    // GET: Query/Delete/1
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = await _context.Query
            .FirstOrDefaultAsync(m => m.QueryId == id);

        if (query == null)
        {
            return NotFound();
        }

        return View(query);
    }

    // POST: Query/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var query = await _context.Query.FindAsync(id);

        if (query != null)
        {
            _context.Query.Remove(query);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool QueryExists(int id)
    {
        return _context.Query.Any(e => e.QueryId == id);
    }
}

