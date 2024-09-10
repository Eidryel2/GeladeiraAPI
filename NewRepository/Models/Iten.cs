using System;
using System.Collections.Generic;

namespace NewRepository.Models;

public partial class Iten
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Andar { get; set; }

    public string Container { get; set; } = null!;

    public string Posicao { get; set; } = null!;
}
