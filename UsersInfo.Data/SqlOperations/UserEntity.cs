using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersInfo.Core;

namespace UsersInfo.Data.SqlOperations
{
    public class UserEntity : IDataHelper<User>
    {
        private DBContext db;
        public UserEntity()
        {
            db = new DBContext();
        }
        public async Task AddAsync(User table)
        {
            await db.Users.AddAsync(table);
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(User table)
        {
            db = new DBContext();
            db.Users.Update(table);
            await db.SaveChangesAsync();
        }

        public async Task<User> FindAsync(int Id)
        {
            return await db.Users.FindAsync(Id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task RemoveAsync(int Id)
        {
            var iteam = await FindAsync(Id);
            db.Users.Remove(iteam);
            await db.SaveChangesAsync();
        }

        public async Task<List<User>> SearchAsync(string Iteam)
        {
            return await db.Users
                .Where(x => x.Id.ToString() == Iteam
            || x.FullName.Contains(Iteam)
            || x.PhoneNumber.Contains(Iteam)
            || x.Email.Contains(Iteam)
            )
                .ToListAsync();
        }
    }
}
