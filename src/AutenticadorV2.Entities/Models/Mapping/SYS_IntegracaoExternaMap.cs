using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_IntegracaoExternaMap : EntityTypeConfiguration<SYS_IntegracaoExterna>
    {
        public SYS_IntegracaoExternaMap()
        {
            // Primary Key
            this.HasKey(t => t.ine_id);

            // Properties
            this.Property(t => t.ine_descricao)
                .HasMaxLength(200);

            this.Property(t => t.ine_urlInterna)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ine_urlExterna)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ine_dominio)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ine_chave)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ine_tokenInterno)
                .HasMaxLength(50);

            this.Property(t => t.ine_tokenExterno)
                .HasMaxLength(50);

            this.Property(t => t.ine_proxyIP)
                .HasMaxLength(15);

            this.Property(t => t.ine_proxyPorta)
                .HasMaxLength(10);

            this.Property(t => t.ine_proxyAutenticacaoUsuario)
                .HasMaxLength(100);

            this.Property(t => t.ine_proxyAutenticacaoSenha)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_IntegracaoExterna");
            this.Property(t => t.ine_id).HasColumnName("ine_id");
            this.Property(t => t.ine_descricao).HasColumnName("ine_descricao");
            this.Property(t => t.ine_urlInterna).HasColumnName("ine_urlInterna");
            this.Property(t => t.ine_urlExterna).HasColumnName("ine_urlExterna");
            this.Property(t => t.ine_dominio).HasColumnName("ine_dominio");
            this.Property(t => t.ine_tipo).HasColumnName("ine_tipo");
            this.Property(t => t.ine_chave).HasColumnName("ine_chave");
            this.Property(t => t.ine_tokenInterno).HasColumnName("ine_tokenInterno");
            this.Property(t => t.ine_tokenExterno).HasColumnName("ine_tokenExterno");
            this.Property(t => t.ine_proxy).HasColumnName("ine_proxy");
            this.Property(t => t.ine_proxyIP).HasColumnName("ine_proxyIP");
            this.Property(t => t.ine_proxyPorta).HasColumnName("ine_proxyPorta");
            this.Property(t => t.ine_proxyAutenticacao).HasColumnName("ine_proxyAutenticacao");
            this.Property(t => t.ine_proxyAutenticacaoUsuario).HasColumnName("ine_proxyAutenticacaoUsuario");
            this.Property(t => t.ine_proxyAutenticacaoSenha).HasColumnName("ine_proxyAutenticacaoSenha");
            this.Property(t => t.ine_situacao).HasColumnName("ine_situacao");
        }
    }
}
