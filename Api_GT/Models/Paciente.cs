using System;
using System.Collections.Generic;

namespace Api_GT.Models;

public partial class Paciente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Documento { get; set; }

    public string? Telefono { get; set; }
}
