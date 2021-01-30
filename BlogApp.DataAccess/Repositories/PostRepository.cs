using BogApp.Models;

namespace BlogApp.DataAccess.Repositories
{
    public class PostRepository : MongoBaseRepository<Post, string>
    {
        public PostRepository(AppSettings settings)
            : base(settings, "Posts")
        {
        }
    }
}
