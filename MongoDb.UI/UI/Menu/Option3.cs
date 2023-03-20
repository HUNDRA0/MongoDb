using System.Transactions;

namespace MongoDb.UI.UI.Menu
{
    public static class Option3
    {
        public static void Build()
        {
            Console.Clear();
            Console.Write("\tUpdating item.\n\n" +
                "\t1) Search item by ID to update\n" +
                "\t2) Cancel\n\n" +
                "\tPlease select: ");
        }

        public static async Task SelectAsync(DbManager db)
        {
            int selection = InputManager.Selection(2);
            switch (selection)
            {
                case 1:
                    Console.Clear();
                    Console.Write("\tPlease enter ID. Leave empty to cancel: ");
                    string itemID = Console.ReadLine();

                    if (itemID == ""|| itemID == null)
                    {
                        Console.WriteLine("\tCancelled. Returning to main menu.");
                        break;
                    }

                    string resultID = await db.ReadOneAsync("_id", itemID);
                    
                    if (resultID == "" || resultID == null)
                    {
                        Console.WriteLine("\tItem with specified ID not found. Returning to main menu.");
                        break;
                    }

                    Console.WriteLine(resultID);
                    Console.Write("\tEdit this item? (Y/N): ");
                    string input = Console.ReadLine();
                    
                    if (input.ToLower() == "n")
                    {
                        Console.WriteLine("\tCancelling action. Returning to main menu.");
                        break;
                    }

                    else if (input.ToLower() != "y")
                    {
                        Console.WriteLine("\tInvalid input. Returning to main menu.");
                        break;
                    }

                    Console.Write("\tPlease enter new item name: ");
                    string itemName = Console.ReadLine();
                    Console.Write("\tPlease enter new item description: ");
                    string itemDescript = Console.ReadLine();
                    Console.Write("\tPlease enter new item price: ");

                    bool resolved = false;
                    float itemPrice = 0;
                    do
                    {
                        resolved = float.TryParse(Console.ReadLine(), out itemPrice);
                        if (!resolved) Console.Write("\tInvalid input! Please try again: ");
                    }
                    while (!resolved);

                    Item updated = new Item(itemName, itemPrice, itemDescript);
                    bool success = await db.UpdateAsync(updated, itemID);

                    switch (success)
                    {
                        case true:
                            Console.WriteLine("\tAction successful! Item updated!\n" +
                                "\tReturning to main menu.");
                            break;

                        case false:
                            Console.WriteLine("\tAction failed. Item not updated.\n" +
                                "\tReturning to main menu.");
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine("\tAction cancelled. Returning to main menu.");
                    break;
            }
        }
    }
}
