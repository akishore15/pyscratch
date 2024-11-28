using System;

namespace AuthenticationSystem
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }  // Note: In a real-world scenario, passwords should be hashed

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
using System;
using System.Collections.Generic;

namespace AuthenticationSystem
{
    public class Authentication
    {
        private readonly List<User> _users = new List<User>();

        public bool Register(string username, string password)
        {
            if (_users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Username already exists.");
                return false;
            }

            _users.Add(new User(username, password));
            Console.WriteLine("Registration successful.");
            return true;
        }

        public bool Login(string username, string password)
        {
            User user = _users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("Login successful.");
                return true;
            }

            Console.WriteLine("Invalid username or password.");
            return false;
        }
    }
}
using System;

namespace AuthenticationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Authentication auth = new Authentication();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser(auth);
                        break;
                    case "2":
                        LoginUser(auth);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RegisterUser(Authentication auth)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            auth.Register(username, password);
        }

        static void LoginUser(Authentication auth)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            auth.Login(username, password);
        }
    }
}

