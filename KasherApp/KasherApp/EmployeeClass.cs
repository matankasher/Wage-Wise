namespace KasherApp
{
    public class Employee
    {
        private int employeeID;
        private string password;
        private string firstName;  
        private string lastName;
        private Department department;
        public Employee(int id, string password, string firstName, string lastName)
        {
            employeeID = id;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set;}
        

    }
}
