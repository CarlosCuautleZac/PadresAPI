using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PadresAPI.DTOs;
using PadresAPI.Models;
using PadresAPI.Repositories;

namespace PadresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        Repository<AlumnoTutor> repository;
        Repository<Alumno> repositoryAlumno;
        Repository<DocenteAlumno> repositoryDocenteAlumno;

        public AlumnoController(Sistem21PrimariaContext context)
        {
            repository  = new(context);
            repositoryAlumno = new(context);
            repositoryDocenteAlumno = new(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetByTutor(int id)
        {

            var alumnos = repository.Get().Include(x=>x.IdAlumnoNavigation).Include(x=>x.IdTutorNavigation).
                Include(x=>x.IdAlumnoNavigation.IdGrupoNavigation).
               Where(x=>x.IdTutor==id).ToList();

            if (alumnos == null)
                return NotFound();

            List<AlumnoDTO> alumnoDTOs = alumnos.Select(x => new AlumnoDTO()
            {
                Id = x.Id,
                Grado = x.IdAlumnoNavigation.IdGrupoNavigation.Grado,
                Seccion = x.IdAlumnoNavigation.IdGrupoNavigation.Seccion,
                Asingaturas = repositoryDocenteAlumno.Get().Include(x => x.IdAlumnoNavigation).Include(x => x.IdDocenteNavigation).
                Include(x => x.IdPeriodoNavigation).Where(m=>m.IdAlumno==x.IdAlumno).Select(y => new AsingaturaDTO()
                {
                    
                }
                
                }).ToList()
            };

            return Ok();
        }

        
    }
}
