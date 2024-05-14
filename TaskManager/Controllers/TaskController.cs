using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TaskController : Controller
	{
		private List<Model.Task> _tasks;

		public TaskController()
		{
			//тут будут даные из бд
			_tasks = new List<Model.Task>() { new Model.Task() {Id= 1, Title="First", Description= "FirstDescription", IsReady=false},
			new Model.Task() {Id= 2, Title="Second", Description= "SecondDescription", IsReady= true}};
		}
		[HttpGet(Name = "GetTasks")]
		public IActionResult GetTasks()
		{
			return Ok(_tasks);
		}


		[HttpGet("{id}", Name ="GetTask")]
		public IActionResult GetTask(int id)
		{
			Model.Task? task = _tasks.FirstOrDefault(i=> i.Id==id);
            if (task==null)
            {
				return NotFound();
            }
            return Ok(task);
		}
		[HttpDelete("{id}", Name = "DeleteTask")]
		public IActionResult DeleteTask(int id) 
		{
			Model.Task? task = _tasks.FirstOrDefault(i => i.Id == id);

			if (task == null)
				return NotFound();

			_tasks.Remove(task);
			return NoContent();
		}
		[HttpPut("{id}", Name = "DeleteTask")]
		public IActionResult UpdateTask(int id, Model.Task updateTask)
		{
			Model.Task? task = _tasks.FirstOrDefault(i => i.Id == id);
			if (task == null)
				return NotFound();

			task.Title= updateTask.Title;
			task.Description= updateTask.Description;
			task.IsReady=updateTask.IsReady;
			return NoContent();
		}
		[HttpPost(Name = "CreateTask")]
		public IActionResult CreateTask(Model.Task task)
		{
			_tasks.Add(task);
			return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
		}
		
	}
}
