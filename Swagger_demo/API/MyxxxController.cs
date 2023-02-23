using Microsoft.AspNetCore.Mvc;
using Swagger_demo.Repository;
using Swagger_demo.Models;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.JsonPatch;

namespace Swagger_demo.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyxxxController : ControllerBase
    {
        private readonly ICRUD _crudService;

        public MyxxxController(ICRUD crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<IActionResult>? Get()
        {
            var rt = await _crudService.Gets();
            return new JsonResult(rt);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult>? Get(int id)
        {
            var rt = await _crudService.Get(id);
            if (rt != null)
            {
                return new JsonResult(rt);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult>? Post(tBook book)
        {
            var rt = await _crudService.Post(book);

            if (string.IsNullOrWhiteSpace(rt))
            {
                return new JsonResult(book);
            }
            else
            {
                return BadRequest(rt);
            }
        }

        [HttpPut]
        public async Task<IActionResult>? Put(tBook book)
        {
            var rt = await _crudService.Put(book);

            if (string.IsNullOrWhiteSpace(rt))
            {
                return new JsonResult(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult>? Patch(int id, [FromBody] JsonPatchDocument<tBook> patch)
        {
            if (patch != null)
            {
                var book = await _crudService.Get(id);

                if (book == null)
                {
                    return NotFound();
                }
                patch.ApplyTo(book, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rt = await _crudService.Patch(book);
                if (string.IsNullOrWhiteSpace(rt))
                {
                    return new JsonResult(book);
                }
                else
                {
                    return BadRequest(rt);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public async Task<IActionResult>? Delete(int id)
        {
            var isDel = await _crudService.Delete(id);

            if (isDel)
            {
                return Ok();
            }
            else
            {
                return BadRequest("失敗");
            }
        }
    }
}
