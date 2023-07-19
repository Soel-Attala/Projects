using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;



namespace LINQ_Snippet
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
               "VW Golf",
               "VW California",
               "Audi A3",
               "Audi A5",
               "Fiat Punto",
               "Seat Ibiza",
               "Seat Leon"
            };

            //1. haremos un select de toda la lista
            var carlist = from car in cars select car;
            foreach (var car in carlist)
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE
            var audiList = from car in cars
                           where car.Contains("Audi")
                           select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }


        //Number Examples
        static public void NumbersLinQ()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //each number multiplied by 3
            //take all numbers but not 9
            //Order numbers by asending value

            var processedNumberList = numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "b",
                "y",
                "cj",
                "f",
                "c"
            };

            //1.First of all elements
            var first = textList.First();

            //2.First element with "c" character
            var cText = textList.First(text => text.Equals("c"));

            //3.First element that contains J character
            var jText = textList.First(text => text.Contains("j"));

            //4. First element that cointains z or default value
            var firstOrDefault = textList.FirstOrDefault(text => text.Contains("z"));

            //5. Last element with z or default
            var lastOrDefault = textList.LastOrDefault(text => text.Contains("z"));

            //6. Single value
            var uniqueText = textList.Single();
            var singleOrDefault = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            //Obtain 4 and 8

            var myEvenNumber = evenNumbers.Except(otherEvenNumbers);
        }


        static public void MultipleSelect()
        {
            string[] myOpinions =
            {
                "Opinion 1: text 1",
                "Opinion 2: text 2",
                "Opinion 3: text 3",
                "Opinion 4: text 4",
                "Opinion 5: text 5"
            };

            var myOpinionSelection = myOpinions.SelectMany(Opinion => Opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id=1,
                            Name = "Martin",
                            Email = "Martin123@gmail.com",
                            Salary = 1000
                        },
                        new Employee()
                        {
                            Id=2,
                            Name = "Juanjo",
                            Email = "Juanjo123@gmail.com",
                            Salary = 1100
                        },
                        new Employee()
                        {
                            Id=3,
                            Name = "Jorge",
                            Email = "Jorge123@gmail.com",
                            Salary = 1500
                        },
                        new Employee()
                        {
                            Id=4,
                            Name = "Maria",
                            Email = "Maria123@gmail.com",
                            Salary = 1200
                        },
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees =new[]
                    {
                        new Employee()
                        {
                            Id=5,
                            Name = "Marisa",
                            Email = "Marisa123@gmail.com",
                            Salary = 1500
                        },
                        new Employee()
                        {
                            Id=6,
                            Name = "Juana",
                            Email = "juanita456@gmail.com",
                            Salary = 1700
                        },
                        new Employee()
                        {
                            Id=7,
                            Name = "Nestor",
                            Email = "Nestor789@gmail.com",
                            Salary = 1400
                        },
                        new Employee()
                        {
                            Id=8,
                            Name = "Alberto",
                            Email = "AlbertitoInflacion@gmail.com",
                            Salary = 4000
                        },
                    }
                }
            };

            //Obtain all employee of all enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //All enterprises at least has an employee with more than 1000 of salary
            bool hasEmployeeWithSalaryMoreThan1000 =
                enterprises.Any(enterprise => enterprise.Employees.Any(employee => employee.Salary >= 1000));

        }



        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c", "d", "m", "h" };
            var secondList = new List<string>() { "b", "d", "c", "z", "x", "y" };

            //INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                                secondList,
                                element => element,
                                secondElement => secondElement,
                                (element, secondElement) => new { element, secondElement }
                                );

            //OUTER JOIN - LEFT

            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where temporalElement != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };


            //OUTER JOIN RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where temporalElement != temporalElement
                                 select new { Element = secondElement };


            //union
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };


            var skipTwoFirstValues = myList.Skip(2);
            var skipTwoLastValues = myList.SkipLast(2);
            var skipWhile = myList.SkipWhile(num => num < 4);


            //TAKE
            var takeFirstTwoValues = myList.Take(2);
            var takeLastTwoValues = myList.TakeLast(2);
            var takeSmallerThanFour = myList.TakeWhile(num => num < 4);
        }


        //Paging
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultPerPage)
        {
            int startIndex = (pageNumber - 1) * resultPerPage;
            return collection.Skip(startIndex).Take(resultPerPage);
        }

        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (var number in aboveAverage)
            {
                Console.WriteLine("Query: Number:{0} Square{1}", number, Math.Pow(number, 2));
            }

        }

        //ZIP
        static public void ZipLINQ()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] stringNumbers = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);
            // 1 = One, 2 = Two... etc.


        }

        //Repeat & Range

        static public void RepeatAndRange()
        {
            //Generate collection from 1-->1000--> Range
            var first1000 = Enumerable.Range(0, 1000);


            var aboveAverage = from number in first1000
                               select number;
            //repeat five x
            var fiveXs = Enumerable.Repeat("X", 5);// list = {"X","X","X","X","X"}

            //Repeat a value N times
            IEnumerable<string> fiveXs2 = Enumerable.Repeat("X", 10);

        }

        static public void StudentLINQ()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Soel",
                    Grade = 90,
                    IsCertificate = true,
                },
                 new Student
                {
                    Id = 2,
                    Name = "Luciano",
                    Grade = 91,
                    IsCertificate = false,
                },
                  new Student
                {
                    Id = 3,
                    Name = "Fernando",
                    Grade = 70,
                    IsCertificate = false,
                },
                   new Student
                {
                    Id = 4,
                    Name = "Juan",
                    Grade = 96,
                    IsCertificate = true,
                }
            };

            //we get the certificated students
            var certificatedStudent = from student in classRoom
                                      where student.IsCertificate == true
                                      select student;

            var notCertificatedStudent = from student in classRoom
                                         where student.IsCertificate == false
                                         select student;

            var appovedStudents = from student in classRoom
                                  where student.Grade >= 50 && student.IsCertificate == true
                                  select student;
        }

        static public void AllLinqs()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

            bool allSmallerThanTen = numbers.All(x => x < 10);
            bool allBiggerOrEqualThan2 = numbers.All(x => x >= 3);

        }

        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            //0 + 1 = 1
            //1 + 2 = 3
            //3 + 4 = 7
            //etc

            string[] words = { "hello ", "my ", "name ", "is ", "Soel " };
            string greeting = words.Aggregate((prevGreting, current) => prevGreting + current);
        }


        static public void DistinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> values = numbers.Distinct();
        }

        //Group By
        static public void GroupBy()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

            //Obtain only even numbers and genrates two groups

            var grouped = numbers.GroupBy(x => x % 2 == 0);

            //group 1
            // 2, 4, 6

            //group 2
            //1,3,5,7

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value);
                }
            }




            //another example
            var classRoom2 = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Soel",
                    Grade = 90,
                    IsCertificate = true,
                },
                 new Student
                {
                    Id = 2,
                    Name = "Luciano",
                    Grade = 91,
                    IsCertificate = false,
                },
                  new Student
                {
                    Id = 3,
                    Name = "Fernando",
                    Grade = 70,
                    IsCertificate = false,
                },
                   new Student
                {
                    Id = 4,
                    Name = "Juan",
                    Grade = 96,
                    IsCertificate = true,
                }
            };

            var approvedQuery = classRoom2.GroupBy(student => student.IsCertificate && student.Grade <= 50);

            /* 1. we obtain two groups
             * a.Not certified
             * b.Certified
             */

            foreach (var group in approvedQuery)
            {
                Console.WriteLine("------------- {0} -------------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id=1,
                    Title ="My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        }
                    }

                },
                 new Post()
                    {
                    Id=2,
                    Title ="My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                     Comments = new List<Comment>
                    {
                         new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My content"
                        },
                         new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My content"
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments,
                (post, comment) => new { PostId = post.Id, comment.Content });

        }
    }
}