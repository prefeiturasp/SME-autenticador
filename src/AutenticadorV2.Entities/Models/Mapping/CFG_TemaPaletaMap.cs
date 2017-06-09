using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_TemaPaletaMap : EntityTypeConfiguration<CFG_TemaPaleta>
    {
        public CFG_TemaPaletaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.tep_id, t.tpl_id });

            // Properties
            this.Property(t => t.tep_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.tpl_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.tpl_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.tpl_caminhoCSS)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.tpl_imagemExemploTema)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("CFG_TemaPaleta");
            this.Property(t => t.tep_id).HasColumnName("tep_id");
            this.Property(t => t.tpl_id).HasColumnName("tpl_id");
            this.Property(t => t.tpl_nome).HasColumnName("tpl_nome");
            this.Property(t => t.tpl_caminhoCSS).HasColumnName("tpl_caminhoCSS");
            this.Property(t => t.tpl_imagemExemploTema).HasColumnName("tpl_imagemExemploTema");
            this.Property(t => t.tpl_situacao).HasColumnName("tpl_situacao");
            this.Property(t => t.tpl_dataCriacao).HasColumnName("tpl_dataCriacao");
            this.Property(t => t.tpl_dataAlteracao).HasColumnName("tpl_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.CFG_TemaPadrao)
                .WithMany(t => t.CFG_TemaPaleta)
                .HasForeignKey(d => d.tep_id);

        }
    }
}
