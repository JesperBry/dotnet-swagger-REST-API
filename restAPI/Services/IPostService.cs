using restAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restAPI.Services
{
    public interface IPostService
    {

        // Should be async methods because this is IO-operations
        Task<List<Post>> GetPosts();

        Task<Post> GetPostById(Guid postId);

        Task<bool> CreateNewPost(Post post);

        Task<bool> UpdatePost(Post postToUpdate);

        Task<bool> DeletePost(Guid postId);
    }
}
