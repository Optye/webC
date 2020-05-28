using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcontrollers.Models;

namespace Webcontrollers.Repositories
{
    public interface IDataRepository
    {
        Data GetOne(int id);
        IList<Data> GetAll();
    }
}
