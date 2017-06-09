using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_VersaoMap : EntityTypeConfiguration<CFG_Versao>
    {
        public CFG_VersaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ver_id);

            // Properties
            this.Property(t => t.ver_Versao)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("CFG_Versao");
            this.Property(t => t.ver_id).HasColumnName("ver_id");
            this.Property(t => t.ver_Versao).HasColumnName("ver_Versao");
            this.Property(t => t.ver_DataCriacao).HasColumnName("ver_DataCriacao");
            this.Property(t => t.ver_DataAlteracao).HasColumnName("ver_DataAlteracao");
        }
    }
}
