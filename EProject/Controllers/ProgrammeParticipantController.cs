
using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProgrammeParticipantController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProgrammeParticipantController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ProgrammeParticipant
    public async Task<IActionResult> Index()
    {
        var participants = await _context.ProgrammeParticipants
            .Include(p => p.User)
            .Include(p => p.Programme)
            .ToListAsync();

        return View(participants);
    }

    // GET: ProgrammeParticipant/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var programmeparticipant = await _context.ProgrammeParticipants
            .Include(p => p.User)
            .Include(p => p.Programme)
            .FirstOrDefaultAsync(
                m => m.ProgrammeParticipantId == id);

        if (programmeparticipant == null)
        {
            return NotFound();
        }

        return View(programmeparticipant);
    }

    // GET: ProgrammeParticipant/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ProgrammeParticipant/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("ProgrammeParticipantId,UserId,User,ProgrammeId,Programme,ParticipationDate,Status")]
        ProgrammeParticipant programmeparticipant)
    {
        if (ModelState.IsValid)
        {
            _context.Add(programmeparticipant);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(programmeparticipant);
    }

    // GET: ProgrammeParticipant/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var programmeparticipant =
            await _context.ProgrammeParticipants.FindAsync(id);

        if (programmeparticipant == null)
        {
            return NotFound();
        }

        return View(programmeparticipant);
    }

    // POST: ProgrammeParticipant/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("ProgrammeParticipantId,UserId,User,ProgrammeId,Programme,ParticipationDate,Status")]
        ProgrammeParticipant programmeparticipant)
    {
        if (id != programmeparticipant.ProgrammeParticipantId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(programmeparticipant);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeParticipantExists(
                    programmeparticipant.ProgrammeParticipantId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(programmeparticipant);
    }

    // GET: ProgrammeParticipant/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var programmeparticipant =
            await _context.ProgrammeParticipants
                .Include(p => p.User)
                .Include(p => p.Programme)
                .FirstOrDefaultAsync(
                    m => m.ProgrammeParticipantId == id);

        if (programmeparticipant == null)
        {
            return NotFound();
        }

        return View(programmeparticipant);
    }

    // POST: ProgrammeParticipant/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var programmeparticipant =
            await _context.ProgrammeParticipants.FindAsync(id);

        if (programmeparticipant != null)
        {
            _context.ProgrammeParticipants.Remove(programmeparticipant);

            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ProgrammeParticipantExists(int id)
    {
        return _context.ProgrammeParticipants
            .Any(e => e.ProgrammeParticipantId == id);
    }
}

