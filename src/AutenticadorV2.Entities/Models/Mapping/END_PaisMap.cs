using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class END_PaisMap : EntityTypeConfiguration<END_Pais>
    {
        public END_PaisMap()
        {
            // Primary Key
            this.HasKey(t => t.pai_id);

            // Properties
            this.Property(t => t.pai_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.pai_sigla)
                .HasMaxLength(10);

            this.Property(t => t.pai_ddi)
                .HasMaxLength(3);

            this.Property(t => t.pai_naturalMasc)
                .HasMaxLength(100);

            this.Property(t => t.pai_naturalFem)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("END_Pais");
            this.Property(t => t.pai_id).HasColumnName("pai_id");
            this.Property(t => t.pai_nome).HasColumnName("pai_nome");
            this.Property(t => t.pai_sigla).HasColumnName("pai_sigla");
            this.Property(t => t.pai_ddi).HasColumnName("pai_ddi");
            this.Property(t => t.pai_naturalMasc).HasColumnName("pai_naturalMasc");
            this.Property(t => t.pai_naturalFem).HasColumnName("pai_naturalFem");
            this.Property(t => t.pai_situacao).HasColumnName("pai_situacao");
            this.Property(t => t.pai_integridade).HasColumnName("pai_integridade");
        }
    }
}
