using Microsoft.EntityFrameworkCore;
using Swagger_demo.DBContext;
using Swagger_demo.Models;
using System.Net;

namespace Swagger_demo.Repository
{
    public class CRUD_Test : ICRUD
    {
        readonly MyDbContext _context;

        public CRUD_Test(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckBook(int id)
        {
            return await _context.tBooks.AsNoTracking().AnyAsync(o => o.Id == id); ;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = false;
            var book = await _context.tBooks.SingleOrDefaultAsync(o => o.Id == id);
            if (book != null)
            {
                _context.tBooks.Remove(book);
                var isDel = await _context.SaveChangesAsync();
                if (isDel > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public async Task<List<tBook>>? Gets()
        {
            return await _context.tBooks.AsNoTracking().ToListAsync();
        }

        public async Task<tBook>? Get(int id)
        {
            return await _context.tBooks.AsNoTracking().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<string> Post(tBook book)
        {
            _context.tBooks.Add(book);
            var isOK = await _context.SaveChangesAsync();

            if (isOK > 0)
            {
                return string.Empty;
            }
            else
            {
                return "失敗";
            }
        }

        public async Task<string> Put(tBook book)
        {
            if (await CheckBook(book.Id))
            {
                _context.tBooks.Attach(book);
                _context.tBooks.Update(book);
                var isOK = await _context.SaveChangesAsync();

                if (isOK > 0)
                {
                    return string.Empty;
                }
                else
                {
                    return "失敗";
                }
            }
            return "找無資料";
        }

        public async Task<string>? Patch(tBook book)
        {
            _context.tBooks.Attach(book);
            _context.tBooks.Update(book);
            var isOK = await _context.SaveChangesAsync();

            if (isOK > 0)
            {
                return string.Empty;
            }
            else
            {
                return "失敗";
            }
        }
    }
}
