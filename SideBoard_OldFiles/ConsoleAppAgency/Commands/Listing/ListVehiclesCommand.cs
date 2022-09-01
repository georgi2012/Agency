using System;
using System.Collections.Generic;
using System.Linq;
using Agency.Commands.Contracts;
using Agency.Core.Contracts;

namespace Agency.Commands.Creating
{
    // TODO
    class ListVehiclesCommand : ICommand
    {
        private readonly IAgencyFactory factory;
        private readonly IEngine engine;


        public ListVehiclesCommand(IAgencyFactory factory, IEngine engine)
        {
            this.factory = factory;
            this.engine = engine;
        }

        public string Execute(IList<string> parameters)
        {
            var vehs = this.engine.Vehicles;

            if (vehs.Count == 0)
            {
                return "There are no registered vehicles.";
            }

            return string.Join(Environment.NewLine + "####################" + Environment.NewLine, vehs);
        }
    }
}
