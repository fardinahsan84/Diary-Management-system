using DiaryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary.Repository
{
    public interface IDiaryRepository : IRepository<Diary>
    {
        IEnumerable<Diary> GetHighestPaid();
    }
}
