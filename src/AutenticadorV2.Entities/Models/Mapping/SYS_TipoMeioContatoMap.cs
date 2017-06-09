using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_TipoMeioContatoMap : EntityTypeConfiguration<SYS_TipoMeioContato>
    {
        public SYS_TipoMeioContatoMap()
        {
            // Primary Key
            this.HasKey(t => t.tmc_id);

            // Properties
            this.Property(t => t.tmc_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_TipoMeioContato");
            this.Property(t => t.tmc_id).HasColumnName("tmc_id");
            this.Property(t => t.tmc_nome).HasColumnName("tmc_nome");
            this.Property(t => t.tmc_validacao).HasColumnName("tmc_validacao");
            this.Property(t => t.tmc_situacao).HasColumnName("tmc_situacao");
            this.Property(t => t.tmc_dataCriacao).HasColumnName("tmc_dataCriacao");
            this.Property(t => t.tmc_dataAlteracao).HasColumnName("tmc_dataAlteracao");
            this.Property(t => t.tmc_integridade).HasColumnName("tmc_integridade");
        }
    }
}
