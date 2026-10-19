using System;
using System.Collections.Generic;
using System.Linq;
using Students.DataAccessLayer;
using Students.Model;

namespace Students.BusinessLogic
{
   
    public class Student1
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
    }
    public class StudentService
    {
        private IRepository<Student> _repository;
        private bool _isUsingEf = true;

        public StudentService()
        {
            _repository = new EFRepository(); 
        }

       
        public void InitializeDatabase()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        }

        public void ToggleRepository()
        {
            _isUsingEf = !_isUsingEf;
          
            _repository = _isUsingEf
                ? new EFRepository()
                : new DapperRepository(DatabaseConfig.ConnectionString);
        }

        public string GetCurrentProviderName() => _isUsingEf ? "EF Core" : "Dapper";

        
        public List<Student1> GetAll()
        {
            return _repository.ReadAll().Select(s => new Student1
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Group = s.Group
            }).ToList();
        }

        public void Add(string firstName, string lastName, string group)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(group))
            {
                throw new ArgumentException("Все поля студента должны быть заполнены!");
            }

            var newStudent = new Student { FirstName = firstName, LastName = lastName, Group = group };
            _repository.Create(newStudent);
        }

        public void Remove(int id)
        {
            _repository.Delete(id);
        }
    }
}