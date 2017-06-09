using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_TipoDocumentacaoMap : EntityTypeConfiguration<SYS_TipoDocumentacao>
    {
        public SYS_TipoDocumentacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.tdo_id);

            // Properties
            this.Property(t => t.tdo_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.tdo_sigla)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SYS_TipoDocumentacao");
            this.Property(t => t.tdo_id).HasColumnName("tdo_id");
            this.Property(t => t.tdo_nome).HasColumnName("tdo_nome");
            this.Property(t => t.tdo_sigla).HasColumnName("tdo_sigla");
            this.Property(t => t.tdo_validacao).HasColumnName("tdo_validacao");
            this.Property(t => t.tdo_situacao).HasColumnName("tdo_situacao");
            this.Property(t => t.tdo_dataCriacao).HasColumnName("tdo_dataCriacao");
            this.Property(t => t.tdo_dataAlteracao).HasColumnName("tdo_dataAlteracao");
            this.Property(t => t.tdo_integridade).HasColumnName("tdo_integridade");
            this.Property(t => t.tdo_classificacao).HasColumnName("tdo_classificacao");
            this.Property(t => t.tdo_atributos).HasColumnName("tdo_atributos");
        }
    }
}