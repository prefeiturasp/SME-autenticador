using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_EntidadeContatoMap : EntityTypeConfiguration<SYS_EntidadeContato>
    {
        public SYS_EntidadeContatoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ent_id, t.enc_id });

            // Properties
            this.Property(t => t.enc_contato)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SYS_EntidadeContato");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.enc_id).HasColumnName("enc_id");
            this.Property(t => t.tmc_id).HasColumnName("tmc_id");
            this.Property(t => t.enc_contato).HasColumnName("enc_contato");
            this.Property(t => t.enc_situacao).HasColumnName("enc_situacao");
            this.Property(t => t.enc_dataCriacao).HasColumnName("enc_dataCriacao");
            this.Property(t => t.enc_dataAlteracao).HasColumnName("enc_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.SYS_Entidade)
                .WithMany(t => t.SYS_EntidadeContato)
                .HasForeignKey(d => d.ent_id);
            this.HasRequired(t => t.SYS_TipoMeioContato)
                .WithMany(t => t.SYS_EntidadeContato)
                .HasForeignKey(d => d.tmc_id);

        }
    }
}
