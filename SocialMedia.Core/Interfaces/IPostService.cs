﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
      //  Task<IEnumerable<Post>> GetPostsByUser(int userId);
    }
}