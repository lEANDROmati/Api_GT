using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_GT.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api_GT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        public readonly TurnosContext _dbcontext;

        public PacienteController(TurnosContext _context) {
            _dbcontext = _context;

        }


        [HttpGet]
        [Route("Lista")]
        public IActionResult lista()
        {
            List<Paciente> lista = new List<Paciente>();

            try
            {
                lista = _dbcontext.Pacientes.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idPaciente:int}")]
        public IActionResult Obtener(int idPaciente)
        {
            Paciente oPaciente = _dbcontext.Pacientes.Find(idPaciente);

            if (oPaciente == null)
            {
                return BadRequest("paciente no encontrado");
            }

            try
            {
                oPaciente = _dbcontext.Pacientes.Where(p => p.Id == idPaciente).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oPaciente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = oPaciente });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Paciente objeto)
        {
            try
            {
                _dbcontext.Pacientes.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Paciente objeto)
        {
            Paciente oPaciente = _dbcontext.Pacientes.Find(objeto.Id);

            if (oPaciente == null)
            {
                return BadRequest("paciente no encontrado");
            }

            try
            {
                oPaciente.Nombre = objeto.Nombre is null ? oPaciente.Nombre : objeto.Nombre;
                oPaciente.Apellido = objeto.Apellido is null ? oPaciente.Apellido : objeto.Apellido;
                oPaciente.Documento = objeto.Documento is null ? oPaciente.Documento : objeto.Documento;
                oPaciente.Telefono = objeto.Telefono is null ? oPaciente.Telefono : objeto.Telefono;

                _dbcontext.Update(oPaciente);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminar/{idPaciente:int}")]
        public IActionResult Eliminar(int idPaciente)
        {
            Paciente oPaciente = _dbcontext.Pacientes.Find(idPaciente);

            if (oPaciente == null)
            {
                return BadRequest("paciente no encontrado");
            }
            try
            {

                _dbcontext.Remove(oPaciente);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}
