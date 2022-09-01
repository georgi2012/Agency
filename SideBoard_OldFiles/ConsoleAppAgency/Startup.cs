using Agency.Core;
using Agency.Models.Vehicles.Contracts;

namespace Agency
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            // Singleton design pattern
            // Ensures that there is only one instance of Engine in existance
            // Yo are already familiar with it, right?
            
            var engine = Engine.Instance;
            engine.Start();
        }
    }
}
