using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaMVC00.Models;
using webApiPractica.Models;

namespace PracticaMVC00.Controllers
{
    public class tipo_equipoController : Controller
    {
        private readonly EquiposDbContext _context;

        public tipo_equipoController(EquiposDbContext context)
        {
            _context = context;
        }

        // GET: tipo_equipo
        public async Task<IActionResult> Index()
        {
            return View(await _context.tipo_equipo.ToListAsync());
        }

        // GET: tipo_equipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_equipo = await _context.tipo_equipo
                .FirstOrDefaultAsync(m => m.id_tipo_equipo == id);
            if (tipo_equipo == null)
            {
                return NotFound();
            }

            return View(tipo_equipo);
        }

        // GET: tipo_equipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tipo_equipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_tipo_equipo,descripcion,estado")] tipo_equipo tipo_equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo_equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo_equipo);
        }

        // GET: tipo_equipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_equipo = await _context.tipo_equipo.FindAsync(id);
            if (tipo_equipo == null)
            {
                return NotFound();
            }
            return View(tipo_equipo);
        }

        // POST: tipo_equipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_tipo_equipo,descripcion,estado")] tipo_equipo tipo_equipo)
        {
            if (id != tipo_equipo.id_tipo_equipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo_equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tipo_equipoExists(tipo_equipo.id_tipo_equipo))
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
            return View(tipo_equipo);
        }

        // GET: tipo_equipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_equipo = await _context.tipo_equipo
                .FirstOrDefaultAsync(m => m.id_tipo_equipo == id);
            if (tipo_equipo == null)
            {
                return NotFound();
            }

            return View(tipo_equipo);
        }

        // POST: tipo_equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo_equipo = await _context.tipo_equipo.FindAsync(id);
            if (tipo_equipo != null)
            {
                _context.tipo_equipo.Remove(tipo_equipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tipo_equipoExists(int id)
        {
            return _context.tipo_equipo.Any(e => e.id_tipo_equipo == id);
        }
    }
}
