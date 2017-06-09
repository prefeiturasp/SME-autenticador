using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_GrupoMap : EntityTypeConfiguration<SYS_Grupo>
    {
        public SYS_GrupoMap()
        {
            // Primary Key
            this.HasKey(t => t.gru_id);

            // Properties
            this.Property(t => t.gru_nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_Grupo");
            this.Property(t => t.gru_id).HasColumnName("gru_id");
            this.Property(t => t.gru_nome).HasColumnName("gru_nome");
            this.Property(t => t.gru_situacao).HasColumnName("gru_situacao");
            this.Property(t => t.gru_dataCriacao).HasColumnName("gru_dataCriacao");
            this.Property(t => t.gru_dataAlteracao).HasColumnName("gru_dataAlteracao");
            this.Property(t => t.vis_id).HasColumnName("vis_id");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.gru_integridade).HasColumnName("gru_integridade");

            // Relationships
            this.HasRequired(t => t.SYS_Sistema)
                .WithMany(t => t.SYS_Grupo)
                .HasForeignKey(d => d.sis_id);
            this.HasRequired(t => t.SYS_Visao)
                .WithMany(t => t.SYS_Grupo)
                .HasForeignKey(d => d.vis_id);

        }
    }
}
