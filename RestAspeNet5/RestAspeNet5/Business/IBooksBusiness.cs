using RestAspeNet5.Data.VO;
using System.Collections.Generic;

namespace RestAspeNet5.Business
{
    public interface IBooksBusiness
    {
        BookVO Create(BookVO book);
        List<BookVO> FindAll();
        BookVO FindByID(long ID);
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
