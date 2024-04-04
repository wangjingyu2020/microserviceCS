using Main.Infrastructure.entity;
using Main.Infrastructure.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Domain.Services.Impl
{
    public class BookService : IBookService
    {
        private ITestService _testService;

        public BookService(ITestService testService)
        {
            _testService = testService;
        }

        public Task<int> addBook()
        {
            return _testService.addBook();
        }

        public Task<List<Book>> getBookList()
        {
            return _testService.getBookList();
        }

        Task<int> IBookService.addBook()
        {
            return (_testService.addBook());
        }
    }
}
