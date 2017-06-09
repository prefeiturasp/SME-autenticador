using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_PessoaDocumentoMap : EntityTypeConfiguration<PES_PessoaDocumento>
    {
        public PES_PessoaDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.pes_id, t.tdo_id });

            // Properties
            this.Property(t => t.psd_numero)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.psd_orgaoEmissao)
                .HasMaxLength(200);

            this.Property(t => t.psd_infoComplementares)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("PES_PessoaDocumento");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.tdo_id).HasColumnName("tdo_id");
            this.Property(t => t.psd_numero).HasColumnName("psd_numero");
            this.Property(t => t.psd_dataEmissao).HasColumnName("psd_dataEmissao");
            this.Property(t => t.psd_orgaoEmissao).HasColumnName("psd_orgaoEmissao");
            this.Property(t => t.unf_idEmissao).HasColumnName("unf_idEmissao");
            this.Property(t => t.psd_infoComplementares).HasColumnName("psd_infoComplementares");
            this.Property(t => t.psd_situacao).HasColumnName("psd_situacao");
            this.Property(t => t.psd_dataCriacao).HasColumnName("psd_dataCriacao");
            this.Property(t => t.psd_dataAlteracao).HasColumnName("psd_dataAlteracao");

            // Relationships
            this.HasOptional(t => t.END_UnidadeFederativa)
                .WithMany(t => t.PES_PessoaDocumento)
                .HasForeignKey(d => d.unf_idEmissao);
            this.HasRequired(t => t.PES_Pessoa)
                .WithMany(t => t.PES_PessoaDocumento)
                .HasForeignKey(d => d.pes_id);
            this.HasRequired(t => t.SYS_TipoDocumentacao)
                .WithMany(t => t.PES_PessoaDocumento)
                .HasForeignKey(d => d.tdo_id);

        }
    }
}
