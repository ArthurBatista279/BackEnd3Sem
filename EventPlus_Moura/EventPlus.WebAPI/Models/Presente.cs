using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Presente")]
public partial class Presente
{
    [Key]
    public Guid IdPresenca { get; set; }

    public bool Situacao { get; set; }

    public Guid? IdEvento { get; set; }

    public Guid? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Presentes")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Presentes")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
