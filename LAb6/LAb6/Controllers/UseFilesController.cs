using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAb6.Data;
using LAb6.Models;

namespace LAb6.Controllers
{
    public class UseFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UseFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UseFiles
        public async Task<IActionResult> Index()
        {
              return _context.UseFiles != null ? 
                          View(await _context.UseFiles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UseFile'  is null.");
        }

        // GET: UseFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UseFiles == null)
            {
                return NotFound();
            }

            var useFile = await _context.UseFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useFile == null)
            {
                return NotFound();
            }

            return View(useFile);
        }

        // GET: UseFiles/Create
        public IActionResult Create(int id)
        {
            ViewBag.ReplyId = id;
            return View();
        }

        // POST: UseFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Path,ParentReplyId")] UseFile useFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(useFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(useFile);
        }

        // GET: UseFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UseFiles == null)
            {
                return NotFound();
            }

            var useFile = await _context.UseFiles.FindAsync(id);
            if (useFile == null)
            {
                return NotFound();
            }
            return View(useFile);
        }

        // POST: UseFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path,ParentReplyId")] UseFile useFile)
        {
            if (id != useFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(useFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseFileExists(useFile.Id))
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
            return View(useFile);
        }

        // GET: UseFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UseFiles == null)
            {
                return NotFound();
            }

            var useFile = await _context.UseFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useFile == null)
            {
                return NotFound();
            }

            return View(useFile);
        }

        // POST: UseFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UseFiles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UseFile'  is null.");
            }
            var useFile = await _context.UseFiles.FindAsync(id);
            if (useFile != null)
            {
                _context.UseFiles.Remove(useFile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseFileExists(int id)
        {
          return (_context.UseFiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
