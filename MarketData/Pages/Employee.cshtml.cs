using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MarketData.Pages
{
    public class EmployeeModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            ViewData["data"] = $"Name: { employee.Name }, Address: { employee.Address }";
        }
    }

    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name lenght should be 1 to 50.")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Address lenght should be 1 to 255.")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}