using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Usuario")]
[Index("Nome", Name = "UQ__Usuario__7D8FE3B2E578F195", IsUnique = true)]
[Index("Email", Name = "UQ__Usuario__A9D10534F95ACDB3", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    public string Senha { get; set; } = null!;

    public Guid? IdTipoUsuario { get; set; }

    [JsonIgnore]
    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<ComentarioEvento> ComentarioEventos { get; set; } = new List<ComentarioEvento>();

    [ForeignKey("IdTipoUsuario")]
    [InverseProperty("Usuarios")]
    public virtual TipoUsuario? IdTipoUsuarioNavigation { get; set; }

    [JsonIgnore]
    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Presente> Presentes { get; set; } = new List<Presente>();
}
