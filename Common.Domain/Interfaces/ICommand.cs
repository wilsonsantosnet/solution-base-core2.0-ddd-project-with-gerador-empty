using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface ICommand<Input, Output, CommandClass>
    {

        bool IsValid(Input aggregationRoot);

        Task<Output> Execute(Input aggregationRoot);

    }
}
