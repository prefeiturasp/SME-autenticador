using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UsuarioFalhaAutenticacaoMap : EntityTypeConfiguration<SYS_UsuarioFalhaAutenticacao>
    {
        public SYS_UsuarioFalhaAutenticacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.usu_id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SYS_UsuarioFalhaAutenticacao");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.ufl_qtdeFalhas).HasColumnName("ufl_qtdeFalhas");
            this.Property(t => t.ufl_dataUltimaTentativa).HasColumnName("ufl_dataUltimaTentativa");

            // Relationships
            this.HasRequired(t => t.SYS_Usuario)
                .WithOptional(t => t.SYS_UsuarioFalhaAutenticacao);

        }
    }
}
