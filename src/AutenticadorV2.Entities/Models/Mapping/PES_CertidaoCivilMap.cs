using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_CertidaoCivilMap : EntityTypeConfiguration<PES_CertidaoCivil>
    {
        public PES_CertidaoCivilMap()
        {
            // Primary Key
            this.HasKey(t => new { t.pes_id, t.ctc_id });

            // Properties
            this.Property(t => t.ctc_numeroTermo)
                .HasMaxLength(50);

            this.Property(t => t.ctc_folha)
                .HasMaxLength(20);

            this.Property(t => t.ctc_livro)
                .HasMaxLength(20);

            this.Property(t => t.ctc_nomeCartorio)
                .HasMaxLength(200);

            this.Property(t => t.ctc_distritoCartorio)
                .HasMaxLength(100);

            this.Property(t => t.ctc_matricula)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("PES_CertidaoCivil");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.ctc_id).HasColumnName("ctc_id");
            this.Property(t => t.ctc_tipo).HasColumnName("ctc_tipo");
            this.Property(t => t.ctc_numeroTermo).HasColumnName("ctc_numeroTermo");
            this.Property(t => t.ctc_folha).HasColumnName("ctc_folha");
            this.Property(t => t.ctc_livro).HasColumnName("ctc_livro");
            this.Property(t => t.ctc_dataEmissao).HasColumnName("ctc_dataEmissao");
            this.Property(t => t.ctc_nomeCartorio).HasColumnName("ctc_nomeCartorio");
            this.Property(t => t.cid_idCartorio).HasColumnName("cid_idCartorio");
            this.Property(t => t.unf_idCartorio).HasColumnName("unf_idCartorio");
            this.Property(t => t.ctc_distritoCartorio).HasColumnName("ctc_distritoCartorio");
            this.Property(t => t.ctc_situacao).HasColumnName("ctc_situacao");
            this.Property(t => t.ctc_dataCriacao).HasColumnName("ctc_dataCriacao");
            this.Property(t => t.ctc_dataAlteracao).HasColumnName("ctc_dataAlteracao");
            this.Property(t => t.ctc_matricula).HasColumnName("ctc_matricula");

            // Relationships
            this.HasOptional(t => t.END_Cidade)
                .WithMany(t => t.PES_CertidaoCivil)
                .HasForeignKey(d => d.cid_idCartorio);
            this.HasOptional(t => t.END_UnidadeFederativa)
                .WithMany(t => t.PES_CertidaoCivil)
                .HasForeignKey(d => d.unf_idCartorio);
            this.HasRequired(t => t.PES_Pessoa)
                .WithMany(t => t.PES_CertidaoCivil)
                .HasForeignKey(d => d.pes_id);

        }
    }
}
