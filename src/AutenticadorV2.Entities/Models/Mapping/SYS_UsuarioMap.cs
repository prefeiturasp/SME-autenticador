using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UsuarioMap : EntityTypeConfiguration<SYS_Usuario>
    {
        public SYS_UsuarioMap()
        {
            // Primary Key
            this.HasKey(t => t.usu_id);

            // Properties
            this.Property(t => t.usu_login)
                .HasMaxLength(500);

            this.Property(t => t.usu_dominio)
                .HasMaxLength(100);

            this.Property(t => t.usu_email)
                .HasMaxLength(500);

            this.Property(t => t.usu_senha)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("SYS_Usuario");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.usu_login).HasColumnName("usu_login");
            this.Property(t => t.usu_dominio).HasColumnName("usu_dominio");
            this.Property(t => t.usu_email).HasColumnName("usu_email");
            this.Property(t => t.usu_senha).HasColumnName("usu_senha");
            this.Property(t => t.usu_criptografia).HasColumnName("usu_criptografia");
            this.Property(t => t.usu_situacao).HasColumnName("usu_situacao");
            this.Property(t => t.usu_dataCriacao).HasColumnName("usu_dataCriacao");
            this.Property(t => t.usu_dataAlteracao).HasColumnName("usu_dataAlteracao");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.usu_integridade).HasColumnName("usu_integridade");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.usu_integracaoAD).HasColumnName("usu_integracaoAD");

            // Relationships
            this.HasOptional(t => t.PES_Pessoa)
                .WithMany(t => t.SYS_Usuario)
                .HasForeignKey(d => d.pes_id);
            this.HasRequired(t => t.SYS_Entidade)
                .WithMany(t => t.SYS_Usuario)
                .HasForeignKey(d => d.ent_id);

        }
    }
}
