using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_GrupoPermissaoMap : EntityTypeConfiguration<SYS_GrupoPermissao>
    {
        public SYS_GrupoPermissaoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.gru_id, t.sis_id, t.mod_id });

            // Properties
            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mod_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SYS_GrupoPermissao");
            this.Property(t => t.gru_id).HasColumnName("gru_id");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.mod_id).HasColumnName("mod_id");
            this.Property(t => t.grp_consultar).HasColumnName("grp_consultar");
            this.Property(t => t.grp_inserir).HasColumnName("grp_inserir");
            this.Property(t => t.grp_alterar).HasColumnName("grp_alterar");
            this.Property(t => t.grp_excluir).HasColumnName("grp_excluir");

            // Relationships
            this.HasRequired(t => t.SYS_Grupo)
                .WithMany(t => t.SYS_GrupoPermissao)
                .HasForeignKey(d => d.gru_id);
            this.HasRequired(t => t.SYS_Modulo)
                .WithMany(t => t.SYS_GrupoPermissao)
                .HasForeignKey(d => new { d.sis_id, d.mod_id });

        }
    }
}
