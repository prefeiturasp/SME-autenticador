using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_ArquivoMap : EntityTypeConfiguration<CFG_Arquivo>
    {
        public CFG_ArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.arq_id);

            // Properties
            this.Property(t => t.arq_nome)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.arq_typeMime)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.arq_data)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CFG_Arquivo");
            this.Property(t => t.arq_id).HasColumnName("arq_id");
            this.Property(t => t.arq_nome).HasColumnName("arq_nome");
            this.Property(t => t.arq_tamanhoKB).HasColumnName("arq_tamanhoKB");
            this.Property(t => t.arq_typeMime).HasColumnName("arq_typeMime");
            this.Property(t => t.arq_data).HasColumnName("arq_data");
            this.Property(t => t.arq_situacao).HasColumnName("arq_situacao");
            this.Property(t => t.arq_dataCriacao).HasColumnName("arq_dataCriacao");
            this.Property(t => t.arq_dataAlteracao).HasColumnName("arq_dataAlteracao");
        }
    }
}
