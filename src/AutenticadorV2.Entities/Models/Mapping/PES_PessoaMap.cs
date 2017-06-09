using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_PessoaMap : EntityTypeConfiguration<PES_Pessoa>
    {
        public PES_PessoaMap()
        {
            // Primary Key
            this.HasKey(t => t.pes_id);

            // Properties
            this.Property(t => t.pes_nome)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.pes_nome_abreviado)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PES_Pessoa");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.pes_nome).HasColumnName("pes_nome");
            this.Property(t => t.pes_nome_abreviado).HasColumnName("pes_nome_abreviado");
            this.Property(t => t.pai_idNacionalidade).HasColumnName("pai_idNacionalidade");
            this.Property(t => t.pes_naturalizado).HasColumnName("pes_naturalizado");
            this.Property(t => t.cid_idNaturalidade).HasColumnName("cid_idNaturalidade");
            this.Property(t => t.pes_dataNascimento).HasColumnName("pes_dataNascimento");
            this.Property(t => t.pes_estadoCivil).HasColumnName("pes_estadoCivil");
            this.Property(t => t.pes_racaCor).HasColumnName("pes_racaCor");
            this.Property(t => t.pes_sexo).HasColumnName("pes_sexo");
            this.Property(t => t.pes_idFiliacaoPai).HasColumnName("pes_idFiliacaoPai");
            this.Property(t => t.pes_idFiliacaoMae).HasColumnName("pes_idFiliacaoMae");
            this.Property(t => t.tes_id).HasColumnName("tes_id");
            this.Property(t => t.pes_foto).HasColumnName("pes_foto");
            this.Property(t => t.pes_situacao).HasColumnName("pes_situacao");
            this.Property(t => t.pes_dataCriacao).HasColumnName("pes_dataCriacao");
            this.Property(t => t.pes_dataAlteracao).HasColumnName("pes_dataAlteracao");
            this.Property(t => t.pes_integridade).HasColumnName("pes_integridade");
            this.Property(t => t.arq_idFoto).HasColumnName("arq_idFoto");

            // Relationships
            this.HasMany(t => t.PES_TipoDeficiencia)
                .WithMany(t => t.PES_Pessoa)
                .Map(m =>
                    {
                        m.ToTable("PES_PessoaDeficiencia");
                        m.MapLeftKey("pes_id");
                        m.MapRightKey("tde_id");
                    });

            this.HasOptional(t => t.CFG_Arquivo)
                .WithMany(t => t.PES_Pessoa)
                .HasForeignKey(d => d.arq_idFoto);
            this.HasOptional(t => t.END_Cidade)
                .WithMany(t => t.PES_Pessoa)
                .HasForeignKey(d => d.cid_idNaturalidade);
            this.HasOptional(t => t.END_Pais)
                .WithMany(t => t.PES_Pessoa)
                .HasForeignKey(d => d.pai_idNacionalidade);
            this.HasOptional(t => t.PES_Pessoa2)
                .WithMany(t => t.PES_Pessoa1)
                .HasForeignKey(d => d.pes_idFiliacaoMae);
            this.HasOptional(t => t.PES_Pessoa3)
                .WithMany(t => t.PES_Pessoa11)
                .HasForeignKey(d => d.pes_idFiliacaoPai);
            this.HasOptional(t => t.PES_TipoEscolaridade)
                .WithMany(t => t.PES_Pessoa)
                .HasForeignKey(d => d.tes_id);

        }
    }
}
