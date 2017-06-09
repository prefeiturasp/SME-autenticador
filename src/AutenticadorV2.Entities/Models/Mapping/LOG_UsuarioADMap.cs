using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class LOG_UsuarioADMap : EntityTypeConfiguration<LOG_UsuarioAD>
    {
        public LOG_UsuarioADMap()
        {
            // Primary Key
            this.HasKey(t => t.usa_id);

            // Properties
            this.Property(t => t.usa_dados)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LOG_UsuarioAD");
            this.Property(t => t.usa_id).HasColumnName("usa_id");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.usa_acao).HasColumnName("usa_acao");
            this.Property(t => t.usa_status).HasColumnName("usa_status");
            this.Property(t => t.usa_dataAcao).HasColumnName("usa_dataAcao");
            this.Property(t => t.usa_origemAcao).HasColumnName("usa_origemAcao");
            this.Property(t => t.usa_dataProcessado).HasColumnName("usa_dataProcessado");
            this.Property(t => t.usa_dados).HasColumnName("usa_dados");

            // Relationships
            this.HasRequired(t => t.SYS_Usuario)
                .WithMany(t => t.LOG_UsuarioAD)
                .HasForeignKey(d => d.usu_id);

        }
    }
}
