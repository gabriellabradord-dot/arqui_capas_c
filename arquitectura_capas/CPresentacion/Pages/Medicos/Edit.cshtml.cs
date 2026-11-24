using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Medicos
{
    public class EditModel : PageModel
    {
        private readonly CRegMedicos regMedicos;

        public EditModel(CRegMedicos regMedicos)
        {
            this.regMedicos = regMedicos;
        }

        [BindProperty]
        public CEMedico Medico { get; set; } = new();

        public string? Error { get; private set; }

        public IActionResult OnGet(string id)
        {
            CEMedico? medico = regMedicos.obtener_medico(id);
            if (medico is null)
                return NotFound();

            Medico = medico;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                regMedicos.guardar_medico(Medico);
                TempData["Mensaje"] = $"El médico {Medico.Nom_medico} fue actualizado correctamente.";
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
