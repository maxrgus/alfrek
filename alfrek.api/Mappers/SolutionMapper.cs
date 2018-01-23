using System.Collections.Generic;
using System.Linq;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
using alfrek.api.Controllers.Resources.View.Solutions;
using alfrek.api.Models;
using alfrek.api.Models.Joins;
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
            var author = solution.Author;
            var roles = solution.SolutionRoles;
            return new SolutionListResource
            (
                solution.Id,
                solution.Title,
                solution.ByLine,
                solution.Slug,
                solution.MapPurposedRoles(),
                new Author(solution.Author.FirstName, solution.Author.LastName, solution.Author.Affiliation)
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
                solution.Tags,
                solution.Comments
            );
            resource.Author.FirstName = solution.Author.FirstName;
            resource.Author.LastName = solution.Author.LastName;
            resource.Author.Affiliation = solution.Author.Affiliation;
            resource.Author.ProfilePictureUrl = solution.Author.ProfilePictureUrl;
            resource.AuthorSlug = solution.Author.Slug;
            resource.CoAuthors = solution.CoAuthors.Select(a => new Author(a.FirstName, a.LastName)).ToList();

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

            resource.SolutionRoles = new List<SolutionRole>();

            List<SolutionRole> solutionRoles = new List<SolutionRole>();

            foreach (var purposedRole in solutionResource.Roles)
            {
                solutionRoles.Add(new SolutionRole(resource, purposedRole));
            }

            resource.SolutionRoles.AddRange(solutionRoles);

            return resource;
        }

        public static void MapFromEditSolutionResource(this Solution solution, EditSolutionResource solutionResource)
        {
            solution.Title = solutionResource.Title;
            solution.ByLine = solutionResource.ByLine;
            solution.ProblemBody = solutionResource.ProblemBody;
            solution.SolutionBody = solutionResource.SolutionBody;
        }

        public static List<PurposedRole> MapPurposedRoles(this Solution solution)
        {
            List<PurposedRole> mapped = new List<PurposedRole>();
            var result = solution.SolutionRoles.Where(e => e.SolutionId == solution.Id).Select(s => s.PurposedRole).ToList();
            foreach (var role in result)
            {
                mapped.Add(new PurposedRole(role.Id, role.Name));
            }
            return mapped;
        }
    }
}