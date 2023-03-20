namespace MongoDb.Common.Static;

public static class InputManager
{
    public static int Selection(int menuLength)
    {
        int selection;
        bool validSelect;

        do
        {
            validSelect = Int32.TryParse(Console.ReadLine(), out selection);
            if (!validSelect || selection > menuLength || selection < 1) Console.Write("\tInvalid selection! Please try again: ");
        }
        while (!validSelect);

        return selection;
    }
    public static void Confirm()
    {
        Console.Write("\tPress any key to continue.");
        Console.ReadKey();
    }
}
