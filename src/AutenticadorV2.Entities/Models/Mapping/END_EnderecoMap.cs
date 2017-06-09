using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class END_EnderecoMap : EntityTypeConfiguration<END_Endereco>
    {
        public END_EnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.end_id);

            // Properties
            this.Property(t => t.end_cep)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.end_logradouro)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.end_bairro)
                .HasMaxLength(100);

            this.Property(t => t.end_distrito)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("END_Endereco");
            this.Property(t => t.end_id).HasColumnName("end_id");
            this.Property(t => t.end_cep).HasColumnName("end_cep");
            this.Property(t => t.end_logradouro).HasColumnName("end_logradouro");
            this.Property(t => t.end_bairro).HasColumnName("end_bairro");
            this.Property(t => t.end_distrito).HasColumnName("end_distrito");
            this.Property(t => t.end_zona).HasColumnName("end_zona");
            this.Property(t => t.cid_id).HasColumnName("cid_id");
            this.Property(t => t.end_situacao).HasColumnName("end_situacao");
            this.Property(t => t.end_dataCriacao).HasColumnName("end_dataCriacao");
            this.Property(t => t.end_dataAlteracao).HasColumnName("end_dataAlteracao");
            this.Property(t => t.end_integridade).HasColumnName("end_integridade");

            // Relationships
            this.HasRequired(t => t.END_Cidade)
                .WithMany(t => t.END_Endereco)
                .HasForeignKey(d => d.cid_id);

        }
    }
}
