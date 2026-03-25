using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus_Moura.Models;

[Table("Contato")]
public partial class Contato
{
    [Key]
    [Column("Id_Contato")]
    public Guid IdContato { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DadosDeContato { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [Column("Id_TipoContato")]
    public Guid IdTipoContato { get; set; }

    [ForeignKey("IdTipoContato")]
    [InverseProperty("Contatos")]
    public virtual TipoContato IdTipoContatoNavigation { get; set; } = null!;
}
