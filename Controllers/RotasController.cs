using Microsoft.AspNetCore.Mvc;
using RotaViagem.Business;
using RotaViagem.Models;
using RotaViagem.Service;
using System.Collections.Generic;

namespace RotaViagem.Controllers
{
    [Route("Rotas")]
    public class RotasController : Controller
    {
        private readonly IRotaService service;

        public RotasController(IRotaService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Rota> rotas = service.Get();

            return Ok(rotas);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Rota rota = service.Get(id);

            if (rota == null)
            {
                return NotFound();
            }

            return Ok(rota);
        }


        [HttpGet("{origem}/{destino}")]
        public string Get(string origem, string destino)
        {
            List<Rota> rotas = service.Get();

            return RotaBusiness.CalcularMelhorRota(rotas, origem, destino);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Rota rota)
        {
            if (rota == null)
            {
                return BadRequest();
            }

            service.Add(rota);

            return CreatedAtRoute("Get", new { id = rota.Id }, rota);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Rota rota)
        {
            if (rota == null || id != rota.Id)
            {
                return BadRequest();
            }

            if (service.Get(id) == null)
            {
                return NotFound();
            }

            service.Update(rota);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Rota rota = service.Get(id);

            if (rota == null)
            {
                return NotFound();
            }

            service.Delete(id);

            return NoContent();
        }
    }
}
