using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages.Medicos
{
    public class IndexModel : PageModel
    {
        private readonly CRegMedicos regMedicos;

        public IndexModel(CRegMedicos regMedicos)
        {
            this.regMedicos = regMedicos;
        }

        [BindProperty(SupportsGet = true)]
        public string? Buscar { get; set; }

        public List<CEMedico> Medicos { get; private set; } = new();

        public void OnGet()
        {
            List<CEMedico> medicos = regMedicos.listar_medicos();

            if (!string.IsNullOrWhiteSpace(Buscar))
            {
                string filtro = Buscar.Trim();
                medicos = medicos.Where(m =>
                    m.Id_medico.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    m.Nom_medico.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    m.Especialidad.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            Medicos = medicos;
        }

        public IActionResult OnPostAnular(string id)
        {
            try
            {
                regMedicos.anular_medico(id);
                TempData["Mensaje"] = $"El médico {id} fue inactivado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
