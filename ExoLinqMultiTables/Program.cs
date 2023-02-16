using ExoLinqMultiTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExoLinqMultiTables
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var students = new List<Student>()
            {
                new Student() {Id=1, Name = "A 1", AddessId=1},
                new Student() {Id=2, Name = "A 2", AddessId=2},
                new Student() {Id=3, Name = "A 3", AddessId=2},
                new Student() {Id=4, Name = "A 4", AddessId=3}
            };

            var addresses = new List<Address>()
            {
                new Address() {Id=1, AddressLine="Address 1"},
                new Address() {Id=2, AddressLine="Address 2"},
                new Address() {Id=3, AddressLine="Address 3"},
                new Address() {Id=4, AddressLine="Address 4"}
            };

            var marks = new List<Marks>()
            {
                new Marks() {Id=1, StudentId=1, TMarks=20},
                new Marks() {Id=2, StudentId=2, TMarks=40},
                new Marks() {Id=3, StudentId=2, TMarks=60},
                new Marks() {Id=4, StudentId=4, TMarks=90}         
            };

            var comments = new List<Comment>()
            {
                new Comment() {Id=1, NameComment="Comment 1", MarksId=1},
                new Comment() {Id=2, NameComment="Comment 2", MarksId=2},
                new Comment() {Id=3, NameComment="Comment 3", MarksId=3},
                new Comment() {Id=4, NameComment="Comment 4", MarksId=4}
            };


            var ms = students.Join(addresses, 
                                    std => std.AddessId, 
                                    address => address.Id,
                                    (std,address) => new {std, address})
                                    .Select(x => new
                                    {
                                        StudentName = x.std.Name,
                                        Line = x.address.AddressLine
                                    }).ToList();

            var qs = (from student in students
                      join address in addresses
                      on student.AddessId equals address.Id
                      join mark in marks
                      on student.Id equals mark.StudentId
                      select new
                      {
                          StudentName = student.Name,
                          Line = address.AddressLine,
                          TotalMarks = mark.TMarks
                      }).ToList();


            var qt = (from joined in (
                     from student in students
                     join mark in marks
                     on student.Id equals mark.StudentId
                     select new { student, mark }
                     )
                     join comment in comments
                     on joined.mark.Id equals comment.MarksId
                     /*where comment.NameComment == "Comment 4"
                     orderby comment.Id*/
                     select new { student = joined.student.Name, Mark = joined.mark.TMarks, Comment = comment.NameComment }).ToList();

            var liste = students
                        .Join(
                            marks,
                            student => student.Id,
                            mark => mark.StudentId,
                            (student, mark) => new
                            {
                                Student = student,
                                Mark = mark,
                            }
                            ).Select(joined => new { stud = joined.Student, mark2 = joined.Mark})
                            .Join(comments,
                            joined => joined.mark2.Id,
                            comment => comment.MarksId,
                            (joined ,comment) => new
                            {
                                studentN = joined.stud.Name,
                                markN = joined.mark2.TMarks,
                                commentN = comment.NameComment
                            }
                            ).ToList();

            foreach (var item in ms)
            {
                Console.WriteLine(item);
            }

            foreach (var item in qs)
            {
                Console.WriteLine(item);
            }

            foreach (var item in qt)
            {
                Console.WriteLine(item);
            }

            foreach (var item in liste)
            {
                Console.WriteLine(item);
            }

        }
    }
}

/*** output
{ StudentName = A 1, Line = Address 1 }
{ StudentName = A 2, Line = Address 2 }
{ StudentName = A 3, Line = Address 2 }
{ StudentName = A 4, Line = Address 3 }
{ StudentName = A 1, Line = Address 1, TotalMarks = 20 }
{ StudentName = A 2, Line = Address 2, TotalMarks = 40 }
{ StudentName = A 2, Line = Address 2, TotalMarks = 60 }
{ StudentName = A 4, Line = Address 3, TotalMarks = 90 }
{ student = A 1, Mark = 20, Comment = Comment 1 }
{ student = A 2, Mark = 40, Comment = Comment 2 }
{ student = A 2, Mark = 60, Comment = Comment 3 }
{ student = A 4, Mark = 90, Comment = Comment 4 }
{ studentN = A 1, markN = 20, commentN = Comment 1 }
{ studentN = A 2, markN = 40, commentN = Comment 2 }
{ studentN = A 2, markN = 60, commentN = Comment 3 }
{ studentN = A 4, markN = 90, commentN = Comment 4 }
 */
