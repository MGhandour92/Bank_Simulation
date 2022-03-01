using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account_API.Interfaces
{
    public interface ICBRespository<T>
    {
        IEnumerable<T> ReadFile();
        void WriteToFile(T log, string action, out string response);
    }
}
