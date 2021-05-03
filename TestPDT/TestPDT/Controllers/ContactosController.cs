using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPDT.Models;

namespace TestPDT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        //Contexto de datos
        private Models.Test_DBContext _context;

        //Inicializar en el constructor por Depency Inyection
        public ContactosController(Test_DBContext context)
        {
            this._context = context;
        }
        
        /// <summary>
        /// Regresa una lista de contactos
        /// </summary>
        /// <returns>List<Contacto></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        [HttpGet("Busca/{criterio}")]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetBuscaContactos(string criterio)
        {
            return await _context.Contactos.Where( c=> c.Nombre.Contains(criterio)).ToListAsync();
        }

        /// <summary>
        /// Regresa el contacto según el parámetro Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contacto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
                return NotFound();
            return Ok(contacto);
        }

        /// <summary>
        /// Actualiza un contacto con los datos de la entidad recibida como parámetro y el id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contacto"></param>
        /// <returns>204 o 404</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return BadRequest();
            }

            _context.Entry(contacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.Contactos.Any(e => e.Id == id)))
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

        /// <summary>
        /// Agrega un contacto a la base de datos en la tabla Contactos
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns>contacto</returns>
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContacto(Contacto contacto)
        {
            contacto.FechaRegistro = DateTime.Now;
            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContacto", new { id = contacto.Id }, contacto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
