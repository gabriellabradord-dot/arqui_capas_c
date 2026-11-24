using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Pacientes
{
    public class CreateModel : PageModel
    {
        private readonly CRegPacientes regPacientes;

        public CreateModel(CRegPacientes regPacientes)
        {
            this.regPacientes = regPacientes;
        }

        [BindProperty]
        public CEPaciente Paciente { get; set; } = new();

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
                regPacientes.guardar_paciente(Paciente);
                TempData["Mensaje"] = $"El paciente {Paciente.Nom_paciente} fue guardado correctamente.";
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
