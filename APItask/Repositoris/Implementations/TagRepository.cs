using System.Linq.Expressions;

namespace APItask.Repositoris.Implementations
{
    public class TagRepository:Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext db) : base(db)
        {
            
        }

    }
}
