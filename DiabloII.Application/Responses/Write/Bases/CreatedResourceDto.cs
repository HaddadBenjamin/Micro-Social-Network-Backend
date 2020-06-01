namespace DiabloII.Application.Responses.Write.Bases
{
    public class CreatedResourceDto<ResourceId>
    {
        public CreatedResourceDto(ResourceId id) => Id = id;

        public ResourceId Id { get; set; }
    }
}
