using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Pacientes
{
    public class IndexModel : PageModel
    {
        private readonly CRegPacientes regPacientes;

        public IndexModel(CRegPacientes regPacientes)
        {
            this.regPacientes = regPacientes;
        }

        [BindProperty(SupportsGet = true)]
        public string? Buscar { get; set; }

        public List<CEPaciente> Pacientes { get; private set; } = new();

        public void OnGet()
        {
            List<CEPaciente> pacientes = regPacientes.listar_pacientes();

            if (!string.IsNullOrWhiteSpace(Buscar))
            {
                string filtro = Buscar.Trim();
                pacientes = pacientes.Where(p =>
                    p.Id_paciente.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    p.Nom_paciente.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            Pacientes = pacientes;
        }

        public IActionResult OnPostAnular(string id)
        {
            try
            {
                regPacientes.anular_paciente(id);
                TempData["Mensaje"] = $"El paciente {id} fue inactivado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
