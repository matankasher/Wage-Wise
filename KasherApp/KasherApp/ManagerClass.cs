namespace KasherApp
{

    // using Early Singleton
    public sealed class ManagerAdmin
    {
        private string id;
        private string password;
        private static ManagerAdmin earlySingleton = new ManagerAdmin("admin" , "1234");

        static ManagerAdmin()
        {
           
        }

        ManagerAdmin(string id , string password)
        {
           this.id = id;
           this.password = password;
        }
        public static ManagerAdmin getInstance()
        {
            return earlySingleton;
        }
        public string Password { get { return password; } }
        public string Id { get { return id; } }
    }
}
