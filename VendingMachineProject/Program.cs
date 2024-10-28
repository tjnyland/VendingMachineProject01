using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineProject
{
    /// Toby Nyland's Vending Machine Project
    /// Started properly on the 28/10/24
    /// Code works in visual studio code.
    internal class Program
    {
        struct VendingMachine //Creates a structure called vending machine
        {
            public string name; //Name of product
            public int ID; //ProductID
            public double price;//Price of the product

        }
        static void Main(string[] args) //Main
        {
            double bal = 0;
            bool online = true;
            VendingMachine[] table = new VendingMachine[20]; //Creates an array with all the values from the struct
            for (int i = 0; i < 10; i++)
            {
                //THE PRODUCTS// 
                //Each product is entered into the vending machine.
                //Mars Bar
                table[0].name = "Mars Bar";
                table[0].ID = 01;
                table[0].price = 1.20;
                // Walkers Crisps
                table[1].name = "Walkers Crisps";
                table[1].ID = 02;
                table[1].price = 0.80;
                //Hula hoop crisps
                table[2].name = "Hula hoop crisps";
                table[2].ID = 03;
                table[2].price = 1.00;
                //Double Decker
                table[3].name = "Double Decker";
                table[3].ID = 04;
                table[3].price = 1.50;
                //doritos
                table[4].name = "Doritos";
                table[4].ID = 05;
                table[4].price = 1.99;
                //cheetos
                table[5].name = "cheetos";
                table[5].ID = 06;
                table[5].price = 1.00;
                //Coca-cola
                table[6].name = "Coca-cola";
                table[6].ID = 07;
                table[6].price = 1.50;
                // Choc Milk
                table[7].name = "Chocolate Milkshake";
                table[7].ID = 08;
                table[7].price = 1.90;
                //"Cookie"
                table[8].name = "Cookie";
                table[8].ID = 09;
                table[8].price = 1.35;
                //Haribo 
                table[9].name = "Haribo starmix";
                table[9].ID = 10;
                table[9].price = 1.99;
            }
            while (online == true)//Main menu while loop
            {
                int choice = 0;
                Console.WriteLine("Welcome to the vending machine | Current balance = £"+bal);
                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("[1] Insert Coins: ");
                Console.WriteLine("[2] Select your product ");
                Console.WriteLine("[3] Admin console ");
                Console.WriteLine("[4] Exit Vending Machine ");
                try //If the value is an int value then:
                {
                    choice = int.Parse(Console.ReadLine());
                    switch (choice) //Switch statment for option values
                    {
                        case 1: //If enter option 1 - Insert Coins
                            Console.WriteLine("Please insert your coins:"); 
                            EnterCoins(ref bal); //passes the varible bal through
                            Console.WriteLine("Coins sucessfully entererd you have a balance of £" + bal); //Balance has been returned to main
                            Console.WriteLine("Would you like to go back to main menu? (y/n)"); //asks user if want to return to main menu
                            string answer = Console.ReadLine(); //stores that value
                            if (answer == "y") //if the answer is yes
                            {
                                Console.WriteLine("Returning to menu....");
                                Console.WriteLine("\n");
                            }
                            else if (answer == "n") //if the answer is no
                            {
                                Console.WriteLine("Exiting the program");
                                online = false; //stops the main menu while loop
                            }
                            else //any other value entered
                            {
                                Console.WriteLine("Invaid input | Returning to main menu");
                                Console.WriteLine("\n");
                                return;
                            }
                            break;
                        case 2: //Option 2 - Select product
                            Console.WriteLine("Please select your product [Anything with Product ID of 0 is a free space in the vending machine]");
                            ViewMachine(ref table, ref bal);//Calls the ViewMachine method and passes table array through it and user's balance
                            break;
                        case 3: //Option 3 - ADMIN
                            Console.WriteLine("Enter ADMIN password to add items to the vending machine:");
                            string password = Console.ReadLine(); //stores password entered
                            if (password == "Password123") //if password is correct
                            {
                                Console.WriteLine("Password entered correctly.You can add up to 10 items to the vending machine!");
                                AddItem(ref table);//Calls the AddItem method and passes table array
                            }
                            else //if password is wrong
                            {
                                Console.WriteLine("Incorrect password. Not allowed in");
                            }
                            break;
                        case 4:// Option 4 - Exit the program and refunds
                            Console.WriteLine("Checking user balance...");
                            if (bal > 0)
                            {
                                Console.WriteLine("Refunding your un-used balance of £"+ bal);
                                bal = 0; //sets balance to 0.
                                Console.WriteLine("Have a great day");
                            }
                            else
                            {
                                Console.WriteLine("Have a great day");
                            }
                            //Balance is now zero. Ready for new customer.
                            break;

                        default:// Any other values
                            Console.WriteLine("An error has occured with the option you have entered! Please try again");
                            break;
                    }
                }
                catch //If option entered is an invaild datatype.
                {
                    Console.WriteLine("An error has occured with the option you have entered! Please try again.");
                    return;
                }
                Console.ReadLine(); //reads the lines of the program
            }
        }
        //OPTION 3 - ADD ITEMS TO VENDING MACHINE --> ADMIN.
        static void AddItem(ref VendingMachine[] table) //admin (option 3)
        {
            Console.WriteLine("How many items would you like to add? ");
            try//Make sure the value entered is an int value!
            {
                int ItemAmount = int.Parse(Console.ReadLine()); //Stores how many items to the user wants to add.
                if (ItemAmount > 0 && ItemAmount <= 10)
                {
                    for (int i = 0; i < ItemAmount; i++) //adds from index 10 as we already have items up to index 9, adds how many items user wants.
                    {
                        Console.WriteLine("Please enter product name: "); //enter name of product
                        table[10 + i].name = Console.ReadLine(); //stored
                        Console.WriteLine("Enter Product ID: "); //enter id of product
                        table[10 + i].ID = int.Parse(Console.ReadLine()); //stored
                        Console.WriteLine("Enter the price of product: "); //enter price of product
                        table[10 + i].price = double.Parse(Console.ReadLine()); //stored
                    }
                }
                else //they try to add more than 10 items or less than 1 items
                {
                    Console.WriteLine("Amount of items entered must be between 1 and 10");
                }
            }
            catch //If they don't enter an int value
            {
                Console.WriteLine("Invalid input entered! (Returning to menu)");

            }
           
        }
        //OPTION 2
        static void ViewMachine(ref VendingMachine[] table, ref double bal)//passing table through it
        {
            int tableLen = table.Length - 1; //For the for loop gets the length of the table[].
            for (int i = 0; i < tableLen; i++) //loops through that. - Shows the vending machine
            {
                Console.WriteLine($"{table[i].ID} - {table[i].name} - £{table[i].price}");// displays the vending machine items.
            }
            Console.WriteLine("Enter the Product ID of the item you want!");
            try // If the product ID entered is an int value
            { 
                int id = int.Parse(Console.ReadLine()) - 1;//Stores value of id (minus one to find the true index value).
                if (id <= 20) //if the ID is within the table[] array
                {
                    Console.WriteLine($"Product selected is {table[id].name} and is £{table[id].price} is this correct");
                    Console.WriteLine("Vending...");
                    if (bal >= table[id].price) // If the user has enough money
                    {
                        Console.WriteLine($"Successfully bought a {table[id].name}!");
                        bal = bal - table[id].price; //takes the price from the user's balance
                    }
                    else
                    {
                        Console.WriteLine("Sorry you don't have enough money for this item");
                    }
                }
                else //if the product id is out of the array.
                {
                    Console.WriteLine("Invalid product ID entered");
                }
               
            }
            catch //If the product ID entered is not the correct data type.
            {
                Console.WriteLine("Invalid product ID entered");
            }
        }
        //OPTION 1 - ENTER COINS
        static double EnterCoins(ref double bal)//passes the user balance through it and returns that back after method is done.
        {
           Console.WriteLine("This mechine only accepts [5p], [10p], [20p],[50p],[£1] and [£2] coins. Any other values will be rejected.");
           Console.WriteLine("To enter the coins enter its value in terms of £s. [50p] = 0.50, [£2] = 2.");
           
            while (true) //while loop
            {
                Console.WriteLine("Would you like to enter a coin? (y/n) "); //asks if the user wants to enter a coin
                string ans = Console.ReadLine(); //stores than ans
                if (ans == "y") //if ans is yes
                {
                    Console.WriteLine("Enter a coin: ");
                    try //If the value entered is a double/int value
                    {
                        double coinValue = double.Parse(Console.ReadLine()); //stores that value
                        if (coinValue == 0.5 || coinValue == 0.05 || coinValue == 0.2 || coinValue == 0.1 || coinValue == 1 || coinValue == 2) //if its a valid coin type entered
                        {
                            bal = bal + coinValue; //adds that coin value to the user's balance
                            Console.WriteLine("Added £" + coinValue + " to your balance!");
                            Console.WriteLine("Your current balance is £" + bal);
                        }
                        else //Not a valid coin
                        {
                            Console.WriteLine("Not a valid or accepted coin!");
                        }
                    }
                    catch //If the value is not a valid datatype
                    {
                        Console.WriteLine("An error has occured with the coin value you have entered! Please try again");
                    }
                }
                if (ans == "n")// if ans is no to entering another coin
                {
                return bal; //retuns the value
                }
            }
        }
    }
}