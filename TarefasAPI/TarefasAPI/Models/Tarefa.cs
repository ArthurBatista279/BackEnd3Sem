using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TarefasAPI.Models;

public partial class Tarefa
{
    [Key]
    [Column("IDTarefas")]
    public Guid Idtarefas { get; set; }

    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [StringLength(255)]
    public string Descricao { get; set; } = null!;

    public bool StatusDeConclusao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataDeCriacao { get; set; }
}
