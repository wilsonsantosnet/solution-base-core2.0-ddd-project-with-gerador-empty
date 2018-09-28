using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface ICommand<T>
    {

        bool IsValid(T aggregationRoot);

        Task<T> Execute(T aggregationRoot);

    }
}
