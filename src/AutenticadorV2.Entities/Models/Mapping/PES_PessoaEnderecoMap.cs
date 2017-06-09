using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_PessoaEnderecoMap : EntityTypeConfiguration<PES_PessoaEndereco>
    {
        public PES_PessoaEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.pes_id, t.pse_id });

            // Properties
            this.Property(t => t.pse_numero)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.pse_complemento)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PES_PessoaEndereco");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.pse_id).HasColumnName("pse_id");
            this.Property(t => t.end_id).HasColumnName("end_id");
            this.Property(t => t.pse_numero).HasColumnName("pse_numero");
            this.Property(t => t.pse_complemento).HasColumnName("pse_complemento");
            this.Property(t => t.pse_situacao).HasColumnName("pse_situacao");
            this.Property(t => t.pse_dataCriacao).HasColumnName("pse_dataCriacao");
            this.Property(t => t.pse_dataAlteracao).HasColumnName("pse_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.END_Endereco)
                .WithMany(t => t.PES_PessoaEndereco)
                .HasForeignKey(d => d.end_id);
            this.HasRequired(t => t.PES_Pessoa)
                .WithMany(t => t.PES_PessoaEndereco)
                .HasForeignKey(d => d.pes_id);

        }
    }
}
