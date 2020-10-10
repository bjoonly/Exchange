using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{


  
    class Customer
    {
        private int money;
        public int Money
        {
            get { return money; }
            set
            {
                if (value < 0)
                    value = 0;
                money = value;
            }
        }
        private int assets;
        public int Assets
        {
            get { return assets; }
            set
            {
                if (value < 0)
                    value = 0;
                assets = value;
            }
        }
        public string Name { get; set; }
        public Customer(string name,int money=100000, int assets=15)
        {
            Name = name;
            Money = money;
            Assets = assets;
        }
        public void Buy (string name, int course)
        {
            int assets;
            Random random = new Random();
            if (Money < course)
            {
                Console.WriteLine("No money.");
                return;
            }
            assets = random.Next(1, (Money / course)+1);
            Assets += assets;
            Money -= assets * course;
            Console.WriteLine(Name+" buy "+assets+" assets of "+name+" by course "+course);
        }
        public void Sell(string name, int course)
        {
            Random random = new Random();
            int assets;
            if (Assets < 1)
            {
                Console.WriteLine("No assets that you can sell.");
                return;
            }
            assets = random.Next(1, Assets + 1);
            Assets -= assets;
            Money += course * assets;
            Console.WriteLine(Name + " sell " + assets + " assets of " + name + " by course " + course);
        }
        public void Show()
        {
            Console.WriteLine($"Name: {Name}\nAssets: {Assets}\nMoney: {Money}\n");
        }
    }
  
    class Exchange
    {
        public delegate void CourseDelegate(string name,int course);
        public event CourseDelegate CourseChangeUp;
        public event CourseDelegate CourseChangeDown;
        public string Name { get; set; }
        private int course;
        public void ChangeCourse()
        {
            Random random = new Random();
            Course = random.Next(-10, 11);
        }
        public int Course
        {
            get { return course; }
            set
            {

                if (course == 0)
                    course = value;
                else if (course + value > course)
                {
                    course += value;
                    CourseChangeUp?.Invoke(Name, course);
                }

                else if (course + value < course)
                {
                    course += value;
                    if (course < 1)
                        course = 1;
                    CourseChangeDown?.Invoke(Name, course);
                }
      
            }
         }
        public Exchange(string name, int course)
        {
            Name = name;
            Course= course;
        }
        public void Show()
        {
            Console.WriteLine($"Name: {Name}\nCourse: {Course}");
        }
 
    }

    class Program
    {
       


        static void Main(string[] args)
        {    
            Customer c1 = new Customer("C1"), c2 = new Customer("C2", 15000000), c3 = new Customer("C3");
            Exchange exchange = new Exchange("Bitcoin",200);
            exchange.CourseChangeUp += c1.Sell;
            exchange.CourseChangeUp += c2.Sell;
            exchange.CourseChangeUp += c3.Sell;
            exchange.CourseChangeDown += c1.Buy;
            exchange.CourseChangeDown += c2.Buy;
            exchange.CourseChangeDown += c3.Buy;
            c1.Show();
            c2.Show();
            c3.Show();
            exchange.Show();
            Console.ReadKey();
            for (int i = 0; i < 15; i++)
            {
                Console.Clear();
                exchange.Show();
                Console.WriteLine(new string('-',25));
                exchange.ChangeCourse();
                Console.WriteLine(new string('-', 25));
                c1.Show();
                c2.Show();
                c3.Show();
                Console.ReadKey();

            }


        }
    }
}
