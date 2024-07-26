using System.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;
  

namespace RazorPagesPizza.Pages
{
    public class PizzaModel : PageModel
    {
        public List<Pizza> pizzas = new();

        [BindProperty]
        public Pizza NewPizza { get; set; } = new();
    
        public void OnGet()
        {
            pizzas = PizzaService.GetAll();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            PizzaService.Add(NewPizza);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            PizzaService.Delete(id);
            pizzas = PizzaService.GetAll();
            return Partial("_PizzaTablePartial", pizzas);
        }
    
        public PartialViewResult OnPostAddPizza()
        {
            if (!ModelState.IsValid)
            {
                pizzas = PizzaService.GetAll();
                return Partial("_PizzaTablePartial", pizzas);
            }
            PizzaService.Add(NewPizza);
            pizzas = PizzaService.GetAll();
            return Partial("_PizzaTablePartial", pizzas);
        }

        public string GlutenFreeText(Pizza pizza)
        {
            if (pizza.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }
    }
}