using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_ServicoMap : EntityTypeConfiguration<SYS_Servico>
    {
        public SYS_ServicoMap()
        {
            // Primary Key
            this.HasKey(t => t.ser_id);

            // Properties
            this.Property(t => t.ser_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ser_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ser_nomeProcedimento)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_Servico");
            this.Property(t => t.ser_id).HasColumnName("ser_id");
            this.Property(t => t.ser_nome).HasColumnName("ser_nome");
            this.Property(t => t.ser_nomeProcedimento).HasColumnName("ser_nomeProcedimento");
            this.Property(t => t.ser_ativo).HasColumnName("ser_ativo");
        }
    }
}
