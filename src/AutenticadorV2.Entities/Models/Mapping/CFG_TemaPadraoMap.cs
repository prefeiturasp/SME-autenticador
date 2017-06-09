using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_TemaPadraoMap : EntityTypeConfiguration<CFG_TemaPadrao>
    {
        public CFG_TemaPadraoMap()
        {
            // Primary Key
            this.HasKey(t => t.tep_id);

            // Properties
            this.Property(t => t.tep_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.tep_descricao)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CFG_TemaPadrao");
            this.Property(t => t.tep_id).HasColumnName("tep_id");
            this.Property(t => t.tep_nome).HasColumnName("tep_nome");
            this.Property(t => t.tep_descricao).HasColumnName("tep_descricao");
            this.Property(t => t.tep_tipoMenu).HasColumnName("tep_tipoMenu");
            this.Property(t => t.tep_exibeLinkLogin).HasColumnName("tep_exibeLinkLogin");
            this.Property(t => t.tep_tipoLogin).HasColumnName("tep_tipoLogin");
            this.Property(t => t.tep_exibeLogoCliente).HasColumnName("tep_exibeLogoCliente");
            this.Property(t => t.tep_situacao).HasColumnName("tep_situacao");
            this.Property(t => t.tep_dataCriacao).HasColumnName("tep_dataCriacao");
            this.Property(t => t.tep_dataAlteracao).HasColumnName("tep_dataAlteracao");
        }
    }
}
