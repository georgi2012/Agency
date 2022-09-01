using Agency.Commands.Contracts;
using System.Collections.Generic;

namespace Agency.Core.Contracts
{
    public interface IParser
    {
        ICommand ParseCommand(string fullCommand);

        IList<string> ParseParameters(string fullCommand);
    }
}
