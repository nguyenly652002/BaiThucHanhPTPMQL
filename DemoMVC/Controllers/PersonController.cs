using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using SQLitePCL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    { 
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _context.Person.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string TimKiem)
        {
          return View( await _context.Person.Where(m => m.FullName.Contains(TimKiem)) .ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if(ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if(id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FindAsync(id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if(id != person.PersonId)
            {
                return NotFound();
            }


            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!PersonExists(person.PersonId))
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

            return View(person);
        }
         public async Task<IActionResult> Delete(string id)
        {
            if(id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FirstOrDefaultAsync(m=>m.PersonId ==id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person' is null");
            }
            var person = await _context.Person.FindAsync(id);
            if(person != null)
            {
                _context.Person.Remove(person);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId ==id)).GetValueOrDefault();
        }
    }

  
}
