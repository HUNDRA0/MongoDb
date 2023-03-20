namespace MongoDb.UI.UI.Menu;

public static class Option4
{
    public static void Build()
    {
        Console.Clear();
        Console.Write("\tDelete item selected.\n\n" +
            "\t1) Delete by ID\n" +
            "\t2) Cancel\n\n" +
            "\tPlease select: ");
    }

    public static async Task SelectAsync(DbManager db)
    {
        int selection = InputManager.Selection(2);
        switch (selection)
        {
            case 1:
                Console.Write("\tPlease enter item ID.\n" +
                    "\tLeave empty to cancel: ");
                string itemID = Console.ReadLine();

                if (itemID.Length < 1)
                {
                    Console.WriteLine("\tReturning to main menu.");
                    break;
                }

                string findItem = await db.ReadOneAsync("_id", itemID);
                if (findItem == null)
                {
                    Console.WriteLine("\tItem with that ID not found. Returning to main menu.");
                    return;
                }

                Console.WriteLine("\t" + findItem);
                Console.Write("\n\tDelete this item? (Y/N): ");

                bool validSelect = false;
                string input = "";

                do
                {
                    input = Console.ReadLine();
                    validSelect = (input.ToLower() == "y" || input.ToLower() == "n");

                    if (!validSelect)
                    {
                        Console.Write("\tInvalid input! Please try again: ");
                    }
                }
                while (!validSelect);

                switch (input.ToLower())
                {
                    case "y":
                        bool successful = await db.DeleteAsync(itemID);
                        if (successful)
                        {
                            Console.WriteLine($"\tSuccessfully deleted item with id: {itemID}\n" +
                                $"\tReturning to main menu.");
                            return;
                        }
                        break;

                    case "n":
                        Console.WriteLine("\tCancelling action. Returning to main menu.");
                        break;
                }
                break;

            case 2:
                Console.WriteLine("\tAction cancelled. Returning to main menu.");
                break;
        }
    }
}
