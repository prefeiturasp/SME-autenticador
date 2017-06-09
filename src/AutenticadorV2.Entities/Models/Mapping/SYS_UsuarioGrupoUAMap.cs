using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UsuarioGrupoUAMap : EntityTypeConfiguration<SYS_UsuarioGrupoUA>
    {
        public SYS_UsuarioGrupoUAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.usu_id, t.gru_id, t.ugu_id });

            // Properties
            // Table & Column Mappings
            this.ToTable("SYS_UsuarioGrupoUA");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.gru_id).HasColumnName("gru_id");
            this.Property(t => t.ugu_id).HasColumnName("ugu_id");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.uad_id).HasColumnName("uad_id");

            // Relationships
            this.HasRequired(t => t.SYS_Entidade)
                .WithMany(t => t.SYS_UsuarioGrupoUA)
                .HasForeignKey(d => d.ent_id);
            this.HasRequired(t => t.SYS_UnidadeAdministrativa)
                .WithMany(t => t.SYS_UsuarioGrupoUA)
                .HasForeignKey(d => new { d.ent_id, d.uad_id });
            this.HasRequired(t => t.SYS_UsuarioGrupo)
                .WithMany(t => t.SYS_UsuarioGrupoUA)
                .HasForeignKey(d => new { d.usu_id, d.gru_id });

        }
    }
}
