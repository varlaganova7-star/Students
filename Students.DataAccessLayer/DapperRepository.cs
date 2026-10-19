using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Students.Model;

namespace Students.DataAccessLayer
{
    public class DapperRepository : IRepository<Student>
    {
        private readonly string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public void Create(Student item)
        {
            using var db = CreateConnection();
         
            db.Execute(@"INSERT INTO Students (FirstName, LastName, [Group]) 
                         VALUES (@FirstName, @LastName, @Group)", item);
        }

        public List<Student> ReadAll()
        {
            using var db = CreateConnection();
            return db.Query<Student>("SELECT Id, FirstName, LastName, [Group] FROM Students").ToList();
        }

        public Student ReadById(int id)
        {
            using var db = CreateConnection();
            return db.QueryFirstOrDefault<Student>("SELECT Id, FirstName, LastName, [Group] FROM Students WHERE Id = @Id", new { Id = id });
        }

        public void Delete(int id)
        {
            using var db = CreateConnection();
            db.Execute("DELETE FROM Students WHERE Id = @Id", new { Id = id });
        }
    }
}