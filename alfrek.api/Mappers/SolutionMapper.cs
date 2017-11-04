using System.Linq;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
using alfrek.api.Controllers.Resources.View.Solutions;
using alfrek.api.Models;
using alfrek.api.Models.Solutions;

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

        public static SolutionListResource ToListSolution(this Solution solution)
        {
            return new SolutionListResource
            (
                solution.Id,
                solution.Title,
                solution.ByLine
            );
        }

        public static SolutionResource ToSingleSolution(this Solution solution)
        {
            var resource = new SolutionResource
            (
                solution.Id,
                solution.Title,
                solution.ByLine,
                solution.Rating,
                solution.ProblemBody,
                solution.SolutionBody,
                solution.Comments
            );
            resource.Author.Email = solution.Author.Email;
            resource.Author.Name = solution.Author.UserName;
            resource.CoAuthors = solution.CoAuthors.Select(a => new AuthorResource(a.Email, a.Name)).ToList();

            return resource;
        }

        public static Solution FromSaveSolutionResource(this SaveSolutionResource solutionResource)
        {
            var resource = new Solution(
                solutionResource.Title,
                solutionResource.ByLine,
                null,
                solutionResource.ProblemBody,
                solutionResource.SolutionBody
            );

            resource.Views = 0;

            foreach (var coAuthor in solutionResource.CoAuthors)
            {
                resource.CoAuthors.Add(new Author(coAuthor.Email, coAuthor.Name));
            }
            
            return resource;
        }

        public static void MapFromEditSolutionResource(this Solution solution, EditSolutionResource solutionResource)
        {
            solution.Title = solutionResource.Title;
            solution.ByLine = solutionResource.ByLine;
            solution.ProblemBody = solutionResource.ProblemBody;
            solution.SolutionBody = solutionResource.SolutionBody;
        }
    }
}