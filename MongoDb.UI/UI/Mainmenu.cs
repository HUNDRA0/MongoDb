using MongoDb.UI.UI.Menu;

namespace MongoDb.UI.UI;

public static class Mainmenu
{
    public static void Build()
    {
        Console.Clear();
        Console.Write("\tWebshop.\n\n" +
            "\tWhat would you like to do?\n" +
            "\t1) Create item\n" +
            "\t2) Read item\n" +
            "\t3) Update item\n" +
            "\t4) Delete item\n" +
            "\t5) Exit application\n\n" +
            "\tSelect: ");
    }
}
