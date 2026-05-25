using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;

namespace WMS.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();

            return projects.Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                ClientId = p.ClientId,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status
            });
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return null;

            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ClientId = project.ClientId,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };
        }

        public async Task AddAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                ProjectName = dto.ProjectName,
                ClientId = dto.ClientId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status
            };

            await _repository.AddAsync(project);
        }

        public async Task UpdateAsync(int id, UpdateProjectDto dto)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return;

            project.ProjectName = dto.ProjectName;
            project.ClientId = dto.ClientId;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Status = dto.Status;

            await _repository.UpdateAsync(project);
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return;

            await _repository.DeleteAsync(project);
        }
    }
}