﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        // GET: api/<AlumnoController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Semestre = new ML.Semestre();
            ML.Result result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
            //return new string[] { "Leonardo", "Isaac","Jesus" };
        }

        // GET api/<AlumnoController>/5
        [HttpGet("GetById/{idAlumno}")]
        public IActionResult Get(int idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Semestre = new ML.Semestre();
            ML.Result result = BL.Alumno.GetById(idAlumno);

            if (result.Correct)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AlumnoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
