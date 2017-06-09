using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_BancoRelacionadoMap : EntityTypeConfiguration<SYS_BancoRelacionado>
    {
        public SYS_BancoRelacionadoMap()
        {
            // Primary Key
            this.HasKey(t => t.bdr_id);

            // Properties
            this.Property(t => t.bdr_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_BancoRelacionado");
            this.Property(t => t.bdr_id).HasColumnName("bdr_id");
            this.Property(t => t.bdr_nome).HasColumnName("bdr_nome");
        }
    }
}
