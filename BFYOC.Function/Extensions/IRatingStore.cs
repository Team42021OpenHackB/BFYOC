using BFYOC.Function.Models;
using System.Threading.Tasks;

namespace BFYOC.Function.Extensions
{
    public interface IRatingStore
    {
        Task StoreRatingAsync(RatingResponseModel model);

        Task<RatingResponseModel> GetRatingAsync(string ratingId);

        Task<RatingResponseModel[]> GetRatingsAsync(string userId);
    }
}
