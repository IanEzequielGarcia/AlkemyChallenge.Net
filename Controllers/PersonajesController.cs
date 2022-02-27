using APIDisney2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDisney2.Controllers
{
    [Authorize]
    public class PersonajesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonajesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/characters")]
        public IActionResult GetListado(string name,int age,int movies)
        {
            IEnumerable<Personaje> personajes = _context.Personaje;
            try
            {
                if (name == null && age == 0 && movies == 0)
                {
                    return View(personajes);
                }
                else if (movies != 0)
                {
                    IEnumerable<Pelicula> peliculas = _context.Pelicula;
                    try
                    {
                        peliculas = peliculas.Where(peliculas => peliculas.Id == movies);
                        if (personajes.Any(personajes => personajes.PeliculasId == peliculas.FirstOrDefault().Id))
                        {
                            personajes = personajes.Where(personajes => personajes.PeliculasId == peliculas.FirstOrDefault().Id);
                        }
                        return View(personajes);
                    }
                    catch (Exception)
                    {
                        return View();
                    }
                }
                else
                {
                    return View(personajes.Where(personajes => personajes.Nombre == name || personajes.Edad == age));
                }

            }
            catch (System.InvalidOperationException)
            {
                return View();
            }
        }

        // GET: api/Personajes/5
        [Route("/characters/{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IEnumerable<Personaje> personaje = _context.Personaje;
            try
            {
                return View(personaje.Where(personaje => personaje.Id == id));
            }
            catch (System.InvalidOperationException)
            {
                return View();
            }

        }
        [Route("/characters/edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var idEdit = _context.Personaje.Find(id);
            if (idEdit == null)
            {
                return NotFound();
            }
            return View(idEdit);
        }

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/characters/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Personaje obj)
        {
            if (ModelState.IsValid)
            {
                _context.Personaje.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Get");
            }
            return View(obj);
        }
        [Route("/characters/new")]
        public IActionResult Post()
        {
            return View();
        }

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/characters/new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(Personaje obj)
        {
            if (ModelState.IsValid)
            {
                _context.Personaje.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Get");
            }
            return View(obj);
        }
        [Route("/characters/delete/")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var idEdit = _context.Personaje.Find(id);
            if (idEdit == null)
            {
                return NotFound();
            }
            return View(idEdit);
        }

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/characters/delete/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Personaje.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Personaje.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Get");
        }
    }
}
