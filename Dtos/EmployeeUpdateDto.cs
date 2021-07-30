using System.ComponentModel.DataAnnotations;

namespace Employees.Dtos
{

    /* Vi vil bruke disse DTO-ene til å representere dataene vi ønsker at klientene til vårt Web API skal motta.
    *  Man kan også kalle DTO-ene for ViewModel
    */
    public class EmployeeUpdateDto
    {

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string EmploymentDate { get; set; }
    }
}