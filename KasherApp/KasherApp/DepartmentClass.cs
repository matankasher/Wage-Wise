namespace KasherApp
{
    public class Department
    {
        private string name;
        private int wage;
        public Department(string name, int wage)
        {
            this.name = name;
            this.wage = wage;
        }
        public int Wage { get; set; }
        public string Name { get { return name; } set { name = value; } }
       

    }
}
