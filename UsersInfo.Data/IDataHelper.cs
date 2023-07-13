using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersInfo.Data
{
    public interface IDataHelper<Table>
    {
        // Read
        Task<List<Table>> GetAllAsync();
        Task<List<Table>> SearchAsync(string Iteam);
        Task<Table> FindAsync(int Id);
        // Write
        Task AddAsync(Table table);
        Task EditAsync(Table table);
        Task RemoveAsync(int Id);
    }
}
