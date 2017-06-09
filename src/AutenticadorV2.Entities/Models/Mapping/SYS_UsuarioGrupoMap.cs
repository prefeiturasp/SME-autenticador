using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UsuarioGrupoMap : EntityTypeConfiguration<SYS_UsuarioGrupo>
    {
        public SYS_UsuarioGrupoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.usu_id, t.gru_id });

            // Properties
            // Table & Column Mappings
            this.ToTable("SYS_UsuarioGrupo");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.gru_id).HasColumnName("gru_id");
            this.Property(t => t.usg_situacao).HasColumnName("usg_situacao");

            // Relationships
            this.HasRequired(t => t.SYS_Grupo)
                .WithMany(t => t.SYS_UsuarioGrupo)
                .HasForeignKey(d => d.gru_id);
            this.HasRequired(t => t.SYS_Usuario)
                .WithMany(t => t.SYS_UsuarioGrupo)
                .HasForeignKey(d => d.usu_id);

        }
    }
}
