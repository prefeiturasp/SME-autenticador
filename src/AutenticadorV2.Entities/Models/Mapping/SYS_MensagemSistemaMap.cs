using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_MensagemSistemaMap : EntityTypeConfiguration<SYS_MensagemSistema>
    {
        public SYS_MensagemSistemaMap()
        {
            // Primary Key
            this.HasKey(t => t.mss_id);

            // Properties
            this.Property(t => t.mss_chave)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.mss_valor)
                .IsRequired();

            this.Property(t => t.mss_descricao)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SYS_MensagemSistema");
            this.Property(t => t.mss_id).HasColumnName("mss_id");
            this.Property(t => t.mss_chave).HasColumnName("mss_chave");
            this.Property(t => t.mss_valor).HasColumnName("mss_valor");
            this.Property(t => t.mss_descricao).HasColumnName("mss_descricao");
            this.Property(t => t.mss_situacao).HasColumnName("mss_situacao");
            this.Property(t => t.mss_dataCriacao).HasColumnName("mss_dataCriacao");
            this.Property(t => t.mss_dataAlteracao).HasColumnName("mss_dataAlteracao");
        }
    }
}
