using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_ServidorRelatorioMap : EntityTypeConfiguration<CFG_ServidorRelatorio>
    {
        public CFG_ServidorRelatorioMap()
        {
            // Primary Key
            this.HasKey(t => new { t.sis_id, t.ent_id, t.srr_id });

            // Properties
            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.srr_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.srr_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.srr_descricao)
                .HasMaxLength(1000);

            this.Property(t => t.srr_usuario)
                .HasMaxLength(512);

            this.Property(t => t.srr_senha)
                .HasMaxLength(512);

            this.Property(t => t.srr_dominio)
                .HasMaxLength(512);

            this.Property(t => t.srr_diretorioRelatorios)
                .HasMaxLength(1000);

            this.Property(t => t.srr_pastaRelatorios)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("CFG_ServidorRelatorio");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.srr_id).HasColumnName("srr_id");
            this.Property(t => t.srr_nome).HasColumnName("srr_nome");
            this.Property(t => t.srr_descricao).HasColumnName("srr_descricao");
            this.Property(t => t.srr_remoteServer).HasColumnName("srr_remoteServer");
            this.Property(t => t.srr_usuario).HasColumnName("srr_usuario");
            this.Property(t => t.srr_senha).HasColumnName("srr_senha");
            this.Property(t => t.srr_dominio).HasColumnName("srr_dominio");
            this.Property(t => t.srr_diretorioRelatorios).HasColumnName("srr_diretorioRelatorios");
            this.Property(t => t.srr_pastaRelatorios).HasColumnName("srr_pastaRelatorios");
            this.Property(t => t.srr_situacao).HasColumnName("srr_situacao");
            this.Property(t => t.srr_dataCriacao).HasColumnName("srr_dataCriacao");
            this.Property(t => t.srr_dataAlteracao).HasColumnName("srr_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.SYS_Sistema)
                .WithMany(t => t.CFG_ServidorRelatorio)
                .HasForeignKey(d => d.sis_id);

        }
    }
}
