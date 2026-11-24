using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Citas
{
    public class IndexModel : PageModel
    {
        private readonly CRegCitas regCitas;

        public IndexModel(CRegCitas regCitas)
        {
            this.regCitas = regCitas;
        }

        [BindProperty(SupportsGet = true)]
        public string? Buscar { get; set; }

        public List<CECitas> Citas { get; private set; } = new();

        public void OnGet()
        {
            List<CECitas> citas = regCitas.listar_citas();

            if (!string.IsNullOrWhiteSpace(Buscar))
            {
                string filtro = Buscar.Trim();
                citas = citas.Where(c =>
                    c.Cod_cita.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    c.Nom_paciente.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    c.Nom_medico.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            Citas = citas;
        }

        public IActionResult OnPostAnular(string codigo)
        {
            try
            {
                regCitas.anular_cita(codigo);
                TempData["Mensaje"] = $"La cita {codigo} fue anulada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
