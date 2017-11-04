using alfrek.api.Controllers.Resources.View;
using alfrek.api.Models;

namespace alfrek.api.Mappers
{
    public static class SolutionMapper
    {
        public static SolutionPreviewResource ToPreviewSolution(this Solution solution)
        {
            return new SolutionPreviewResource
            (
                solution.Id,
                solution.Title,
                solution.ByLine,
                solution.Rating,
                solution.ProblemBody
            );
        }
    }
}