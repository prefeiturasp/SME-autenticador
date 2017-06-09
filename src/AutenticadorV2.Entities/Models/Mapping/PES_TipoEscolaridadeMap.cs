using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_TipoEscolaridadeMap : EntityTypeConfiguration<PES_TipoEscolaridade>
    {
        public PES_TipoEscolaridadeMap()
        {
            // Primary Key
            this.HasKey(t => t.tes_id);

            // Properties
            this.Property(t => t.tes_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PES_TipoEscolaridade");
            this.Property(t => t.tes_id).HasColumnName("tes_id");
            this.Property(t => t.tes_nome).HasColumnName("tes_nome");
            this.Property(t => t.tes_ordem).HasColumnName("tes_ordem");
            this.Property(t => t.tes_situacao).HasColumnName("tes_situacao");
            this.Property(t => t.tes_dataCriacao).HasColumnName("tes_dataCriacao");
            this.Property(t => t.tes_dataAlteracao).HasColumnName("tes_dataAlteracao");
            this.Property(t => t.tes_integridade).HasColumnName("tes_integridade");
        }
    }
}
