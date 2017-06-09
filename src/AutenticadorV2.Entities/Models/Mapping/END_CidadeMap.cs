using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class END_CidadeMap : EntityTypeConfiguration<END_Cidade>
    {
        public END_CidadeMap()
        {
            // Primary Key
            this.HasKey(t => t.cid_id);

            // Properties
            this.Property(t => t.cid_nome)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.cid_ddd)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("END_Cidade");
            this.Property(t => t.cid_id).HasColumnName("cid_id");
            this.Property(t => t.pai_id).HasColumnName("pai_id");
            this.Property(t => t.unf_id).HasColumnName("unf_id");
            this.Property(t => t.cid_nome).HasColumnName("cid_nome");
            this.Property(t => t.cid_ddd).HasColumnName("cid_ddd");
            this.Property(t => t.cid_situacao).HasColumnName("cid_situacao");
            this.Property(t => t.cid_integridade).HasColumnName("cid_integridade");

            // Relationships
            this.HasRequired(t => t.END_Pais)
                .WithMany(t => t.END_Cidade)
                .HasForeignKey(d => d.pai_id);
            this.HasOptional(t => t.END_UnidadeFederativa)
                .WithMany(t => t.END_Cidade)
                .HasForeignKey(d => d.unf_id);

        }
    }
}
