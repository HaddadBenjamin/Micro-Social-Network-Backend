using DiabloII.Domain.Queries.Bases;

namespace DiabloII.Domain.Queries.Domains.Users
{
    public class GetUserQuery : IGetQuery<string>
    {
        public string Id { get; set; }
    }
}
