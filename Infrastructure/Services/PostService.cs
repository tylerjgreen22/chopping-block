using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        public PostService(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Post>> GetPostsAsync(PostParams postParams)
        {
            var spec = new PostListSpecification(postParams);
            var posts = await _unitOfWork.Repository<Post>().ListAsync(spec);

            if (posts == null) return null;

            return posts;

        }

        public async Task<int> GetPostsCountAsync(PostParams postParams)
        {
            var countSpec = new PostCountSpecification(postParams);
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
            var spec = new PostDetailSpecification(id);

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

            var spec = new StepListSpecification(id);

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
            var spec = new PostDetailSpecification(id);
            var post = await _unitOfWork.Repository<Post>().GetEntityWithSpecAsync(spec);
            if (post == null) return false;

            _unitOfWork.Repository<Post>().Delete(post);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return false;

            return true;
        }
    }
}