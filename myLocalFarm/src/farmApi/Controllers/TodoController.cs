﻿using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using farmApi.Models;
using farmApi.DAL;
using farmApi.DAL.Interfaces;

namespace farmApi.Controllers
{
    // api/todo
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET /api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return unitOfWork.TodoItemRepository.All();
        }

        // GET /api/todo/1
        [HttpGet("{id:int}", Name = "GetByIdRoute")]
        public IActionResult GetById(int id)
        {
            var items = unitOfWork.TodoItemRepository.GetById(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(items);
        }

        // POST /api/todo
        [HttpPost]
        public void CreateTodoItem([FromBody] TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            else
            {
                unitOfWork.TodoItemRepository.Add(item);

                string url = Url.RouteUrl("GetByIdRoute", new { id = item.Id },
                    Request.Scheme, Request.Host.ToUriComponent());

                Context.Response.StatusCode = 201;
                Context.Response.Headers["Location"] = url;
            }
        }

        // DELETE /api/todo/1
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            if (unitOfWork.TodoItemRepository.TryDelete(id))
            {
                return new HttpStatusCodeResult(204);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}
