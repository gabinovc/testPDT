using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestPDT.Models;

namespace TestPDT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        //Contexto de datos
        private Models.Test_DBContext _context;

        //Inicializar en el constructor por Depency Inyection
        public UploadController(Test_DBContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            string[] permittedExtensions = { ".csv" };

            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    return new ObjectResult(new
                    {
                        error = new
                        {
                            code = "SOME_API_ERROR",
                            message = "Invalid file extension",
                            id = "JK7L2390"
                        }
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (file.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + ext;////ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var filepath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    IEnumerable<Contacto> values = System.IO.File.ReadAllLines(fullPath)
                                            .Skip(1)
                                            .Select(v => ContactoFile.FromCsv(v))
                                            .ToList();

                    _context.Contactos.AddRange(values);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }

    class ContactoFile
    {
        public static Contacto FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Contacto contactoValues = new Contacto();
            contactoValues.Id = 0;
            contactoValues.FechaRegistro =Convert.ToDateTime(values[1]);
            contactoValues.Nombre = values[2];
            contactoValues.Direccion = values[3];
            contactoValues.Telefono = values[4];
            contactoValues.Curp = values[5];
            return contactoValues;
        }
    }
}
