using crm_software_back.Models;
using crm_software_back.Services.ProjectServices;
using crm_software_back.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crm_software_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<Project?>> getProject(int projectId)
        {
            var user = await _projectService.getProject(projectId);

            if (user == null)
            {
                return NotFound("Project does not exist");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>?>> getProjects()
        {
            var users = await _projectService.getProjects();

            if (users == null)
            {
                return NotFound("Project list is Empty..!");
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Project?>> postProject(Project newProject)
        {
            var project = await _projectService.postProject(newProject);

            if (project == null)
            {
                return NotFound("Project is already exist..!");
            }

            return Ok(project);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<Project?>> putProject(int projectId, Project newProject)
        {
            var project = await _projectService.putProject(projectId, newProject);

            if (project == null)
            {
                return NotFound("Project is not found..!");
            }

            return Ok(project);
        }

        [HttpDelete("{projectId}")]
        public async Task<ActionResult<Project?>> deleteProject(int projectId)
        {
            var project = await _projectService.deleteProject(projectId);

            if (project == null)
            {
                return NotFound("Project is not found..!");
            }

            return Ok(project);
        }
    }
}
