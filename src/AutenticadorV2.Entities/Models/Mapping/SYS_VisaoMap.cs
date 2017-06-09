using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_VisaoMap : EntityTypeConfiguration<SYS_Visao>
    {
        public SYS_VisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.vis_id);

            // Properties
            this.Property(t => t.vis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.vis_nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_Visao");
            this.Property(t => t.vis_id).HasColumnName("vis_id");
            this.Property(t => t.vis_nome).HasColumnName("vis_nome");
        }
    }
}
