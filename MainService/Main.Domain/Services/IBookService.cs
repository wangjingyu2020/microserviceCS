using Main.Infrastructure.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Domain.Services
{
    public interface IBookService
    {
        public Task<List<Book>> getBookList();

        public Task<int> addBook();
    }
}
