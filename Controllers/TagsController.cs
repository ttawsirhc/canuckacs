using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using canuckacs.Models;

namespace canuckacs.Controllers
{
    /*
    The URL path for each method is constructed as follows:
    -Start with the template string in the controller's Route attribute:
    [Route("api/[controller]")] // original code
    [ApiController]
    public class TodoItemsController : ControllerBase
    -Replace [controller] with the name of the controller, which by convention is the controller class name minus the "Controller" suffix.
    -For this sample, the controller class name is TodoItemsController, so the controller name is "TodoItems".
    -ASP.NET Core routing is case insensitive.
    -If the [HttpGet] attribute (below) has a route template (for example, [HttpGet("products")]), append that to the path.
    -This sample doesn't use a template.
    -For more information, see Attribute routing with Http[Verb] attributes.
    */
    [Route("api/[controller]")]
    /*
    The generated code: Marks the class with the [ApiController] attribute.
    This attribute indicates that the controller responds to web API requests.
    For information about specific behaviors that the attribute enables, see Create web APIs with ASP.NET Core (in the tutorial).
    The generated code: Uses Dependency Injection (DI) to inject the database context (TagContext) into the controller.
    The database context is used in each of the CRUD methods in the controller.
    */
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly CanuckContext _context;

        public TagsController(CanuckContext context)
        {
            _context = context;
        }

        /*
        The [HttpGet] attribute denotes a method that responds to an HTTP GET request.
        RETURN VALUES: The return type of the GetTodoItems and GetTodoItem methods is ActionResult<T> type.
        ASP.NET Core automatically serializes the object to JSON and writes the JSON into the body of the response message.
        The response code for this return type is 200 OK, assuming there are no unhandled exceptions.
        Unhandled exceptions are translated into 5xx errors.
        */
        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTag()
        {
            return await _context.Tag.ToListAsync();
        }

        /*
        In the following GetTodoItem method, "{id}" is a placeholder variable for the unique identifier of the to-do item.
        When GetTodoItem is invoked, the value of "{id}" in the URL is provided to the method in its id parameter.

        ActionResult return types can represent a wide range of HTTP status codes.
        For example, GetTodoItem can return two different status values:
        -If no item matches the requested ID, the method returns a 404 status NotFound error code.
        -Otherwise, the method returns 200 with a JSON response body.
        --Returning item results in an HTTP 200 response.
        */
        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(long id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        /*
        THE PUTTODOITEM METHOD; Examine the PutTodoItem method:
        PutTodoItem is similar to PostTodoItem, except it uses HTTP PUT.
        The response is 204 (No Content).
        According to the HTTP specification, a PUT request requires the client to send the entire updated entity, not just the changes.
        **To support partial updates, use HTTP PATCH.** (I.E., PUT IS FOR FULL REQUESTS, PATCH IS FOR PARTIAL REQUESTS!)
        */
        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(long id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /*
        The following code is an HTTP POST method, as indicated by the [HttpPost] attribute.
        The method gets the value of the TodoItem from the body of the HTTP request.
        For more information, see Attribute routing with Http[Verb] attributes.
        */
        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _context.Tag.Add(tag);
            await _context.SaveChangesAsync();

            /*
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem); // original code
            Update the return statement in the PostTodoItem to use the nameof operator:

            The CreatedAtAction method:
            -Returns an HTTP 201 status code if successful.
            --HTTP 201 is the standard response for an HTTP POST method that creates a new resource on the server.
            -Adds a Location header to the response.
            --The Location header specifies the URI of the newly created to-do item.
            --For more information, see 10.2.2 201 Created.
            -References the GetTodoItem action to create the Location header's URI.
            -The C# nameof keyword is used to avoid hard-coding the action name in the CreatedAtAction call.
            */
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(long id)
        {
            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(long id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
