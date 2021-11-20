using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntroSQL
{
    public class ListTable
    {
        public bool ListTableContents(IDbConnection connection, string whichTable)
        {


            switch (whichTable)
            {
                case "c":
                    var repoCategories = new CategoryRepository(connection);
                    var categories = repoCategories.GetAllCategory();
                    foreach (var cat in categories)
                    {
                        Console.WriteLine($"CategoryID: {cat.CategoryID}     Name: {cat.Name}    DepartmentID: {cat.DepartmentID}");
                    }
                    break;
                case "d":
                    var repoDepartment = new DepartmentRepository(connection);
                    var departments = repoDepartment.GetAllDepartments();
                    foreach (var dept in departments)
                    {
                        Console.WriteLine($"ID: {dept.DepartmentID}     Name: {dept.Name}");
                    }
                    break;
                case "e":
                    var repoEmployees = new EmpoyeesRepository(connection);
                    var employees = repoEmployees.GetAllEmployee();
                    foreach (var emp in employees)
                    {
                        var fullName = emp.FirstName.Trim();
                        if (emp.MiddleName is null)
                        {
                            fullName += emp.LastName.Trim();
                        } else 
                        { 
                            fullName += emp.MiddleName.Trim() + emp.LastName.Trim();
                        }
                        Console.WriteLine($"EmployeeID: {emp.EmployeeID}  Name: {fullName}  eMail: {emp.EmailAddress}  Phone: {emp.PhoneNumber} Title: {emp.Title} DateofBirth: {emp.DateOfBirth}");
                    }
                    break;
                case "p":
                    var repoProducts = new DapperProductRepository(connection);
                    var products = repoProducts.GetAllProducts();
                    foreach (var prod in products)
                    {
                        Console.WriteLine($"Product ID: {prod.ProductID} Name: {prod.Name}  Price: {prod.Price}  CategoryID: {prod.CategoryID}  OnSale: {prod.OnSale}  StockLevel: {prod.StockLevel}");
                    }
                    break;
                case "r":
                    var repoReviews = new ReviewsDepository(connection);
                    var reviews = repoReviews.GetAllReviews();
                    foreach (var rev in reviews)
                    {
                        Console.WriteLine($"Review ID: {rev.ReviewID} Product ID: {rev.ProductID}  Reviewer: {rev.Reviewer}  Rating: {rev.Rating}  Comment: {rev.Comment}");
                    }
                    break;
                case "s":
                    var repoSales = new SalesRepository(connection);
                    var sales = repoSales.GetAllSales();
                    foreach (var sale in sales)
                    {
                        Console.WriteLine($"Sales ID: {sale.SalesID} Product ID: {sale.ProductID}  Quantity: {sale.Quantity}  Price/Unit: {sale.PricePerUnit}  Date: {sale.Date}  EmployeeID: {sale.EmployeeID}");
                    }
                    break;
            }

            return true;
        }
    }

    public class AddToTable
    {
        public bool AddToTableContents(IDbConnection connection, string whichTable)
        {
            var itemToAdd = "";
            switch (whichTable)
            {
                case "c":
                    Console.Write("What category would you like to add? ");
                    itemToAdd = Console.ReadLine();
                    // when adding an item to the categories table, you need to add the category name and the category department.  
                    // read the departments table and ask the user which department the new category should be in

                    var repoDepartment = new DepartmentRepository(connection);
                    var departments = repoDepartment.GetAllDepartments();
                    var departmentID = 0;
                    Console.WriteLine();
                    Console.WriteLine($"In order to add the {itemToAdd} category, you need to specify what department the {itemToAdd} belongs in.");
                    do
                    {
                        Console.WriteLine($"From the list below, enter the value of the Department ID that {itemToAdd} should belong to.");
                        foreach (var dept in departments)
                        {
                            Console.WriteLine($"Department ID: {dept.DepartmentID}     Name: {dept.Name}");
                        }
                        Console.Write("Which Department ID? ");
                        var userInput = Console.ReadLine();
                        int.TryParse(userInput, out departmentID);
                        if (departmentID < 1 || departmentID > departments.Count()) {
                            Console.WriteLine("That is not a valid Department ID");
                            departmentID = 0;
                        }

                    } while (departmentID == 0);

                    //To get here, the user has entered a category to add and a valid deparment ID that the category belongs to
                    //now we can insert a new record into the categories table with the data entered by the user

                    var repo = new CategoryRepository(connection);
                    try
                    {
                        repo.InsertCategory(itemToAdd, departmentID);
                        Console.WriteLine($"The category {itemToAdd} has been added.");
                    }
                    catch
                    {
                        Console.WriteLine($"There was a problem adding the category {itemToAdd}");
                    }
                    break;

                case "d":
                    Console.WriteLine("Sorry, I have not implemented adding to the department table yet.");
                    break;

                case "e":
                    Console.WriteLine("Sorry, I have not implemented adding to the employee table yet.");
                    break;

                case "p":
                    Console.Write("What procuct would you like to add? ");
                    itemToAdd = Console.ReadLine();
                    // when adding an item to the products table, you need to add the product name, price, category ID, onSale status, and stock level.  
                    // read the categories table and ask the user which category the new product should be in

                    var repoCategory = new CategoryRepository(connection);
                    var categories = repoCategory.GetAllCategory();
                    var category = 0;
                    Console.WriteLine();
                    Console.WriteLine($"In order to add the product {itemToAdd}, you need to specify what category the {itemToAdd} belongs in.");
                    do
                    {
                        Console.WriteLine($"From the list below, enter the value of the Category that {itemToAdd} belongs to.");
                        foreach (var cat in categories)
                        {
                            Console.WriteLine($"categories ID: {cat.CategoryID}     Name: {cat.Name}");
                        }
                        Console.Write("Which Category ID? ");
                        var userInput = Console.ReadLine();
                        int.TryParse(userInput, out category);
                        if (category < 1 || category > categories.Count())
                        {
                            Console.WriteLine("That is not a valid category ID");
                            category = 0;
                        }

                    } while (category == 0);
                    Console.WriteLine();
                    double price = 0.0;
                    do
                    {

                        Console.Write($"Now you need to price the {itemToAdd}. What should the price be? ");
                        var itemPrice = Console.ReadLine();
                        double.TryParse(itemPrice, out price);
                        if (price < .01)
                        {
                            Console.WriteLine("That is not a valid price");
                            price = 0;
                        }
                    } while (price == 0);
                    Console.WriteLine();

                    bool onSale = false;
                    var loopcontrol = 0;
                    do
                    {

                        Console.Write($"Now you need to tell me if the {itemToAdd} is on sale. Type in Y if the {itemToAdd} is on sale.  Otherwise, type in N? ");
                        var itemOnSale = Console.ReadLine().ToLower();
                        if (itemOnSale == "y" || itemOnSale == "n")
                        {
                            if (itemOnSale == "y")
                            {
                                onSale = true;
                                loopcontrol = 1;
                            } else
                            {
                                onSale = false;
                                loopcontrol = 1;
                            }

                        } else
                        {
                            Console.WriteLine("That is not a valid entry");
                            loopcontrol = 0;
                        }
                    } while (loopcontrol == 0);
                    Console.WriteLine();

                    var inStock = -1;
                    loopcontrol = -1;

                    do
                    {

                        Console.Write($"Now you need to tell me how many {itemToAdd}'s you have in stock. How many are in stock? ");
                        var itemInStock = Console.ReadLine();
                        int.TryParse(itemInStock, out inStock);
                        if (inStock<0)
                        {
                            Console.WriteLine("That is not a valid entry.");
                            loopcontrol = -1;
                        } else
                        {
                            loopcontrol = 0;
                        }

                    } while (loopcontrol == -1);

                    //To get here, the user has entered all the values that are needed to add a product to the table
                    //now we can insert a new record into the categories table with the data entered by the user

                    var repoProduct = new DapperProductRepository(connection);
                    try
                    {
                        repoProduct.InsertProduct(itemToAdd, price, category, onSale, inStock);
                        Console.WriteLine($"The product {itemToAdd} has been added.");
                    }
                    catch
                    {
                        Console.WriteLine($"There was a problem adding the product {itemToAdd}");
                    }
                    break;

                case "r":
                    Console.WriteLine("Sorry, I have not implemented adding to the review table yet.");
                    break;

                case "s":
                    Console.WriteLine("Sorry, I have not implemented adding to the sales table yet.");
                    break;
            }
             
            return true;
        }
    }

    public class UpdateTable
    {
        public bool UpdateTableContents(IDbConnection connection, string whichTable)
        {
            var userInput = "";
            bool stayInLoop = true;

            switch (whichTable)
            {
                case "c":
                    Console.WriteLine("Sorry, updating the categories table is currently unavailable.");
                    break;
                case "d":
                    Console.WriteLine("Sorry, updating the department table is currently unavailable.");
                    break;
                case "e":
                    Console.WriteLine("Sorry, updating the employee table is currently unavailable.");
                    break;
                case "p":
                    int product = 0;
                    string productName = "";
                    double productPrice = 0.0;
                    int productCategoryID = 0;
                    bool productOnSale = false;
                    int productStockLevel = 0;

                    Console.WriteLine("From the list below, enter the value of the product that you wish to update.");
                    var repoUProducts = new DapperProductRepository(connection);
                    var products = repoUProducts.GetAllProducts();

                    var productIDFound = false;
                    do
                    {
                        foreach (var prod in products)
                        {
                            Console.WriteLine($"Product ID: {prod.ProductID}     Name: {prod.Name}");
                        }
                        Console.Write("Which Product ID? ");
                        userInput = Console.ReadLine();
                        int.TryParse(userInput, out product);
                        if (product < 1)
                        {
                            Console.WriteLine("That is not a valid product ID.  Please try again.");
                        } else
                        {
                            foreach (var prod in products)
                            {
                                if (product == prod.ProductID)
                                {
                                    productIDFound = true;
                                    productName = prod.Name;
                                    productPrice = prod.Price;
                                    productCategoryID = prod.CategoryID;
                                    productOnSale = prod.OnSale;
                                    productStockLevel = prod.StockLevel;
                                }
                            }
                            if (productIDFound == false)
                            {
                                Console.WriteLine("That product ID was not found.  Please try again.");
                            }
                        }

                    } while (productIDFound == false);
                    Console.WriteLine();

                    //we now know the product ID of the product the user wishes to modify.  Now we need to find out which fields in the
                    //product table the user wants to modify

                    //use boolean switches to store whether the user wants to update a specific table field
                    bool updateName = false;
                    bool updatePrice = false;
                    bool updateCategoryID = false;
                    bool updateOnSale = false;
                    bool updateStockLevel = false;

                    //use temporary variables to store the new values that the user enters for each table field the user will be changing
                    string newName = "";
                    double newPrice = 0.0;
                    int newCategoryID = 0;
                    bool newOnSale = false;
                    int newStockLevel = 0;

                    Console.WriteLine($"Now you can update the values for the {productName}");
                    userInput = "";
                    userInput = "";
                    var insideUserInput = "";

                    //find out if the user wants to update the Name field
                    //if yes, change updateName to true and store the new name value in newName

                    do
                    {
                        Console.Write("Do you want to update the name? (Y/N) ");
                        userInput = Console.ReadLine().ToLower();
                        if (userInput == "y")
                        {
                            updateName = true;
                            stayInLoop = true;
                            insideUserInput = "";
                            do
                            {
                                Console.Write("What should the new Name be? ");
                                insideUserInput = Console.ReadLine();
                                insideUserInput = insideUserInput.Trim();
                                if (insideUserInput.Length<1)
                                {
                                    Console.WriteLine("That is not a valid input.  Please try again.");
                                    userInput = "";
                                } else
                                {
                                    if (insideUserInput == productName)
                                    {
                                        Console.WriteLine("The name you entered is the same name that is stored.");
                                        stayInLoop = true;
                                    } else
                                    {
                                        string first = insideUserInput.FirstOrDefault().ToString();
                                        if (first == " ")
                                        {
                                            Console.WriteLine("Your product name can not begin with a space.  Please correct this.");
                                            stayInLoop = true;
                                        } else
                                        {
                                            newName = insideUserInput;
                                            stayInLoop = false;
                                        }
                                    }
                                }

                            } while (stayInLoop == true);
                        } else
                        {
                            if (userInput != "n")
                            {
                                Console.WriteLine("Your input is not valid.");
                                userInput = "";
                            }
                        }
                    } while (userInput == "");

                    //find out if the user wants to update the price field
                    //if yes, change updatePrice to true and store the new price value in newPrice

                    userInput = "";

                    do
                    {
                        Console.Write("Do you want to update the price? (Y/N) ");
                        userInput = Console.ReadLine().ToLower();
                        if (userInput == "y")
                        {
                            updatePrice = true;
                            stayInLoop = true;
                            do
                            {
                                Console.Write("What should the new price be? ");
                                insideUserInput = Console.ReadLine();
                                double doubleUserInput = -1.0;
                                double.TryParse(insideUserInput, out doubleUserInput);

                                if (doubleUserInput < .01)
                                {
                                    Console.WriteLine("That is not a valid input.  Please try again.");
                                    userInput = "";
                                }
                                else
                                {
                                    if (doubleUserInput == productPrice)
                                    {
                                        Console.WriteLine("The price you entered is the same price that is stored.");
                                        stayInLoop = true;
                                    } else
                                    {
                                        newPrice = doubleUserInput;
                                        stayInLoop = false;
                                        userInput = "1";
                                    }
                                }

                            } while (stayInLoop == true);
                        }
                        else
                        {
                            if (userInput != "n")
                            {
                                Console.WriteLine("Your input is not valid.");
                                userInput = "";
                            }
                        }
                    } while (userInput == "");

                    //find out if the user wants to update the CategoryID field
                    //if yes, change updateCategoryID to true and store the new category ID value in newCategoryID

                    userInput = "";

                    do
                    {
                        Console.Write("Do you want to update the Category ID? (Y/N) ");
                        userInput = Console.ReadLine().ToLower();
                        if (userInput == "y")
                        {
                            updateCategoryID = true;
                            stayInLoop = true;
                            do
                            {
                                Console.Write("What should the new Category ID be? ");
                                //
                                // in a real world senario, we should read the contents of categories table and store the values and verify that the
                                // user is changing the category to a valid category.  If needed, I will add that logic at a future point in time.
                                //
                                insideUserInput = Console.ReadLine();
                                int intUserInput = -1;
                                int.TryParse(insideUserInput, out intUserInput);

                                if (intUserInput < 1)
                                {
                                    Console.WriteLine("That is not a valid input.  Please try again.");
                                    userInput = "";
                                }
                                else
                                {
                                    if (intUserInput == productCategoryID)
                                    {
                                        Console.WriteLine("The category ID you entered is the same that is stored");
                                        stayInLoop = true;
                                    } else
                                    {
                                        newCategoryID = intUserInput;
                                        stayInLoop = false;
                                    }
                                }

                            } while (stayInLoop == true);
                        }
                        else
                        {
                            if (userInput != "n")
                            {
                                Console.WriteLine("Your input is not valid.");
                                userInput = "";
                            }
                        }
                    } while (userInput == "");

                    //find out if the user wants to update the OnSale field
                    //if yes, change updateOnSale to true and since this is a boolean field, whatever the current value is, assign the opposite

                    userInput = "";
                    stayInLoop = true;

                    do
                    {
                        Console.Write("Do you want to update the On Sale status? (Y/N) ");
                        userInput = Console.ReadLine().ToLower();
                        if (userInput == "y")
                        {
                            updateCategoryID = true;
                            newOnSale = productOnSale == true ? newOnSale = false : newOnSale = true;
                            stayInLoop = false;
                        }
                        else
                        {
                            if (userInput != "n")
                            {
                                Console.WriteLine("Your input is not valid.");
                                stayInLoop = true;
                            } else
                            {
                                stayInLoop = false;
                            }
                        }
                    } while (stayInLoop == true);

                    //find out if the user wants to update the StockLevel field
                    //if yes, change updateStockLevel to true and store the new stock level value in newStockLevel

                    userInput = "";

                    do
                    {
                        Console.Write("Do you want to update the Stock Level? (Y/N) ");
                        userInput = Console.ReadLine().ToLower();
                        if (userInput == "y")
                        {
                            updateStockLevel = true;
                            stayInLoop = true;
                            do
                            {
                                Console.Write("What should the new Stock Level be? ");
                                insideUserInput = Console.ReadLine();
                                int intUserInput = -1;
                                int.TryParse(insideUserInput, out intUserInput);

                                if (intUserInput < 0)
                                {
                                    Console.WriteLine("That is not a valid input.  Please try again.");
                                    userInput = "";
                                }
                                else
                                {
                                    if (intUserInput == productStockLevel)
                                    {
                                        Console.WriteLine("The stock level you entered is the same that is stored");
                                        stayInLoop = true;
                                    }
                                    else
                                    {
                                        newStockLevel = intUserInput;
                                        stayInLoop = false;
                                    }
                                }

                            } while (stayInLoop == true);
                        }
                        else
                        {
                            if (userInput != "n")
                            {
                                Console.WriteLine("Your input is not valid.");
                                userInput = "";
                            }
                        }
                    } while (userInput == "");

                    //at this point we know which fields the user wants to update and the new values that should be stored.
                    var repoProduct = new DapperProductRepository(connection);
                    try
                    {
                        repoProduct.UpdateProduct(product, updateName, updatePrice, updateCategoryID, updateOnSale, updateStockLevel, newName, newPrice, newCategoryID, newOnSale, newStockLevel);
                        Console.WriteLine($"The product has been updated.");
                    }
                    catch
                    {
                        Console.WriteLine($"There was a problem updating the product.");
                    }

                    break;

                case "r":
                    Console.WriteLine("Sorry, updating the rating table is currently unavailable.");
                    break;
                case "s":
                    Console.WriteLine("Sorry, updating the sales table is currently unavailable.");
                    break;
            }
            return true;
        }
    }

    public class DeleteFromTable
    {
        public bool DeleteFromTableContents(IDbConnection connection, string whichTable)
        {
            if (whichTable != "p")
            {
                Console.WriteLine("We only support deleting products at this time.");
                return true;
            }

            Console.WriteLine("In order to delete a product, you need to provide the productID.  Please enter the product ID you would like to delete.");

            var repoProducts = new DapperProductRepository(connection);
            var products = repoProducts.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"Product ID: {prod.ProductID} Name: {prod.Name}  Price: {prod.Price}  CategoryID: {prod.CategoryID}  OnSale: {prod.OnSale}  StockLevel: {prod.StockLevel}");
            }

            var stayInLoop = true;
            var userInput = "";
            int productID = 0;

            do
            {
                Console.Write("What is the product ID you would like to delete? ");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out productID);

                foreach (var prod in products)
                {
                    if (productID == prod.ProductID)
                    {
                        stayInLoop = false;
                    }
                }
                if (stayInLoop == true)
                {
                    Console.WriteLine("The value you entered was not found in the product list");
                }


            } while (stayInLoop == true);
            var repoProduct = new DapperProductRepository(connection);
            var deleteReturn = repoProduct.DeleteProduct(productID);
            if (deleteReturn == true)
            {
                Console.WriteLine($"The records have been removed for product {productID}");
            }

            return true;
        }
    }
}
