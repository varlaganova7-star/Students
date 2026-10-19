namespace Students.DataAccessLayer
{
    public static class DatabaseConfig
    {
        // ВНИМАНИЕ: Проверьте, что этот путь совпадает с реальным путем к вашему файлу Database1.mdf!
        public static readonly string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\1\source\repos\Students\Students.DataAccessLayer\Database1.mdf;Integrated Security=True;TrustServerCertificate=True;";
    }
}