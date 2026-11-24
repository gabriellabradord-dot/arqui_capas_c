using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Medicos
{
    public class CreateModel : PageModel
    {
        private readonly CRegMedicos regMedicos;

        public CreateModel(CRegMedicos regMedicos)
        {
            this.regMedicos = regMedicos;
        }

        [BindProperty]
        public CEMedico Medico { get; set; } = new();

        public string? Error { get; private set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                regMedicos.guardar_medico(Medico);
                TempData["Mensaje"] = $"El médico {Medico.Nom_medico} fue guardado correctamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return Page();
            }
        }
    }
}
