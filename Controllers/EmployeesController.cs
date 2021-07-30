using AutoMapper;
using Employees.Data;
using Employees.Dtos;
using Employees.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Controllers
{

    // Viser hvordan man kommer til de nødvendige ressursene og API endpoints inni controlleren
    // Altså en "base route" til vår controller

    //api/employees
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeesRepo _repository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET api/employees
        [HttpGet]
        public ActionResult <IEnumerable<EmployeeReadDto>> GetAllEmployees()
        {
            var EmployeeItems = _repository.GetAllEmployees();

            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(EmployeeItems));
        }

        // GET api/employees/{id}
        [HttpGet("{id}", Name ="GetEmployeeById")]
        public ActionResult <EmployeeReadDto> getEmployeeById(int id)
        {
            var employeeItem = _repository.GetEmployeeById(id);
            if(employeeItem != null)
            {
                return Ok(_mapper.Map<EmployeeReadDto>(employeeItem));
            }
            return NotFound();
        }

        // POST api/employees
        [HttpPost]
        public ActionResult<EmployeeReadDto> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employeeModel = _mapper.Map<Employee>(employeeCreateDto);
            _repository.CreateEmployee(employeeModel);
            _repository.SaveChanges(); // Lagrer endringene mot databasen, uten lagringen vil all informasjon bli sent men ikke lagret.

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);

            return CreatedAtRoute(nameof(getEmployeeById), new {Id = employeeReadDto.Id }, employeeReadDto);

        }

        // PUT api/employees/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            // Ber om employee ID for å så returnere informasjon som har blitt bedt om fra API-et
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }
            // Tar data fra employee update DTO, som er sendt fra en klient, og legger dem inn i employee modellen.
            // bruker: EmployeeProfile.cs

            // Bruker eksisterende kilder, EmployeeUpdateDto (som vi kartlegger data fra), og vi vil kartlegge til vår ansattmodell fra repositorien.
            _mapper.Map(employeeUpdateDto, employeeModelFromRepo);


            // Ikke nødvendig, men god praksis
            _repository.UpdateEmployee(employeeModelFromRepo);

            // Lagrer endringene som har blitt gjort mot databasen
            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/employees/{id}
        [HttpPatch("{id}")]
        // Henter et patch dokument fra API-etterspørselen

        public ActionResult PartialEmployeeUpdate(int id, JsonPatchDocument<EmployeeUpdateDto> patchDoc)
        {

            // Sjekker om ressursen eksisterer og er oppdatert fra repositoriet.
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }

            // Lager en tom update employee DTO, bruker data fra repository modellen og gjennomfører endringen
            var employeeToPatch = _mapper.Map<EmployeeUpdateDto>(employeeModelFromRepo);
            patchDoc.ApplyTo(employeeToPatch, ModelState); // ModelState sjekker om alle valideringene er gyldige
            if (!TryValidateModel(employeeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(employeeToPatch, employeeModelFromRepo);

            // Ikke nødvendig, men god praksis
            _repository.UpdateEmployee(employeeModelFromRepo);

            // Gjennomfører endringene som er gjort tilbake til databasen
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/employees/{id}
        [HttpDelete]
        public ActionResult DeleteEmployees(int id)
        {
            // Sjekker om ressursen eksisterer og er oppdatert fra repositorien (som er en modell)
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteEmployee(employeeModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
         
    }
}
