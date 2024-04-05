using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaMVC00.Models;

namespace PracticaMVC00.Controllers
{
    public class equiposController : Controller
    {
        private readonly EquiposDbContext _context;

        public equiposController(EquiposDbContext context)
        {
            _context = context;
        }

        // GET: equipos
        public IActionResult Index() 
        { 
            var listaDeMarcas = (from m in _context.marcas select m).ToList();
            ViewData["listadoMarcas"] = new SelectList(listaDeMarcas, "id_marcas", "nombre_marca");
            
            var listadoDeEquipos = (from e in _context.equipos
                                    join m in _context.marcas on e.marca_id equals m.id_marcas
                                    select new
                                    {
                                        nombre = e.nombre,
                                        descripcion = e.descripcion,
                                        marca_id = e.marca_id,
                                        marca_nombre = m.nombre_marca
                                    }).ToList();

            ViewData["ListadoEquipo"] = listadoDeEquipos;
            return View(); 
        }


        // GET: equipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipos = await _context.equipos
                .FirstOrDefaultAsync(m => m.id_equipos == id);
            if (equipos == null)
            {
                return NotFound();
            }

            return View(equipos);
        }

        // GET: equipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_equipos,nombre,descripcion,tipo_equipo_id,marca_id,modelo,anio_compra,costo,vida_util,estado_equipo_id,estado")] equipos equipos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipos);
        }

        // GET: equipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipos = await _context.equipos.FindAsync(id);
            if (equipos == null)
            {
                return NotFound();
            }
            return View(equipos);
        }

        // POST: equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_equipos,nombre,descripcion,tipo_equipo_id,marca_id,modelo,anio_compra,costo,vida_util,estado_equipo_id,estado")] equipos equipos)
        {
            if (id != equipos.id_equipos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!equiposExists(equipos.id_equipos))
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
            return View(equipos);
        }

        // GET: equipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipos = await _context.equipos
                .FirstOrDefaultAsync(m => m.id_equipos == id);
            if (equipos == null)
            {
                return NotFound();
            }

            return View(equipos);
        }

        // POST: equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipos = await _context.equipos.FindAsync(id);
            if (equipos != null)
            {
                _context.equipos.Remove(equipos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool equiposExists(int id)
        {
            return _context.equipos.Any(e => e.id_equipos == id);
        }

    }
}
