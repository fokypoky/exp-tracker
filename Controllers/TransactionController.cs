using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        static public int ID = 2;

        public static List<TransactionCategory> transactionCategories = new List<TransactionCategory>
        {
            new TransactionCategory {Id = 1, Name = "Продукты питания", User = "Василий"},
            new TransactionCategory {Id = 2, Name = "Аренда квартиры", User = "Василий"}
        };

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var category = transactionCategories.FirstOrDefault(x => x.Id == id);

            return Ok($"{category.User}, Ваша категория: {category.Name}");
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] TransactionCategory data)
        {
            var newCategory = new TransactionCategory
            {
                Id = ++ID,
                Name = data.Name,
                User = "Василий"
            };

            transactionCategories.Add(newCategory);

            return Ok(newCategory);
        }

    }
}