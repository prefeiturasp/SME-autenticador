using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_RelatorioMap : EntityTypeConfiguration<CFG_Relatorio>
    {
        public CFG_RelatorioMap()
        {
            // Primary Key
            this.HasKey(t => t.rlt_id);

            // Properties
            this.Property(t => t.rlt_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.rlt_nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CFG_Relatorio");
            this.Property(t => t.rlt_id).HasColumnName("rlt_id");
            this.Property(t => t.rlt_nome).HasColumnName("rlt_nome");
            this.Property(t => t.rlt_situacao).HasColumnName("rlt_situacao");
            this.Property(t => t.rlt_dataCriacao).HasColumnName("rlt_dataCriacao");
            this.Property(t => t.rlt_dataAlteracao).HasColumnName("rlt_dataAlteracao");
            this.Property(t => t.rlt_integridade).HasColumnName("rlt_integridade");

            // Relationships
            this.HasMany(t => t.CFG_ServidorRelatorio)
                .WithMany(t => t.CFG_Relatorio)
                .Map(m =>
                    {
                        m.ToTable("CFG_RelatorioServidorRelatorio");
                        m.MapLeftKey("rlt_id");
                        m.MapRightKey("sis_id", "ent_id", "srr_id");
                    });


        }
    }
}
