/// ETML
/// Auteur      : Sylvain Laydernier
/// Date        : 15.03.2018
/// Description : Main

namespace RandomAnimals
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create controller
            Controller appController = new Controller();

            // Create model
            Model appModel = new Model(appController);

            // Create view
            View appView = new View(appController);
        }
    }
}
