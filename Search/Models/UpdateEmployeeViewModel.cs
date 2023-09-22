namespace Search.Models
{
    public class UpdateEmployeeViewModel
    {

        public Guid Id { get; set; } //Guid -- Unique Identifier

        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DOB { get; set; }
        public string Department { get; set; }
    }
}

