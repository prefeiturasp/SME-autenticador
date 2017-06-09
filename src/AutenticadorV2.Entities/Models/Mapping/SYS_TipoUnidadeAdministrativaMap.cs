using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_TipoUnidadeAdministrativaMap : EntityTypeConfiguration<SYS_TipoUnidadeAdministrativa>
    {
        public SYS_TipoUnidadeAdministrativaMap()
        {
            // Primary Key
            this.HasKey(t => t.tua_id);

            // Properties
            this.Property(t => t.tua_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_TipoUnidadeAdministrativa");
            this.Property(t => t.tua_id).HasColumnName("tua_id");
            this.Property(t => t.tua_nome).HasColumnName("tua_nome");
            this.Property(t => t.tua_situacao).HasColumnName("tua_situacao");
            this.Property(t => t.tua_dataCriacao).HasColumnName("tua_dataCriacao");
            this.Property(t => t.tua_dataAlteracao).HasColumnName("tua_dataAlteracao");
            this.Property(t => t.tua_integridade).HasColumnName("tua_integridade");
        }
    }
}
