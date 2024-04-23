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
    public class TelefonosController : Controller
    {
        private readonly PersonDbContext _context;

        public TelefonosController(PersonDbContext context)
        {
            _context = context;
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {
            var personDbContext = _context.Telefonos.Include(t => t.DuenioNavigation);
            return View(await personDbContext.ToListAsync());
        }

        // GET: Telefonos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonos/Create
        public IActionResult Create()
        {
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc");
            return View();
        }

        // POST: Telefonos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telefono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // POST: Telefonos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Num))
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
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(string id)
        {
            return _context.Telefonos.Any(e => e.Num == id);
        }
    }

    // Se realizan los siguientes controladores para la documentación en Swagger y demostrar su funcionamiento.
    // Puesto a que Swagger no está diseñado para documentar controladores MVC que devuelven vistas.
    // Sin embargo, se usan los controladores MVC en la ejecución del proyecto.

    [ApiController]
    [Route("api/[controller]")]
    public class TelefonosApiController : ControllerBase
    {
        private readonly PersonDbContext _context;

        public TelefonosApiController(PersonDbContext context)
        {
            _context = context;
        }

        // GET: api/Telefonos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
            return await _context.Telefonos.Include(t => t.DuenioNavigation).ToListAsync();
        }

        // GET: api/Telefonos/5
        [HttpGet("{num}")]
        public async Task<ActionResult<Telefono>> GetTelefono(string num)
        {
            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == num);
            if (telefono == null)
            {
                return NotFound();
            }
            return telefono;
        }

        // POST: api/Telefonos
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTelefono", new { num = telefono.Num }, telefono);
        }

        // PUT: api/Telefonos/5
        [HttpPut("{num}")]
        public async Task<IActionResult> PutTelefono(string num, Telefono telefono)
        {
            if (num != telefono.Num)
            {
                return BadRequest();
            }
            _context.Entry(telefono).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(num))
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

        // DELETE: api/Telefonos/5
        [HttpDelete("{num}")]
        public async Task<IActionResult> DeleteTelefono(string num)
        {
            var telefono = await _context.Telefonos.FindAsync(num);
            if (telefono == null)
            {
                return NotFound();
            }
            _context.Telefonos.Remove(telefono);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TelefonoExists(string num)
        {
            return _context.Telefonos.Any(e => e.Num == num);
        }
    }
}
