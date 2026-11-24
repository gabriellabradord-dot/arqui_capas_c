using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Pacientes
{
    public class EditModel : PageModel
    {
        private readonly CRegPacientes regPacientes;

        public EditModel(CRegPacientes regPacientes)
        {
            this.regPacientes = regPacientes;
        }

        [BindProperty]
        public CEPaciente Paciente { get; set; } = new();

        public string? Error { get; private set; }

        public IActionResult OnGet(string id)
        {
            CEPaciente? paciente = regPacientes.obtener_paciente(id);
            if (paciente is null)
                return NotFound();

            Paciente = paciente;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                regPacientes.guardar_paciente(Paciente);
                TempData["Mensaje"] = $"El paciente {Paciente.Nom_paciente} fue actualizado correctamente.";
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
