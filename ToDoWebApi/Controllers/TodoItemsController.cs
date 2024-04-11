using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Model;

namespace ToDoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly List<TodoItem> _todoItems;
        public TodoItemsController(List<TodoItem> _items) 
        {
          _todoItems =_items;
        }


        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            return _todoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem item)
        {
            item.Id = _todoItems.Count+1;
            _todoItems.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var existingItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = item.Name;
            existingItem.IsComplete = item.IsComplete;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var itemToRemove = _todoItems.FirstOrDefault(x => x.Id == id);
            if (itemToRemove == null)
            {
                return NotFound();
            }
            _todoItems.Remove(itemToRemove);
            return NoContent();
        }
    }

}
