using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_ConfiguracaoMap : EntityTypeConfiguration<CFG_Configuracao>
    {
        public CFG_ConfiguracaoMap()
        {
            // Primary Key
            this.HasKey(t => t.cfg_id);

            // Properties
            this.Property(t => t.cfg_chave)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.cfg_valor)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.cfg_descricao)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CFG_Configuracao");
            this.Property(t => t.cfg_id).HasColumnName("cfg_id");
            this.Property(t => t.cfg_chave).HasColumnName("cfg_chave");
            this.Property(t => t.cfg_valor).HasColumnName("cfg_valor");
            this.Property(t => t.cfg_descricao).HasColumnName("cfg_descricao");
            this.Property(t => t.cfg_situacao).HasColumnName("cfg_situacao");
            this.Property(t => t.cfg_dataCriacao).HasColumnName("cfg_dataCriacao");
            this.Property(t => t.cfg_dataAlteracao).HasColumnName("cfg_dataAlteracao");
        }
    }
}
