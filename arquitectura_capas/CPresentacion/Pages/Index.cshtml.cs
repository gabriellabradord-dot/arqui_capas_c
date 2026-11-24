using CEntidades;
using CReglas;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CPresentacion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CRegCitas regCitas;
        private readonly CRegPacientes regPacientes;
        private readonly CRegMedicos regMedicos;

        public IndexModel(CRegCitas regCitas, CRegPacientes regPacientes, CRegMedicos regMedicos)
        {
            this.regCitas = regCitas;
            this.regPacientes = regPacientes;
            this.regMedicos = regMedicos;
        }

        public int CitasActivas { get; private set; }
        public int PacientesActivos { get; private set; }
        public int MedicosActivos { get; private set; }
        public List<CECitas> ProximasCitas { get; private set; } = new();
        public string? Error { get; private set; }

        public void OnGet()
        {
            try
            {
                List<CECitas> citas = regCitas.listar_citas();
                CitasActivas = citas.Count(c => c.Activo);
                ProximasCitas = citas
                    .Where(c => c.Activo && c.Fecha >= DateTime.Today)
                    .OrderBy(c => c.Fecha)
                    .ThenBy(c => c.Hora)
                    .Take(5)
                    .ToList();

                PacientesActivos = regPacientes.listar_pacientes().Count(p => p.Activo);
                MedicosActivos = regMedicos.listar_medicos().Count(m => m.Activo);
            }
            catch (Exception ex)
            {
                Error = "No se pudo consultar la base de datos: " + ex.Message;
            }
        }
    }
}
