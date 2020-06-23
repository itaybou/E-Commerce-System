﻿using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public bool Initialize()
        {
            var initFilePath = GetInitFilePath();
            Console.WriteLine("Starting System Initialization.");
            try
            {
                var externalURL = GetExternalSystemsURLFromInitFile(initFilePath);
                InitSystem(externalURL);
            } catch(Exception)
            {
                return false;
            }
            Console.WriteLine("Finished initializing system.");
            if (initFilePath != null)
            {
                Console.WriteLine("Lodaing initial file data from file located at: " + initFilePath);
                if(!initWithInput(initFilePath))
                {
                    Console.WriteLine("Data initilization from init file failed, view error log for more details.");
                }
                else Console.WriteLine("Initial data loaded, initialization process is done!");
            }
            else
            {
                Console.WriteLine("Missing init file. init from file aborted.");
                return false;
            }
            return true;
        }

        private void InitSystem(string externalURL)
        {
            // Initialize Singleton Constructors
            try
            {
                SystemLogger.initLogger();
                Console.WriteLine("Intialized System Logger.");
                InitDatabase();
                _communication = Communication.Instance;
                Console.WriteLine("Intialized Communication and Websockets.");
                Console.WriteLine("Initializing Domain.");
                _usersManagement = UsersManagement.Instance;
                Console.WriteLine("Intialized User Management.");
                _storeManagement = StoreManagement.Instance;
                Console.WriteLine("Intialized Stores Management.");
                InitExternalSystems(externalURL);
                _systemManagement = SystemManager.Instance;
                Console.WriteLine("Initialized System Manager, Spell Checker and Search And Filter system.");
            } catch(DatabaseException)
            {
                SystemLogger.LogError("Database init error.");
                Console.WriteLine("Initialization failed. connection timeout to database. try to initialize again.");
                throw new DatabaseException("init failed");
            } catch(ExternalSystemException)
            {
                Console.WriteLine("Initialization failed. external system handshake failed.");
                throw new ExternalSystemException("init failed");
            } catch(Exception e)
            {
                SystemLogger.LogError("Initilization error: " + e);
                Console.WriteLine("Initialization failed due to undetermined reason. please check error log file for more information.");
            }
        }

        private void InitDatabase()
        {
            try
            {
                Console.WriteLine("Initializing DataAcces, establishing remote database communication.");
                _data = DataAccess.Instance;
                Console.WriteLine("Intialized DataAccess, Database communication established.");
                Console.WriteLine($"Established connection to remote host: {_data.ConnectionString}");
                Console.WriteLine("Resetting database.");
                _data.DropDatabase();
                Console.WriteLine("Database reset.");
                Console.WriteLine("Creating database.");
                _data.InitializeDatabase();
                Console.WriteLine("Database created.");
            } catch(DatabaseException)
            {
                throw;
            }
        }

        private async void InitExternalSystems(string externalURL)
        {
            Console.WriteLine("Initializing External Systems");
            _transactionManager = TransactionManager.Instance;
            Console.WriteLine("Initialized Transaction Manager");
            Console.WriteLine("Establishing connection to external systems");
            try
            {
                await _transactionManager.ConnectExternal(externalURL);
            }
            catch (ExternalSystemException e)
            {
                SystemLogger.LogError("External systems initilization failed: " + e);
                Console.WriteLine("Initialization failed. external system handshake failed.");
                throw new ExternalSystemException("init failed");
            }
            Console.WriteLine("Handshake with external systems established. external systems are active.");
        }

        private string GetInitFilePath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            if(File.Exists(projectDirectory + RELATIVE_INIT_FILE_PATH))
                return projectDirectory + RELATIVE_INIT_FILE_PATH;
            else
            {
                projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
                if (File.Exists(projectDirectory + RELATIVE_INIT_FILE_PATH))
                    return null;
                return projectDirectory + RELATIVE_INIT_FILE_PATH;
            }
        }

        private string GetExternalSystemsURLFromInitFile(string path)
        {
            try
            {
                var externalTags = File.ReadAllLines(path).Where(line => line.StartsWith("<external_url>") && line.EndsWith("</external_url>"));
                var url = Regex.Replace(externalTags.First(), @"<[^>]+>| ", "").Trim();
                if (String.IsNullOrEmpty(url))
                {
                    throw new ArgumentException("init fail");
                }
                return url;
            } catch(Exception)
            {
                Console.WriteLine("No external systems url provided in init file. aborting init.");
                throw new ArgumentException("init fail");
            }
        }

        private bool initWithInput(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] args = line.Split(' ');
                    if (args.Length < 1)
                    {
                        Console.WriteLine("empty line in the input file");
                        return false;
                    }
                    if (args[0].StartsWith("<external_url>"))
                        continue;
                    switch (args[0])
                    {
                        case "register":
                            if (!register(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        case "create-admin":
                            if (!createAdmin(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        case "login":
                            if (!login(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        case "open-store":
                            if (!openStore(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        case "add-product-inventory":
                            if (!addProductInv(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        case "assign-manager":
                            if (!assignManager(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        case "edit-permissions":
                            if (!editPermissions(args))
                            {
                                SystemLogger.LogError("Failed to perform init file command: " + args[0]);
                                return false;
                            }
                            break;
                        default:
                            Console.WriteLine("incorrect input command");
                            throw new ArgumentException(args[0]);
                    }
                }
            } catch(ArgumentException e)
            {
                SystemLogger.LogError("Incorrect initalization file command: " + e);
                return false;
            } catch(Exception e)
            {
                SystemLogger.LogError("Unexpected error occured durring initialization from init file: " + e);
                return false;
            }

            return true;
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