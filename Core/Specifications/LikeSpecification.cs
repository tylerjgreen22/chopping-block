using Core.Entities;

namespace Core.Specifications
{
    // Specification that obtains like by post and user id
    public class LikeSpecification : BaseSpecification<Like>
    {
        public LikeSpecification(string postId, string userId) : base(like => (like.PostId == postId) && (like.UserId == userId))
        {

        }
    }
}