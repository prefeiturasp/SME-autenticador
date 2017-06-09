using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class PES_PessoaContatoMap : EntityTypeConfiguration<PES_PessoaContato>
    {
        public PES_PessoaContatoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.pes_id, t.psc_id });

            // Properties
            this.Property(t => t.psc_contato)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PES_PessoaContato");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.psc_id).HasColumnName("psc_id");
            this.Property(t => t.tmc_id).HasColumnName("tmc_id");
            this.Property(t => t.psc_contato).HasColumnName("psc_contato");
            this.Property(t => t.psc_situacao).HasColumnName("psc_situacao");
            this.Property(t => t.psc_dataCriacao).HasColumnName("psc_dataCriacao");
            this.Property(t => t.psc_dataAlteracao).HasColumnName("psc_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.PES_Pessoa)
                .WithMany(t => t.PES_PessoaContato)
                .HasForeignKey(d => d.pes_id);
            this.HasRequired(t => t.SYS_TipoMeioContato)
                .WithMany(t => t.PES_PessoaContato)
                .HasForeignKey(d => d.tmc_id);

        }
    }
}
