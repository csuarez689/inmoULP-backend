using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InmobiliariaAPI.Domain.Enums;

namespace InmobiliariaAPI.Domain.Entities;

[Table("usuarios")]
public class Usuario
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("nombre")]
    public string nombre { get; set; }

    [Column("apellido")]
    public string apellido { get; set; }

    [Column("email")]
    public string email { get; set; }

    [Column("password")]
    public string password { get; set; }

    [Column("rol")]
    public RolUsuario rol { get; set; }
}
