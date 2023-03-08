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
        Repository<DocenteAsignatura> repositoryDocenteAsignatura;
        Repository<Asignatura> repositoryAsignatura;
        Repository<Calificacion> repositoryCalificacion;
        Repository<Periodo> repositoryPeriodo;

        public AlumnoController(Sistem21PrimariaContext context)
        {
            repository = new(context);
            repositoryAlumno = new(context);
            repositoryDocenteAlumno = new(context);
            repositoryDocenteAsignatura = new(context);
            repositoryAsignatura = new(context);
            repositoryCalificacion = new(context);
            repositoryPeriodo = new(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("kardex/{idalumno:int}")]
        public IActionResult GetKardex(int idalumno)
        {
            var alumno = repositoryAlumno.Get(idalumno);
            if (alumno == null)
                return NotFound();



            return Ok();
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetAlumnosByTutor(int id)
        {

            var alumnos = repository.Get().Include(x => x.IdAlumnoNavigation).Include(x => x.IdTutorNavigation).
                Include(x => x.IdAlumnoNavigation.IdGrupoNavigation).
               Where(x => x.IdTutor == id).ToList();

            if (alumnos == null)
                return NotFound();

            List<AlumnoDTO> alumnoDTOs = alumnos.Select(x => new AlumnoDTO()
            {
                Id = x.IdAlumno,
                Nombre = x.IdAlumnoNavigation.Nombre,
                Grado = x.IdAlumnoNavigation.IdGrupoNavigation.Grado,
                Seccion = x.IdAlumnoNavigation.IdGrupoNavigation.Seccion,
                Asingaturas = GetAsignaturas(x.IdAlumno),
                Calificaciones = GetCalificaciones(x.IdAlumno),
            }).ToList();

            return Ok(alumnoDTOs);
        }

        private List<CalificacionDTO> GetCalificaciones(int idAlumno)
        {
            List<CalificacionDTO> calificaciones = new();
            int maxperiodo = repositoryPeriodo.Get().Max(x => x.Id);

            var c = repositoryCalificacion.Get().Include(x => x.IdAsignaturaNavigation).Include(x => x.IdDocenteNavigation)
                .Include(x => x.IdAlumnoNavigation).Include(x=>x.IdPeriodoNavigation).Where(x => x.IdAlumno == idAlumno && x.IdPeriodo == maxperiodo).ToList();

            if (c.Count==0)
                return new List<CalificacionDTO>();

            var calificacionesalumno = c.Select(x => new CalificacionDTO()
            {
                Año = x.IdPeriodoNavigation.Año,
                Calificacion = x.Calificacion1,
                NombreAsignatura = x.IdAsignaturaNavigation.Nombre,
                Unidad = x.Unidad
            }).ToList();

            calificaciones.AddRange(calificacionesalumno);
            return calificaciones;

        }

        private List<AsingaturaDTO> GetAsignaturas(int idalumno)
        {
            var docentes = repositoryDocenteAlumno.Get().Include(x => x.IdDocenteNavigation).Include(x => x.IdDocenteNavigation)
                .Include(x => x.IdPeriodoNavigation).Where(x => x.IdAlumno == idalumno).Select(x => x.IdDocenteNavigation).ToList();

            if (docentes.Count == 0)
                return new List<AsingaturaDTO>();

            //Dos caminos
            //Si es docente con el tipo 1, tenemos que ir a la tabla de aisgnaturas y tomar todas aquellas con tipo 1

            //si es docente con el tipo 2, ir a buscar la aasingatura
            List<AsingaturaDTO> asingaturas = new();

            foreach (var docente in docentes)
            {
                if (docente.TipoDocente == 1)
                {
                    var asignaturasalumno = repositoryAsignatura.Get().Where(x => x.TipoAsignatura == 1).Select(m => new AsingaturaDTO()
                    {
                        NombreAsignatura = m.Nombre,
                        NombreDocente = docente.Nombre
                    }).ToList();
                    asingaturas.AddRange(asignaturasalumno);
                }
                else
                {
                    var asignatura = repositoryDocenteAsignatura.Get().
                        Include(x => x.IdAsignaturaNavigation).Where(x => x.IdDocente == docente.Id).Select(m => new AsingaturaDTO()
                        {
                            NombreAsignatura = m.IdAsignaturaNavigation.Nombre,
                            NombreDocente = docente.Nombre
                        }).FirstOrDefault();

                    if (asignatura != null)
                        asingaturas.Add(asignatura);
                }
            }


            if (asingaturas != null)
                return asingaturas;

            else
                return new List<AsingaturaDTO>();

        }


    }
}
