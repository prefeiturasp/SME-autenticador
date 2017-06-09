using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_EntidadeMap : EntityTypeConfiguration<Entidade>
    {
        public SYS_EntidadeMap()
        {
            // Primary Key
            this.HasKey(t => t.ent_id);

            // Properties
            this.Property(t => t.ent_codigo)
                .HasMaxLength(20);

            this.Property(t => t.ent_nomeFantasia)
                .HasMaxLength(200);

            this.Property(t => t.ent_razaoSocial)
                .HasMaxLength(200);

            this.Property(t => t.ent_sigla)
                .HasMaxLength(50);

            this.Property(t => t.ent_cnpj)
                .HasMaxLength(14);

            this.Property(t => t.ent_inscricaoMunicipal)
                .HasMaxLength(20);

            this.Property(t => t.ent_inscricaoEstadual)
                .HasMaxLength(20);

            this.Property(t => t.ent_urlAcesso)
                .HasMaxLength(200);

            this.Property(t => t.ent_logoCliente)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("SYS_Entidade");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.ten_id).HasColumnName("ten_id");
            this.Property(t => t.ent_codigo).HasColumnName("ent_codigo");
            this.Property(t => t.ent_nomeFantasia).HasColumnName("ent_nomeFantasia");
            this.Property(t => t.ent_razaoSocial).HasColumnName("ent_razaoSocial");
            this.Property(t => t.ent_sigla).HasColumnName("ent_sigla");
            this.Property(t => t.ent_cnpj).HasColumnName("ent_cnpj");
            this.Property(t => t.ent_inscricaoMunicipal).HasColumnName("ent_inscricaoMunicipal");
            this.Property(t => t.ent_inscricaoEstadual).HasColumnName("ent_inscricaoEstadual");
            this.Property(t => t.ent_idSuperior).HasColumnName("ent_idSuperior");
            this.Property(t => t.ent_situacao).HasColumnName("ent_situacao");
            this.Property(t => t.ent_dataCriacao).HasColumnName("ent_dataCriacao");
            this.Property(t => t.ent_dataAlteracao).HasColumnName("ent_dataAlteracao");
            this.Property(t => t.ent_integridade).HasColumnName("ent_integridade");
            this.Property(t => t.ent_urlAcesso).HasColumnName("ent_urlAcesso");
            this.Property(t => t.ent_exibeLogoCliente).HasColumnName("ent_exibeLogoCliente");
            this.Property(t => t.ent_logoCliente).HasColumnName("ent_logoCliente");
            this.Property(t => t.tep_id).HasColumnName("tep_id");
            this.Property(t => t.tpl_id).HasColumnName("tpl_id");

            // Relationships
            this.HasOptional(t => t.CFG_TemaPaleta)
                .WithMany(t => t.SYS_Entidade)
                .HasForeignKey(d => new { d.tep_id, d.tpl_id });
            this.HasOptional(t => t.SYS_Entidade2)
                .WithMany(t => t.SYS_Entidade1)
                .HasForeignKey(d => d.ent_idSuperior);
            this.HasRequired(t => t.SYS_TipoEntidade)
                .WithMany(t => t.SYS_Entidade)
                .HasForeignKey(d => d.ten_id);

        }
    }
}
