using Main.Infrastructure.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Infrastructure.service
{
    public interface ITestService
    {
        Task<int> GetCount();

        public Task<List<Book>> getBookList();

        public Task<int> addBook();
    }
}
