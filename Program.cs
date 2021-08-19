using System;
using System.Collections.Generic;
using System.IO;


namespace homework._6_Jakovlev
{
    class Student
    {
        string lastName;
        string firstName;
        string univercity;
        string faculty;
        public int course;
        public string department;
        public int group;
        public string city;
        public int age;

        public string LastName
        {
            get { return lastName; }
        }

        public string FirstName
        {
            get { return firstName; }
        }

        public string Univercity
        {
            get { return univercity; }
        }

        public string Faculty
        {
            get { return faculty; }
        }



        //Создаем конструктор
        public Student(string firstName, string lastName, string univercity, string faculty, string department, int age, int course, int group, string city)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.univercity = univercity;
            this.faculty = faculty;
            this.department = department;
            this.course = course;
            this.age = age;
            this.group = group;
            this.city = city;
        }

        public override string ToString()
        {
            return String.Format("{0,20}{1,15}{2,15}{3,15}{4,15}{5,15}", firstName, lastName, age, course, group, city);
        }
    }
    class Program
    {
        static void CalcStud(List<Student> list)
        {
            List<Student> newlist = new List<Student>();
            Student t;
            int[] courseFreq = new int[7];
            for (int i = 0; i < list.Count; i++)
            {
                t = list[i];
                if (t.age >= 18 && t.age <= 20)
                {
                    newlist.Add(t);
                    courseFreq[t.course]++;
                }
            }
            Console.WriteLine("У нас " + newlist.Count + " студентов в возрасте 18-20");
            Console.WriteLine("1 курс: " + courseFreq[1]);
            Console.WriteLine("2 курс: " + courseFreq[2]);
            Console.WriteLine("3 курс: " + courseFreq[3]);
            Console.WriteLine("4 курс: " + courseFreq[4]);
            Console.WriteLine("5 курс: " + courseFreq[5]);
            Console.WriteLine("6 курс: " + courseFreq[6]);
        }

        static int MyMethod(Student st1, Student st2)
        {
            if (st1.age > st2.age) return 1;
            if (st1.age < st2.age) return -1;
            return 0;
        }
        static int MyMethod2(Student st1, Student st2)
        {
            if (st1.course > st2.course) return 1;
            if (st1.course < st2.course) return -1;
            return 0;
        }

        static int SortByAgeAndCourse(Student st1, Student st2)
        {
            if (st1.age > st2.age) return 1;
            if (st1.age < st2.age) return -1;
            if (st1.course > st2.course) return 1;
            if (st1.course < st2.course) return -1;
            return 0;
        }

        static void Main(string[] args)
        {
            int magistr = 0;
            int bakalavr = 0;

            List<Student> list = new List<Student>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader("students_1.csv");
                Student t;
                string temp = "";
                string[] s;
                while (!sr.EndOfStream)
                {
                    try
                    {
                        temp = sr.ReadLine();
                        s = temp.Split(';');
                        //Добавляем в список новый экземляр класса Student
                        t = new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), Convert.ToInt32(s[6]), int.Parse(s[7]), s[8]);
                        list.Add(t);
                        //Одновременно подсчитываем кол-во бакалавров и магистров
                        if (t.course < 5) bakalavr++; else magistr++;
                    }
                    catch (ArgumentNullException e)
                    {

                    }
                    catch (ArgumentException exc)
                    {

                    }
                    catch (Exception e)
                    {
                        //throw new Exception();
                        Console.WriteLine(e.Message);
                        Console.WriteLine(temp);

                    }
                }
            }
            catch
            {

            }
            finally//try
            {
                if (sr != null) sr.Close();
            }

            Console.WriteLine("Всего студентов:" + list.Count);
            Console.WriteLine("Учащихся на 5 и 6 курсах (магистров):{0}", magistr); //учащихся на 5 и 6 курсах;

            List<Student> list2 = new List<Student>();
            list2 = list;

            list2.Sort(SortByAgeAndCourse);

            list.Sort(MyMethod); //отсортировать список по возрасту студента;
            list.Sort(MyMethod2); // и по курсу

            if (list.ToArray() == list2.ToArray())
            {
                Console.WriteLine("Да, оба метода возвращают одинаковый результат");
            }
            else
            {
                Console.WriteLine("Нет, методы возвращают другой результат");
            }

            CalcStud(list);//подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(частотный массив);
            Console.ReadKey();
        }
    }


}
