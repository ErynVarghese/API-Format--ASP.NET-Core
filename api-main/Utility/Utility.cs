namespace API_main.Utility
{
    public static class ConnectionString
    {
        private static string _connectionString = "Data Source=ELS6;Integrated Security=true;Initial Catalog=CCM;TrustServerCertificate=True;";                                   
        public static string CName => _connectionString;
    }
}


