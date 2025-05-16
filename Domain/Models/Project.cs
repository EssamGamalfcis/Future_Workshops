namespace TaskManagement.Domain.Entities
{
    public class ProjectByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public static ProjectByIdResponse Map(Project project)
         => new ProjectByIdResponse()
         {
             Id = project.Id,
             CreatedDate = project.CreatedDate,
             Name = project.Name,
         };
    }
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public static List<ProjectResponse> Map(List<Project> projects)
         => projects.Select(project => new ProjectResponse
         {
             Id = project.Id,
             CreatedDate = project.CreatedDate,
             Name = project.Name,
         }).ToList();
    }
}