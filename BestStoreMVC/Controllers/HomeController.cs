using BestStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BestStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment environment)
       
        {
            _logger = logger;
            this.context = context;
            this.environment = environment;
        }


        public IActionResult Index()
        {
            var items = context.Items.ToList();
            //  var items = context.Items.OrderByDescending(p => p.Id).ToList();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ItemDto itemDto)
        {

            if (!ModelState.IsValid)
            {
                return View(itemDto);
            }

       
            //save new item in database
            Item item = new Item
            {
                Name = itemDto.Name,
                Brand = itemDto.Brand,
                Category = itemDto.Category,
                Price = itemDto.Price,
                Description = itemDto.Description,
                CreatedAt = DateTime.Now
            };
            context.Items.Add(item);
            context.SaveChanges();
            return RedirectToAction("index", "Home");
        }
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Edit(int id)
        {
            var item = context.Items.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //edit product in database
            var itemDto = new ItemDto()
            {
                Name = item.Name,
                Brand = item.Brand,
                Category = item.Category,
                Price = item.Price,
                Description = item.Description
            };
            ViewData["ProductId"] = item.Id;
            ViewData["CreatedAt"] = item.CreatedAt.ToString("MM/dd/yyyy");
            return View(itemDto);
        }
        [HttpPost]
        public IActionResult Edit(int id, ItemDto itemDto)
        {

            var items = context.Items.Find(id);
            if (items == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ItemId"] = items.Id;
                ViewData["CreatedAt"] = items.CreatedAt.ToString("MM/dd/yyyy");
                return View(itemDto);
            }

            //update the product in the database
            items.Name = itemDto.Name;
            items.Brand = itemDto.Brand;
            items.Category = itemDto.Category;
            items.Price = itemDto.Price;
            items.Description = itemDto.Description;
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Delete(int id)
        {
            var items = context.Items.Find(id);
            if (items == null)
            {
                return RedirectToAction("Index", "Home");
            }
            context.Items.Remove(items);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
