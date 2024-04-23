using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class ProfesionsController : Controller
    {
        private readonly PersonDbContext _context;

        public ProfesionsController(PersonDbContext context)
        {
            _context = context;
        }

        // GET: Profesions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesions.ToListAsync());
        }

        // GET: Profesions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: Profesions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: Profesions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionExists(profesion.Id))
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
            return View(profesion);
        }

        // GET: Profesions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

    // POST: Profesions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var profesion = await _context.Profesions.FindAsync(id);
      if (profesion == null)
      {
        return NotFound();
      }

      // Eliminar los estudios asociados a la profesión
      var estudios = _context.Estudios.Where(e => e.IdProf == id);
      _context.Estudios.RemoveRange(estudios);

      // Eliminar la profesión
      _context.Profesions.Remove(profesion);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }

    private bool ProfesionExists(int id)
        {
            return _context.Profesions.Any(e => e.Id == id);
        }
    }

    // Se realizan los siguientes controladores para la documentación en Swagger y demostrar su funcionamiento.
    // Puesto a que Swagger no está diseñado para documentar controladores MVC que devuelven vistas.
    // Sin embargo, se usan los controladores MVC en la ejecución del proyecto.

    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionsApiController : ControllerBase
    {
        private readonly PersonDbContext _context;

        public ProfesionsApiController(PersonDbContext context)
        {
            _context = context;
        }

        // GET: api/Profesions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetProfesions()
        {
            return await _context.Profesions.ToListAsync();
        }

        // GET: api/Profesions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> GetProfesion(int id)
        {
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return profesion;
        }

        // POST: api/Profesions
        [HttpPost]
        public async Task<ActionResult<Profesion>> PostProfesion(Profesion profesion)
        {
            _context.Profesions.Add(profesion);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProfesion", new { id = profesion.Id }, profesion);
        }

        // PUT: api/Profesions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesion(int id, Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return BadRequest();
            }
            _context.Entry(profesion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Profesions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesion(int id)
        {
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            _context.Profesions.Remove(profesion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProfesionExists(int id)
        {
            return _context.Profesions.Any(e => e.Id == id);
        }
    }
}
