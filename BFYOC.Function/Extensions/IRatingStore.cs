using BFYOC.Function.Models;
using System.Threading.Tasks;

namespace BFYOC.Function.Extensions
{
    public interface IRatingStore
    {
        Task StoreRatingAsync(RatingResponseModel model);
    }
}
