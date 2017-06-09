using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_SistemaMap : EntityTypeConfiguration<SYS_Sistema>
    {
        public SYS_SistemaMap()
        {
            // Primary Key
            this.HasKey(t => t.sis_id);

            // Properties
            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.sis_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.sis_caminho)
                .HasMaxLength(2000);

            this.Property(t => t.sis_urlImagem)
                .HasMaxLength(2000);

            this.Property(t => t.sis_urlLogoCabecalho)
                .HasMaxLength(2000);

            this.Property(t => t.sis_urlIntegracao)
                .HasMaxLength(200);

            this.Property(t => t.sis_caminhoLogout)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("SYS_Sistema");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.sis_nome).HasColumnName("sis_nome");
            this.Property(t => t.sis_descricao).HasColumnName("sis_descricao");
            this.Property(t => t.sis_caminho).HasColumnName("sis_caminho");
            this.Property(t => t.sis_urlImagem).HasColumnName("sis_urlImagem");
            this.Property(t => t.sis_urlLogoCabecalho).HasColumnName("sis_urlLogoCabecalho");
            this.Property(t => t.sis_tipoAutenticacao).HasColumnName("sis_tipoAutenticacao");
            this.Property(t => t.sis_urlIntegracao).HasColumnName("sis_urlIntegracao");
            this.Property(t => t.sis_situacao).HasColumnName("sis_situacao");
            this.Property(t => t.sis_caminhoLogout).HasColumnName("sis_caminhoLogout");
            this.Property(t => t.sis_ocultarLogo).HasColumnName("sis_ocultarLogo");
        }
    }
}
