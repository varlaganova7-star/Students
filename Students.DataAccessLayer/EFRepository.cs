using Students.Model;
using System.Collections.Generic;
using System.Linq;

namespace Students.DataAccessLayer
{
    public class EFRepository : IRepository<Student>
    {
        public void Create(Student item)
        {
            using var db = new AppDbContext();
            db.Students.Add(item);
            db.SaveChanges();
        }

        public List<Student> ReadAll()
        {
            using var db = new AppDbContext();
            return db.Students.ToList();
        }

        public Student ReadById(int id)
        {
            using var db = new AppDbContext();
            return db.Students.Find(id);
        }

        public void Delete(int id)
        {
            using var db = new AppDbContext();
            var item = db.Students.Find(id);
            if (item != null)
            {
                db.Students.Remove(item);
                db.SaveChanges();
            }
        }
    }
}