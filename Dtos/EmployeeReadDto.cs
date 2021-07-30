namespace Employees.Dtos
{

    /* Vi vil bruke disse DTO-ene til å representere dataene vi ønsker at klientene til vårt Web API skal motta.
    *  Man kan også kalle DTO-ene for ViewModel
    *  -----------------------------------------------
    *  
    *  I koden under vil du se at all informasjon som vi har i API-et ligger skrevet i koden.
    *  Om vi ønsker så kan vi gjemme sensitiv informasjom slik som "ID" så kan vi fjerne ID linjen fra DTO-en,
    *  på denne måten vil API-et sende alt utenom IDen til den ansatte.
    *  
    */
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmploymentDate { get; set; }
    }
}