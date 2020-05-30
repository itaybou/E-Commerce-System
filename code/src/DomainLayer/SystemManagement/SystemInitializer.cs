using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public class SystemInitializer
    {
        private const string RELATIVE_INIT_FILE_PATH = "\\init_file\\init_file.txt";

        private SystemManager _systemManagement;
        private UsersManagement _usersManagement;
        private StoreManagement _storeManagement;
        private TransactionManager _transactionManager;
        private ICommunication _communication;
        private IDataAccess _data;

        public void Initialize()
        {
            var initFilePath = GetInitFilePath();
            Console.WriteLine("Starting System Initialization.");
            InitSystem();
            Console.WriteLine("Finished initializing system.");
            if (initFilePath != null)
            {
                Console.WriteLine("Lodaing initial file data from file located at: " + initFilePath);
                initWithInput(initFilePath);
                Console.WriteLine("Initial data loaded, initialization process is done!");
            }
        }

        private void InitSystem()
        {
            // Initialize Singleton Constructors
            SystemLogger.initLogger();
            Console.WriteLine("Intialized System Logger.");
            Console.WriteLine("Initializing DataAcces, establishing database communication.");
            _data = DataAccess.Instance;
            Console.WriteLine("Intialized DataAccess, Database communication established.");
            Console.WriteLine("Resetting database.");
            _data.DropDatabase();
            Console.WriteLine("Database reset.");
            Console.WriteLine("Creating database.");
            _data.InitializeDatabase();
            Console.WriteLine("Database created.");
            _communication = Communication.Instance;
            Console.WriteLine("Intialized Communication and Websockets.");
            Console.WriteLine("Initializing Domain.");
            _usersManagement = UsersManagement.Instance;
            Console.WriteLine("Intialized User Management.");
            _storeManagement = StoreManagement.Instance;
            Console.WriteLine("Intialized Stores Management.");
            _transactionManager = TransactionManager.Instance;
            Console.WriteLine("Initialized Transaction Manager, Established connection with external systems.");
            _systemManagement = SystemManager.Instance;
            Console.WriteLine("Initialized System Manager, Spell Checker and Search And Filter system.");
        }

        private string GetInitFilePath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return projectDirectory + RELATIVE_INIT_FILE_PATH;
        }

        private void initWithInput(string path)
        {
            string[] lines = File.ReadAllLines(path);


            foreach (string line in lines)
            {
                string[] args = line.Split(' ');
                if (args.Length < 1)
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
                    case "create-admin":
                        if (!createAdmin(args))
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
                    case "edit-permissions":
                        if (!editPermissions(args))
                        {
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine("incorrect input command");
                        return;
                }
            }
        }

        //edit-permissions <editor username> <store name> <manager username> <permissions>*
        private bool editPermissions(string[] args)
        {
            if (args.Length >= 4)
            {
                List<PermissionType> permissions = new List<PermissionType>();
                for (int i = 4; i < args.Length; i++)
                {
                    try
                    {
                        permissions.Add((PermissionType)Enum.Parse(typeof(PermissionType), args[i], true));
                    }
                    catch (Exception p)
                    {
                        Console.WriteLine("incorrect permissions for edit permissions");
                    }
                }
                string username = args[1];
                Guid userID = _usersManagement.getUserByName(username).Guid;
                if (userID.Equals(Guid.Empty) || !_storeManagement.editPermissions(userID, args[2], args[3], permissions))
                {
                    Console.WriteLine("edit permissions from input file fail");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for edit permissions in the input file");
                return false;
            }
        }

        //register <username> <password> <first name> <last name> <mail>
        private bool register(string[] args)
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

        //register <username> <password> <first name> <last name> <mail>
        private bool createAdmin(string[] args)
        {
            if (args.Length == 6)
            {
                if (!_usersManagement.CreateAdmin(args[1], args[2], args[3], args[4], args[5]))
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
        private bool login(string[] args)
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
        private bool openStore(string[] args)
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

        //add-product-inventory <username> <store name> <description> <product name> <price> <quantity> <category> <min quantity> <max qauantity> <image-url> <keywords>*
        private bool addProductInv(string[] args)
        {
            if (args.Length >= 11)
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
                catch (Exception e)
                {
                    Console.WriteLine("incorrect args types for add product inventory");
                }
                List<string> keywords = new List<string>();
                //make the keywords list
                for (int i = 11; i < args.Length; i++)
                {
                    keywords.Add(args[i]);
                }

                if (userID.Equals(Guid.Empty))
                {
                    Console.WriteLine("incorrect args for add product inventory");
                    return false;
                }
                // Added store name to product inventory
                _storeManagement.addProductInv(userID, args[2], args[3], args[4], price, quantity, category, keywords, minQuantity, maxQuantity, args[10]);
            }
            else
            {
                Console.WriteLine("incorrect input, incorrect number of arguments for add product inventory in the input file");
                return false;
            }

            return true;
        }

        //assign-manager <assigner username> <assignee username> <store name>
        private bool assignManager(string[] args)
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