using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;

namespace ECommerceSystem
{
    internal class Program
    {

        private UserServices _userService = new UserServices();
        private UsersManagement _usersManagement = UsersManagement.Instance;
        private StoreManagement _storeManagement = StoreManagement.Instance;

        private static void Main(string[] args)
        {
            IDataAccess data = DataAccess.Instance;

            var admin = new User(new SystemAdmin("itay", "linkin9p", "itay", "bou", "itay@email.com"));
            data.Users.Insert(new User(new Subscribed("itay", "linkin9p", "itay", "bou", "itay@email.com")));
            data.Users.Insert(admin);

            var user = data.Users.GetByIdOrNull(admin.Guid, u => u.Guid);
            Console.ReadLine();
        }


        private void initWithInput(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);


            foreach(string line in lines)
            {
                string [] args = line.Split(' ');
                if(args.Length < 1)
                {
                    Console.WriteLine("empty line in the input file");
                    return;
                }
                switch (args[0])
                {
                    case "register": 
                        if (!register(args))
                        {
                            return;
                        }
                        break;
                    case "login": 
                        if (!login(args))
                        {
                            return;
                        }
                        break;
                    case "open-store":
                        if (!openStore(args))
                        {
                            return;
                        }
                        break;
                    case "add-product-inventory": 
                        if (!addProductInv(args))
                        {
                            return;
                        }
                        break;
                    case "assign-manager":
                        if (!assignManager(args))
                        {
                            return;
                        }
                        break;
                }
            }
        }

        //register <username> <password> <first name> <last name> <mail>
        private bool register(string [] args)
        {
            if (args.Length == 6)
            {
                if (_usersManagement.register(args[1], args[2], args[3], args[4], args[5]) != null)
                {
                    Console.WriteLine("registration from input file fail");
                    return false;
                }

            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for register in the input file");
                return false;
            }

            return true;
        }

        //login <username> <password>
        private bool login(string [] args)
        {
            if (args.Length == 3)
            {
                if (!_usersManagement.login(args[1], args[2]).Item1)
                {
                    Console.WriteLine("registration from input file fail");
                    return false;
                }

            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for login in the input file");
                return false;
            }

            return true;
        }

        //open-store <username> <store name>
        private bool openStore(string [] args)
        {
            if (args.Length == 3)
            {
                string username = args[1];
                Guid userID = _usersManagement.getUserByName(username).Guid;
                if (userID.Equals(Guid.Empty) || !_storeManagement.openStore(userID, args[2]))
                {
                    Console.WriteLine("open store from input file fail");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for open store in the input file");
                return false;
            }
            return true;
        }
        
        //add-product-inventory <username> <store name> <description> <product name> <price> <quantity> <category> <min quantity> <max qauantity> <keywords>?
        private bool addProductInv(string [] args)
        {
            if (args.Length >= 10)
            {
                string username = args[1];
                double price = 0;
                int quantity = 0;
                Category category = 0;
                int minQuantity = 0;
                int maxQuantity = 0;
                Guid userID = _usersManagement.getUserByName(username).Guid;
                try
                {
                    price = Double.Parse(args[5]);
                    quantity = Int32.Parse(args[6]);
                    category = (Category)Enum.Parse(typeof(Category), args[7], true);
                    minQuantity = Int32.Parse(args[8]);
                    maxQuantity = Int32.Parse(args[9]);
                }
                catch(Exception e)
                {
                    Console.WriteLine("incorrect args types for add product inventory");
                }
                List<string> keywords = new List<string>();
                //make the keywords list
                for(int i = 10; i < args.Length; i++)
                {
                    keywords.Add(args[i]);
                }

                if (userID.Equals(Guid.Empty))
                {
                    Console.WriteLine("incorrect args for add product inventory");
                    return false;
                }
                // Added store name to product inventory
                _storeManagement.addProductInv(userID, args[2], args[3], args[4], price, quantity, category, keywords, minQuantity, maxQuantity, "Moshe Store");
            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for add product inventory in the input file");
                return false;
            }

            return true;
        }

        //assign-manager <assigner username> <assignee username> <store name>
        private bool assignManager(string [] args)
        {
            if (args.Length == 4)
            {
                string assignerUserName = args[1];
                Guid userID = _usersManagement.getUserByName(assignerUserName).Guid;
                if (userID.Equals(Guid.Empty) || !_storeManagement.assignManager(userID, args[2], args[3]))
                {
                    Console.WriteLine("assign manager from input file fail");
                    return false;
                }

            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for assign manager in the input file");
                return false;
            }

            return true;
        }

    }
}