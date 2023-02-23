using Swagger_demo.Models;

namespace Swagger_demo.Repository
{
    public interface ICRUD
    {
        public Task<List<tBook>>? Gets();
        public Task<tBook>? Get(int id);
        public Task<string>? Post(tBook book);
        public Task<string>? Put(tBook book);
        public Task<string>? Patch(tBook book);
        public Task<bool> Delete(int id);
        public Task<bool>? CheckBook(int id);
    }
}
