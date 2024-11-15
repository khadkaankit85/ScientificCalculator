using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;

namespace aspnetcoreapp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Result { get; set; }

        public void OnGet()
        {
            Result = string.Empty;
        }

        public IActionResult OnPost(string InputValue, string Clear)
        {
            try
            {
                if (!string.IsNullOrEmpty(Clear))
                {
                    // Clear button clicked
                    Result = string.Empty;
                    return Page();
                }

                var expression = InputValue;

                // Replace constants and functions with their respective values
                expression = expression.Replace("π", Math.PI.ToString())
                                         .Replace("e", Math.E.ToString())
                                         .Replace("sin", "Math.Sin")
                                         .Replace("cos", "Math.Cos")
                                         .Replace("tan", "Math.Tan")
                                         .Replace("sqrt", "Math.Sqrt")
                                         .Replace("x²", "^2")
                                         .Replace("x³", "^3");

                // Use DataTable to compute the result of the expression
                var result = new DataTable().Compute(expression, null).ToString();

                // Handle errors in expression evaluation
                Result = result == null ? "Error" : result;
            }
            catch (Exception)
            {
                Result = "Error";
            }

            return Page();
        }
    }
}
