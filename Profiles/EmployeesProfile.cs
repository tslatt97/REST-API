using AutoMapper;
using Employees.Dtos;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Profiles
{
    public class EmployeesProfile : Profile
    {

        // Her utfører vi kartlegging (Mapping) av egenskapene til Employee objektene over til en DTO (Data Transfer Object)
        public EmployeesProfile()
        {
            // Kilde  -> Mål
            // Employee -> ReadDto

            // Kartlegger (Mapper) Employee objektet til Employee DTO-en
            CreateMap<Employee, EmployeeReadDto>();

            // Kartlegger Employee DTO-en til Employee objektet
            CreateMap<EmployeeCreateDto, Employee>();

            // Kartlegger EmployeeUpdateDTO-en til Employee objektet
            CreateMap<EmployeeUpdateDto, Employee>();

            // Kartlegger Employee objektet til EmployeeUpdateDTO-en
            CreateMap<Employee, EmployeeUpdateDto>();
        }
    }
}
