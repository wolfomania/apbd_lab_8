﻿using Exercise6.Models;

namespace Exercise6
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            #region Load depts

            List<Dept> depts =
            [
                new Dept
                {
                    Deptno = 1,
                    Dname = "Research",
                    Loc = "Warsaw"
                },
                new Dept
                {
                    Deptno = 2,
                    Dname = "Human Resources",
                    Loc = "New York"
                },
                new Dept
                {
                    Deptno = 3,
                    Dname = "IT",
                    Loc = "Los Angeles"
                }
            ];

            Depts = depts;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            List<Emp> emps =
            [
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10
            ];

            Emps = emps;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
            IEnumerable<Emp> result = Emps.Where(e => e.Job == "Backend programmer");
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            IEnumerable<Emp> result = Emps
                .Where(emp => emp is { Job: "Frontend programmer", Salary: > 1000 })
                .OrderByDescending(emp => emp.Ename);
            return result;
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            int result = Emps.Max(emp => emp.Salary);
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            IEnumerable<Emp> result = Emps
                .Where(emp => emp.Salary == Emps.Max(emp1 => emp1.Salary));
            return result;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            IEnumerable<object> result = Emps.Select(emp => new { Nazwisko = emp.Ename, Praca = emp.Job });
            return result;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     The result: merge of Emps and Depts collections.
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            IEnumerable<object> result = Emps
                .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, 
                (emp, dept) => new { emp.Ename, emp.Job, dept.Dname });
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            IEnumerable<object> result = Emps
                .GroupBy(emp => emp.Job)
                .Select(group => new { Praca = group.Key, LiczbaPracownikow = group.Count() });
            return result;
        }

        /// <summary>
        ///     Return the value "true" if at least one
        ///     of the elements in the collection works as a "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            bool result = Emps.Any(emp => emp.Job == "Backend programmer");
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            Emp result = Emps.Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .First();
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            IEnumerable<object> result = Emps.Select(emp => new {emp.Ename, emp.Job, emp.HireDate}).
                Union(new[] {new {Ename = "Brak wartości", Job = (string) null, HireDate = (DateTime?) null}});
            return result;
        }

        /// <summary>
        /// Using LINQ, retrieve employees divided by departments, keeping in mind that:
        /// 1. We are only interested in departments with more than 1 employee
        /// 2. We want to return a list of objects with the following structure:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Use anonymous types
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            IEnumerable<object> result = Depts
                .Join(Emps, dept => dept.Deptno, emp => emp.Deptno, 
                    (dept, emp) => new {dept.Dname, emp.Empno})
                .GroupBy(dept => dept.Dname)
                .Where(group => group.Count() > 1)
                .Select(group => new {name = group.Key, numOfEmployees = group.Count()});
                    
            return result;
        }

        /// <summary>
        /// Write your own extension method that will allow the following code snippet to compile.
        /// Add the method to the CustomExtensionMethods class, which is defined below.
        ///
        /// The method should return only those employees who have at least 1 direct subordinate.
        /// Employees should be sorted within the collection by surname (ascending) and salary (descending).
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {
            IEnumerable<Emp> result = Emps.TaskGetEmployeesWithSubordinates();
            return result;
        }

        /// <summary>
        /// The method below should return a single int number.
        /// It takes a list of integers as input.
        /// Try to find, using LINQ, the number that appears an odd number of times in the array of ints.
        /// It is assumed that there will always be one such number.
        /// For example: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static int Task13(int[] arr)
        {
            int result = arr.GroupBy(x => x)
                .Where(group => group.Count() % 2 != 0)
                .Select(group => group.Key)
                .First();
            //result=
            return result;
        }

        /// <summary>
        /// Return only those departments that have exactly 5 employees or no employees at all.
        /// Sort the result by department name in ascending order.
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            IEnumerable<Dept> result = Depts
                .GroupJoin(Emps, dept => dept.Deptno, emp => emp.Deptno, (dept, emp) => new { dept, emp })
                .Where(group => group.emp.Count() == 5 || !group.emp.Any())
                .OrderBy(group => group.dept.Dname)
                .Select(group => group.dept);
            //result =
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        public static IEnumerable<Emp> TaskGetEmployeesWithSubordinates(this IEnumerable<Emp> emps)
        {
            return emps.Where(emp => emps.FirstOrDefault(emp1 => emp1.Mgr == emp) != null)
                .OrderBy(emp => emp.Ename)
                .ThenByDescending(emp => emp.Salary);
        }
    }
}