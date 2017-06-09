using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UsuarioSenhaHistoricoMap : EntityTypeConfiguration<SYS_UsuarioSenhaHistorico>
    {
        public SYS_UsuarioSenhaHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.usu_id, t.ush_id });

            // Properties
            this.Property(t => t.ush_senha)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("SYS_UsuarioSenhaHistorico");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.ush_senha).HasColumnName("ush_senha");
            this.Property(t => t.ush_criptografia).HasColumnName("ush_criptografia");
            this.Property(t => t.ush_id).HasColumnName("ush_id");
            this.Property(t => t.ush_data).HasColumnName("ush_data");

            // Relationships
            this.HasRequired(t => t.SYS_Usuario)
                .WithMany(t => t.SYS_UsuarioSenhaHistorico)
                .HasForeignKey(d => d.usu_id);

        }
    }
}
