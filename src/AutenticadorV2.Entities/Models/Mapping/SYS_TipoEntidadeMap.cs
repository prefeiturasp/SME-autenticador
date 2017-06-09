using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_TipoEntidadeMap : EntityTypeConfiguration<TipoEntidade>
    {
        public SYS_TipoEntidadeMap()
        {
            // Primary Key
            this.HasKey(t => t.ten_id);

            // Properties
            this.Property(t => t.ten_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_TipoEntidade");
            this.Property(t => t.ten_id).HasColumnName("ten_id");
            this.Property(t => t.ten_nome).HasColumnName("ten_nome");
            this.Property(t => t.ten_situacao).HasColumnName("ten_situacao");
            this.Property(t => t.ten_dataCriacao).HasColumnName("ten_dataCriacao");
            this.Property(t => t.ten_dataAlteracao).HasColumnName("ten_dataAlteracao");
            this.Property(t => t.ten_integridade).HasColumnName("ten_integridade");
        }
    }
}
