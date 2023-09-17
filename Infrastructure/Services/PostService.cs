using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;

namespace Infrastructure.Services
{
    // Post service responsible for CRUD operations on posts
    public class PostService : IPostService
    {
        // Injecting unit of work and user accessor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        public PostService(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _unitOfWork = unitOfWork;
        }

        // Get posts based on post params
        public async Task<IReadOnlyList<Post>> GetPostsAsync(PostParams postParams)
        {
            var spec = new PostSpecification(postParams);
            var user = await _userAccessor.GetUser();
            if (postParams.ByUser)
            {

                if (user == null) return null;
                spec = new PostSpecification(postParams, user.Id);
            }

            var posts = await _unitOfWork.Repository<Post>().ListAsync(spec);

            if (posts == null) return null;

            if (user != null)
            {
                foreach (var post in posts)
                {
                    var likeSpec = new LikeSpecification(post.Id, user.Id);
                    var like = await _unitOfWork.Repository<Like>().GetEntityWithSpecAsync(likeSpec);
                    if (like != null) post.IsLiked = true;
                }

            }

            return posts;

        }

        // Get the count of total posts based on params
        public async Task<int> GetPostsCountAsync(PostParams postParams)
        {
            var countSpec = new PostCountSpecification(postParams);

            if (postParams.ByUser)
            {
                var user = await _userAccessor.GetUser();
                if (user == null) return 0;
                countSpec = new PostCountSpecification(postParams, user.Id);
            }

            var totalItems = await _unitOfWork.Repository<Post>().CountAsync(countSpec);

            return totalItems;
        }

        public async Task<IReadOnlyList<Category>> GetPostCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();

            return categories;
        }

        public async Task<Post> GetPostAsync(string id)
        {
            var spec = new PostSpecification(id);

            var post = await _unitOfWork.Repository<Post>().GetEntityWithSpecAsync(spec);

            if (post == null) return null;

            return post;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var user = await _userAccessor.GetUser();
            if (user == null) return null;
            post.UserId = user.Id;

            _unitOfWork.Repository<Post>().Create(post);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return post;
        }

        public async Task<bool> UpdatePostAsync(string id, Post post)
        {
            var user = await _userAccessor.GetUser();
            if (user == null) return false;

            post.Id = id;
            post.UserId = user.Id;

            _unitOfWork.Repository<Post>().Update(post);

            var spec = new StepSpecification(id);

            var stepIds = post.Steps.Select(s => s.Id).ToList();
            var existingSteps = await _unitOfWork.Repository<Step>().ListAsync(spec);

            foreach (var step in post.Steps)
            {
                var existingStep = existingSteps.FirstOrDefault(s => s.Id == step.Id);

                if (existingStep != null)
                {
                    _unitOfWork.Repository<Step>().Update(step);
                }
                else
                {
                    stepIds.Add(step.Id);
                    _unitOfWork.Repository<Step>().Create(step);
                }
            }

            var stepsToDelete = existingSteps.Where(s => !stepIds.Contains(s.Id)).ToList();
            foreach (var step in stepsToDelete)
            {
                _unitOfWork.Repository<Step>().Delete(step);
            }

            var result = await _unitOfWork.Complete();

            if (result <= 0) return false;

            return true;
        }

        public async Task<bool> DeletePostAsync(string id)
        {
            var spec = new PostSpecification(id);
            var post = await _unitOfWork.Repository<Post>().GetEntityWithSpecAsync(spec);
            if (post == null) return false;

            _unitOfWork.Repository<Post>().Delete(post);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return false;

            return true;
        }
    }
}