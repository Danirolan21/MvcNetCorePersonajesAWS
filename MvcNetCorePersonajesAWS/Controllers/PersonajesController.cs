using Microsoft.AspNetCore.Mvc;
using MvcNetCorePersonajesAWS.Models;
using MvcNetCorePersonajesAWS.Services;

namespace MvcNetCorePersonajesAWS.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiPersonajes service;
        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajes();
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.CreatePersonajesAsync(personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.service.GetPersonaje(id);
            return View(personaje);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.GetPersonaje(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonajesAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajesAsync(id);
            return RedirectToAction("Index");
        }
    }
}
