// See https://aka.ms/new-console-template for more information


// Initialize
using MongoDb.UI.UI.Menu;

var db = new DbManager(
    "mongodb+srv://Jacoberc:hejsan1@cluster0.goula8n.mongodb.net/test",
    "MongoDb",
    "MongoDbuppgift");



// Begin build
bool running = true;
while (running)
{
    Mainmenu.Build();

    int selection = InputManager.Selection(5);
    switch (selection)
    {
        case 1:
            Option1.Build();
            await Option1.SelectAsync(db);
            InputManager.Confirm();
            break;

        case 2:
            Option2.Build();
            await Option2.SelectAsync(db);
            InputManager.Confirm();
            break;

        case 3:
            Option3.Build();
            await Option3.SelectAsync(db);
            InputManager.Confirm();
            break;

        case 4:
            Option4.Build();
            await Option4.SelectAsync(db);
            InputManager.Confirm();
            break;

        case 5:
            Console.Write("\tClose the software? (Y/N) ");
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

            switch (input)
            {
                case "y":
                    Environment.Exit(0);
                    break;

                case "n":
                    running = true;
                    Console.WriteLine("\tReturning to main menu.");
                    InputManager.Confirm();
                    break;
            }
            break;
    }
}