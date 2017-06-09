using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_SistemaEntidadeMap : EntityTypeConfiguration<SYS_SistemaEntidade>
    {
        public SYS_SistemaEntidadeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.sis_id, t.ent_id });

            // Properties
            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.sen_chaveK1)
                .HasMaxLength(100);

            this.Property(t => t.sen_urlAcesso)
                .HasMaxLength(200);

            this.Property(t => t.sen_logoCliente)
                .HasMaxLength(2000);

            this.Property(t => t.sen_urlCliente)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("SYS_SistemaEntidade");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.sen_chaveK1).HasColumnName("sen_chaveK1");
            this.Property(t => t.sen_urlAcesso).HasColumnName("sen_urlAcesso");
            this.Property(t => t.sen_logoCliente).HasColumnName("sen_logoCliente");
            this.Property(t => t.sen_urlCliente).HasColumnName("sen_urlCliente");
            this.Property(t => t.sen_situacao).HasColumnName("sen_situacao");

            // Relationships
            this.HasRequired(t => t.SYS_Entidade)
                .WithMany(t => t.SYS_SistemaEntidade)
                .HasForeignKey(d => d.ent_id);
            this.HasRequired(t => t.SYS_Sistema)
                .WithMany(t => t.SYS_SistemaEntidade)
                .HasForeignKey(d => d.sis_id);

        }
    }
}
