using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_EntidadeEnderecoMap : EntityTypeConfiguration<SYS_EntidadeEndereco>
    {
        public SYS_EntidadeEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ent_id, t.ene_id });

            // Properties
            this.Property(t => t.ene_numero)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ene_complemento)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_EntidadeEndereco");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.ene_id).HasColumnName("ene_id");
            this.Property(t => t.end_id).HasColumnName("end_id");
            this.Property(t => t.ene_numero).HasColumnName("ene_numero");
            this.Property(t => t.ene_complemento).HasColumnName("ene_complemento");
            this.Property(t => t.ene_situacao).HasColumnName("ene_situacao");
            this.Property(t => t.ene_dataCriacao).HasColumnName("ene_dataCriacao");
            this.Property(t => t.ene_dataAlteracao).HasColumnName("ene_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.END_Endereco)
                .WithMany(t => t.SYS_EntidadeEndereco)
                .HasForeignKey(d => d.end_id);
            this.HasRequired(t => t.SYS_Entidade)
                .WithMany(t => t.SYS_EntidadeEndereco)
                .HasForeignKey(d => d.ent_id);

        }
    }
}
