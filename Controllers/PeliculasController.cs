using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using APIDisney2.Models;

namespace APIDisney2.Controllers
{
    [Authorize]
    public class PeliculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeliculasController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/movies")]
        public IActionResult GetListado(string name, int genre, string order)
        {
            IEnumerable<Pelicula> peliculas = _context.Pelicula;
            try
            {
                if (name == null && order == null && genre==0)
                {
                    return View(peliculas);
                }
                else if (genre != 0)
                {
                    IEnumerable<Genero> genero = _context.Genero;
                    try
                    {
                        genero = genero.Where(genero => genero.Id == genre);
                        ViewData["Genero"] = genero;
                    }
                    catch (Exception)
                    {
                        return View();
                    }
                }
                else if(order != null)
                {
                    if(order=="ASC")
                    {
                        peliculas = peliculas.OrderBy(peli => peli.FechaEstreno);
                    }
                    else if(order == "DESC")
                    {
                        peliculas = peliculas.OrderByDescending(peli => peli.FechaEstreno);
                    }
                }
                else
                {
                    return View(peliculas.Where(peliculas => peliculas.Titulo == name));
                }
                return View(peliculas);
            }
            catch (System.InvalidOperationException)
            {
                return View();
            }
        }
        // GET: api/peliculas/5
        [Route("/movies/")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IEnumerable<Pelicula> pelicula = _context.Pelicula;
            try
            {
                return View(pelicula.Where(pelicula => pelicula.Id == id));
            }
            catch (System.InvalidOperationException)
            {
                return View();
            }
        }
        [Route("/movies/edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var idEdit = _context.Pelicula.Find(id);
            if (idEdit == null)
            {
                return NotFound();
            }
            return View(idEdit);
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/movies/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pelicula obj)
        {
            if (ModelState.IsValid)
            {
                _context.Pelicula.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Get");
            }
            return View(obj);
        }
        [Route("/movies/new")]
        public IActionResult Post()
        {
            return View();
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/movies/new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(Pelicula obj)
        {
            if (ModelState.IsValid)
            {
                _context.Pelicula.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Get");
            }
            return View(obj);
        }
        [Route("/movies/delete/")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var idEdit = _context.Pelicula.Find(id);
            if (idEdit == null)
            {
                return NotFound();
            }
            return View(idEdit);
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/movies/delete/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Pelicula.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Pelicula.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Get");
        }
    }
}

