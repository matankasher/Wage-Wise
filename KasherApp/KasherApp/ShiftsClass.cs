namespace KasherApp
{
    public class Shifts
    {
        private int employeeID;
        private DateOnly date;
        private double hours; 
        public Shifts(int employeeID, DateOnly date, double hours)
        {
            this.employeeID = employeeID;
            this.date = date;
            this.hours = hours;
        }
        public int EmployeeID { get { return employeeID; } }
        public DateOnly Date { get { return date; } }
        public double Hours { get { return hours; } }
    }
}
