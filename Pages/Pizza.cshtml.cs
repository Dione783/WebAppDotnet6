using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Pages
{
    public class PizzaModel : PageModel
    {
        public List<Pizza> pizzas = new();
        private static Pizza defaultPizza = new Pizza { Id = PizzaService.Pizzas.Count + 1, Name = "Veggie", Price=15.00M, Size=PizzaSize.Small,IsGlutenFree = true };
        [BindProperty]
        public Pizza NewPizza { get; set;}
        private readonly ILogger<PizzaModel> _logger;
        public void OnGet()
        {
            pizzas = PizzaService.GetAll();
        }
        public PizzaModel(ILogger<PizzaModel> logger){
            _logger = logger;
        }
        
        public string GlutenFreeText(Pizza pizza)
        {
                if (pizza.IsGlutenFree)
                return "Gluten Free";
                return "Not Gluten Free";
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(NewPizza == null){
                return Page();
            }
            PizzaService.Add(NewPizza);
            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            PizzaService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}