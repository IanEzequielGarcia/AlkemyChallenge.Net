using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDisney2.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIDisney2.Controllers
{
    [Route("[controller]")]
    public class GenerosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenerosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/genres")]
        public IActionResult Get()
        {
            IEnumerable<Genero> generos = _context.Genero;
            return View(generos);
        }

        // GET: api/Generos/5
        [Route("/genres/")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IEnumerable<Genero> personaje = _context.Genero;
            try
            {
                return View(personaje.Where(personaje => personaje.Id == id));
            }
            catch (System.InvalidOperationException)
            {
                return View();
            }

        }
        [Route("/genres/edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var idEdit = _context.Genero.Find(id);
            if (idEdit == null)
            {
                return NotFound();
            }
            return View(idEdit);
        }

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/genres/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genero obj)
        {
            if (ModelState.IsValid)
            {
                _context.Genero.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Get");
            }
            return View(obj);
        }
        [Route("/genres/new")]
        public IActionResult Post()
        {
            return View();
        }

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/genres/new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(Genero obj)
        {
            if (ModelState.IsValid)
            {
                _context.Genero.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Get");
            }
            return View(obj);
        }
        [Route("/genres/delete/")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var idEdit = _context.Genero.Find(id);
            if (idEdit == null)
            {
                return NotFound();
            }
            return View(idEdit);
        }

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/genres/delete/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Genero.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Genero.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Get");
        }
    }
}
