using Core.Entities;

namespace Core.Specifications
{
    public class LikeSpecification : BaseSpecification<Like>
    {
        public LikeSpecification(string postId, string userId) : base(like => (like.PostId == postId) && (like.UserId == userId))
        {

        }
    }
}