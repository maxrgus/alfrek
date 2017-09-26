using System;

namespace alfrek.api.Controllers.Resources.View
{
    public class CommentListResource
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentBody { get; set; }
    }
}