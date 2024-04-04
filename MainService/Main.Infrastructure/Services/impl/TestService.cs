using Main.Infrastructure.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Infrastructure.service.impl
{
    public class TestService : ITestService
    {
        private TestDbContext _dbContext;
        public TestService(TestDbContext dbContext) {  _dbContext = dbContext; }

        public Task<int> addBook()
        {
            _dbContext.Books.Add(new Book() { AuthorName = "wjy", Title = "test" });
            return Task.FromResult(_dbContext.SaveChanges());
        }

        public Task<List<Book>> getBookList()
        {
            _dbContext.Books.Add(new Book() { AuthorName = "wjy", Title = "test"});
            throw new NotImplementedException();
        }

        public Task<int> GetCount()
        {
            return Task.FromResult(666);
        }
    }
}
