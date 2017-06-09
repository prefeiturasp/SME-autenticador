using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_TipoDeficienciaMap : EntityTypeConfiguration<PES_TipoDeficiencia>
    {
        public PES_TipoDeficienciaMap()
        {
            // Primary Key
            this.HasKey(t => t.tde_id);

            // Properties
            this.Property(t => t.tde_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PES_TipoDeficiencia");
            this.Property(t => t.tde_id).HasColumnName("tde_id");
            this.Property(t => t.tde_nome).HasColumnName("tde_nome");
            this.Property(t => t.tde_situacao).HasColumnName("tde_situacao");
            this.Property(t => t.tde_dataCriacao).HasColumnName("tde_dataCriacao");
            this.Property(t => t.tde_dataAlteracao).HasColumnName("tde_dataAlteracao");
            this.Property(t => t.tde_integridade).HasColumnName("tde_integridade");
        }
    }
}
