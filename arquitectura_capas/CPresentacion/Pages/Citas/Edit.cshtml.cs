using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPresentacion.Pages.Citas
{
    public class EditModel : PageModel
    {
        private readonly CRegCitas regCitas;
        private readonly CRegPacientes regPacientes;
        private readonly CRegMedicos regMedicos;

        public EditModel(CRegCitas regCitas, CRegPacientes regPacientes, CRegMedicos regMedicos)
        {
            this.regCitas = regCitas;
            this.regPacientes = regPacientes;
            this.regMedicos = regMedicos;
        }

        [BindProperty]
        public CECitas Cita { get; set; } = new();

        public SelectList Pacientes { get; private set; } = default!;
        public SelectList Medicos { get; private set; } = default!;
        public string? Error { get; private set; }

        public IActionResult OnGet(string codigo)
        {
            CECitas? cita = regCitas.obtener_cita(codigo);
            if (cita is null)
                return NotFound();

            Cita = cita;
            CargarListas();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                CargarListas();
                return Page();
            }

            try
            {
                regCitas.guardar_cita(Cita);
                TempData["Mensaje"] = $"La cita {Cita.Cod_cita} fue actualizada correctamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                CargarListas();
                return Page();
            }
        }

        private void CargarListas()
        {
            Pacientes = new SelectList(regPacientes.listar_pacientes(), "Id_paciente", "Nom_paciente");
            Medicos = new SelectList(regMedicos.listar_medicos(), "Id_medico", "Nom_medico");
        }
    }
}
