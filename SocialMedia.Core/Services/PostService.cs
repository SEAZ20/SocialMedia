using SocialMedia.Core.CustomEntites;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exeptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetPost(id);
        }

        public PageList<Post> GetPosts(PostQueryFilter filters)
       {
            var posts= _postRepository.GetPosts();
            if (filters.UserId !=null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }
            if (filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }
            var pagedPosts = PageList<Post>.Create(posts, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

      

        //public Task<IEnumerable<Post>> GetPostsByUser(int userId)
        //{
        //    var postuser = _postRepository.GetPostsByUser(userId);
        //    return postuser;
        //}

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUser(post.UserId);
            if (user == null)
            {
                throw new BusinessExeption("Usuario no esta registrado");
            }
            var userpost = await _postRepository.GetPostsByUser(post.UserId);
            if (userpost.Count()<10)
            {
                var lastpost = userpost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if((DateTime.Now- lastpost.Date).TotalDays < 7)
                {
                    throw new BusinessExeption("Tu no puedes publicar");
                }
            }
            if (post.Description.Contains("sexo"))
            {
                throw new BusinessExeption("Contenido no permitido");
            }
            await _postRepository.InsertPost(post);
            
        }

        public async Task<bool> UpdatePost(Post post)
        {
           return await _postRepository.UpdatePost(post);
        }
    }
}
